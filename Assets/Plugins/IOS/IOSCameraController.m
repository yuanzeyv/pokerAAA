#import "IOSCameraController.h"
 
@implementation IOSCameraController
-(void)OpenTarget:(UIImagePickerControllerSourceType)type{
	//创建UIImagePickerController实例
    UIImagePickerController *picker;
    picker= [[UIImagePickerController alloc]init];
    //设置代理
    picker.delegate = self;
    //是否允许编辑 (默认为NO)
    picker.allowsEditing = YES;
    //设置照片的来源
    // UIImagePickerControllerSourceTypePhotoLibrary,      // 来自图库
    // UIImagePickerControllerSourceTypeCamera,            // 来自相机
    // UIImagePickerControllerSourceTypeSavedPhotosAlbum   // 来自相册
    picker.sourceType = type;
    
    //这里需要判断设备是iphone还是ipad  如果使用的是iphone并没有问题 但是如果 是在ipad上调用相册获取图片 会出现没有确定(选择)的按钮 所以这里判断
    //了一下设备，针对ipad 使用另一种方法 但是这种方法是弹出一个界面 并不是覆盖整个界面 需要改进 试过另一种方式 重写一个相册界面 
    //（QQ的ipad选择头像的界面 就使用了这种方式 但是这里我们先不讲 （因为我也不太懂 但是我按照简书的一位老哥的文章写出来了 这里放一下这个简书的链接
    //https://www.jianshu.com/p/0ddf4f7476aa）
    if (picker.sourceType == UIImagePickerControllerSourceTypePhotoLibrary &&[[UIDevice currentDevice] userInterfaceIdiom] == UIUserInterfaceIdiomPad) {
        // 设置弹出的控制器的显示样式
        picker.modalPresentationStyle = UIModalPresentationPopover;
        //获取这个弹出控制器
        UIPopoverPresentationController *popover = picker.popoverPresentationController;
        //设置代理
        popover.delegate = self;
        //下面两个属性设置弹出位置
        popover.sourceRect = CGRectMake(0, 0, 0, 0);
        popover.sourceView = self.view;
        //设置箭头的位置
        popover.permittedArrowDirections = UIPopoverArrowDirectionAny;
        //展示选取照片控制器
        [self presentViewController:picker animated:YES completion:nil];
    } else {
        //展示选取照片控制器
        [self presentViewController:picker animated:YES completion:^{}];
    }
   
}
//选择完成，点击界面中的某个图片或者选择（Choose）按钮时触发
-(void)imagePickerController:(UIImagePickerController *)picker didFinishPickingMediaWithInfo:(NSDictionary<NSString *,id> *)info{
    //关闭界面
    [picker dismissViewControllerAnimated:YES completion:^{}];
    //得到照片
    UIImage *image = [info objectForKey:@"UIImagePickerControllerEditedImage"];
    if (image == nil) {
        image = [info objectForKey:@"UIImagePickerControllerOriginalImage"];
    }
    //有些时候我们拍照后经常发现导出的照片方向会有问题，要么横着，要么颠倒着，需要旋转才适合观看。但是在ios设备上是正常的
    //所以在这里处理了图片  让他旋转成我们需要的
    if (image.imageOrientation != UIImageOrientationUp) {
    //图片旋转
        image = [self fixOrientation:image];
    }
    //获取保存图片的地址
    NSString *imagePath = [self GetSavePath:@"Temp.jpg"];
    //保存图片到沙盒路径 对应unity中的Application.persistentDataPath 之后我们取图片就需要在这个路径下取  这是一个可读可写的路径
    [self SaveFileToDoc:image path:imagePath];
}
//获取保存文件的路径 如果有返回路径 没有创建一个返回
-(NSString*)GetSavePath:(NSString *)filename{
    NSArray *pathArray = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *docPath = [pathArray objectAtIndex:0];
    return [docPath stringByAppendingPathComponent:filename];
}
//将图片保存到沙盒路径
-(void)SaveFileToDoc:(UIImage *)image path:(NSString *)path{
    NSData *data;
    if (UIImagePNGRepresentation(image)==nil) {
        data = UIImageJPEGRepresentation(image, 1);
    }else{
        data = UIImagePNGRepresentation(image);
    }
    [data writeToFile:path atomically:YES];
    //保存之后通知unity 执行对应的回调 
    //UnitySendMessage 是用来给unity发消息的  有三个参数 1.挂载对应回调脚本的物体名 2.回调函数的名称 3.对应回调上的参数
    UnitySendMessage("Canvas", "Message", "Temp.jpg");
}
#pragma mark 图片处理方法
//图片旋转处理
- (UIImage *)fixOrientation:(UIImage *)aImage {
    CGAffineTransform transform = CGAffineTransformIdentity;
    
    switch (aImage.imageOrientation) {
        case UIImageOrientationDown:
        case UIImageOrientationDownMirrored:
            transform = CGAffineTransformTranslate(transform, aImage.size.width, aImage.size.height);
            transform = CGAffineTransformRotate(transform, M_PI);
            break;
            
        case UIImageOrientationLeft:
        case UIImageOrientationLeftMirrored:
            transform = CGAffineTransformTranslate(transform, aImage.size.width, 0);
            transform = CGAffineTransformRotate(transform, M_PI_2);
            break;
            
        case UIImageOrientationRight:
        case UIImageOrientationRightMirrored:
            transform = CGAffineTransformTranslate(transform, 0, aImage.size.height);
            transform = CGAffineTransformRotate(transform, -M_PI_2);
            break;
        default:
            break;
    }
    
    switch (aImage.imageOrientation) {
        case UIImageOrientationUpMirrored:
        case UIImageOrientationDownMirrored:
            transform = CGAffineTransformTranslate(transform, aImage.size.width, 0);
            transform = CGAffineTransformScale(transform, -1, 1);
            break;
            
        case UIImageOrientationLeftMirrored:
        case UIImageOrientationRightMirrored:
            transform = CGAffineTransformTranslate(transform, aImage.size.height, 0);
            transform = CGAffineTransformScale(transform, -1, 1);
            break;
        default:
            break;
    }
    
    // Now we draw the underlying CGImage into a new context, applying the transform
    // calculated above.
    CGContextRef ctx = CGBitmapContextCreate(NULL, aImage.size.width, aImage.size.height,
                                             CGImageGetBitsPerComponent(aImage.CGImage), 0,
                                             CGImageGetColorSpace(aImage.CGImage),
                                             CGImageGetBitmapInfo(aImage.CGImage));
    CGContextConcatCTM(ctx, transform);
    switch (aImage.imageOrientation) {
        case UIImageOrientationLeft:
        case UIImageOrientationLeftMirrored:
        case UIImageOrientationRight:
        case UIImageOrientationRightMirrored:
            // Grr...
            CGContextDrawImage(ctx, CGRectMake(0,0,aImage.size.height,aImage.size.width), aImage.CGImage);
            break;
            
        default:
            CGContextDrawImage(ctx, CGRectMake(0,0,aImage.size.width,aImage.size.height), aImage.CGImage);
            break;
    }
    // And now we just create a new UIImage from the drawing context
    CGImageRef cgimg = CGBitmapContextCreateImage(ctx);
    UIImage *img = [UIImage imageWithCGImage:cgimg];
    CGContextRelease(ctx);
    CGImageRelease(cgimg);
    return img;
}
@end
//由于C++编译器需要支持函数的重载，会改变函数的名称，因此dll的导出函数通常是标准C定义的。
//这就使得C和C++的互相调用变得很常见。但是有时可能又会直接用C来调用，不想重新写代码，
//让标准C编写的dll函数定义在C和C++编译器下都能编译通过，通常会使用以下的格式：（这个格式在很多成熟的代码中很常见）
#if defined(__cplusplus)
extern "C" {
#endif
    //导出接口供unity使用
    void IOS_OpenCamera(){
        IOSCameraController *app = [[IOSCameraController alloc]init];
        UIViewController *vc = UnityGetGLViewController();
        [vc.view addSubview:app.view];
        [app OpenTarget:UIImagePickerControllerSourceTypeCamera];
    }
    void IOS_OpenAlbum(){
        IOSCameraController *app = [[IOSCameraController alloc]init];
        UIViewController *vc = UnityGetGLViewController();
        [vc.view addSubview:app.view];
        [app OpenTarget:UIImagePickerControllerSourceTypePhotoLibrary];
    }
#if defined(__cplusplus)
}
#endif