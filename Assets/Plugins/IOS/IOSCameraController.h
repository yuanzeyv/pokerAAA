//import 引用头文件 相当远Using 
#import<QuartzCore/CADisplayLink.h>
//声明一个IOSCameraController类  继承自UIViewController <>里面是是协议/代理的调用声明 可以理解为c#的接口
@interface IOSCameraController : UIViewController<UIImagePickerControllerDelegate,UINavigationControllerDelegate>
@end
