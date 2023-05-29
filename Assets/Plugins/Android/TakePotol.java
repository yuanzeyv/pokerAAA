//
// Source code recreated from a .class file by IntelliJ IDEA
// (powered by Fernflower decompiler)
//

package com.unity.InvokeAlbum;

import android.Manifest;
import android.content.ContentValues;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.Bitmap.CompressFormat;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.provider.MediaStore.Images.Media;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v4.content.FileProvider;
import android.support.v4.os.EnvironmentCompat;
import android.util.Log;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;

public class TakePotol extends UnityPlayerActivity {
    String gameobject;
    String method;
    String imagePath;
    private ClipImageLayout mClipImageLayout;
    static UnityPlayerActivity mContext = null;
    public static final int NONE = 0;
    public static final int PHOTOCircleTake = 1;
    public static final int PHOTOCircleAlbum = 2;
    public static final int PHOTORectTake = 3;
    public static final int PHOTORectAlbum = 4;
    public static final int PHOTORectResult = 5;
    public static final int PHOTORESOULT = 6;
    public static final String IMAGE_UNSPECIFIED = "image/*";
    public static final String FILE_NAME = "image.png";
    public static final String DATA_URL = "/data/data/";
    // 申请相机权限的requestCode
    private static final int PERMISSION_CAMERA_REQUEST_CODE = 0x00000012;
    //用于保存拍照图片的uri
    private Uri mCameraUri;
    // 用于保存图片的文件路径，Android 10以下使用图片路径访问图片
    private String mCameraImagePath;
    // 是否是Android 10以上手机
    private boolean isAndroidQ = Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.Q;


    public TakePotol() {
    }

    protected void onCreate(Bundle arg0) {
        super.onCreate(arg0);
        mContext = this;
    }

    /**
     * 检查权限并拍照。
     * 调用相机前先检查权限。
     */
    private void checkPermissionAndCamera() {
        int hasCameraPermission = ContextCompat.checkSelfPermission(getApplication(),
                Manifest.permission.CAMERA);
        if (hasCameraPermission == PackageManager.PERMISSION_GRANTED) {
            //有调起相机拍照。
            openCamera();
        } else {
            //没有权限，申请权限。
            ActivityCompat.requestPermissions(this,new String[]{Manifest.permission.CAMERA},
                    PERMISSION_CAMERA_REQUEST_CODE);
        }
    }

    /**
     * 处理权限申请的回调。
     */
    @Override
    public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] grantResults) {
        if (requestCode == PERMISSION_CAMERA_REQUEST_CODE) {
            if (grantResults.length > 0
                    && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                //允许权限，有调起相机拍照。
                openCamera();
            } else {
                //拒绝权限，弹出提示框。
                Toast.makeText(this,"拍照权限被拒绝",Toast.LENGTH_LONG).show();
            }
        }
    }

    /**
     * 调起相机拍照
     */
    private void openCamera() {
        Intent captureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        // 判断是否有相机
        if (captureIntent.resolveActivity(getPackageManager()) != null) {
            File photoFile = null;
            Uri photoUri = null;

            if (isAndroidQ) {
                // 适配android 10
                photoUri = createImageUri();
            } else {
                try {
                    photoFile = createImageFile();
                } catch (IOException e) {
                    e.printStackTrace();
                }

                if (photoFile != null) {
                    mCameraImagePath = photoFile.getAbsolutePath();
                    if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N) {
                        //适配Android 7.0文件权限，通过FileProvider创建一个content类型的Uri
                        photoUri = FileProvider.getUriForFile(this, getPackageName() + ".fileprovider", photoFile);
                    } else {
                        photoUri = Uri.fromFile(photoFile);
                    }
                }
            }

            mCameraUri = photoUri;
            if (photoUri != null) {
                captureIntent.putExtra(MediaStore.EXTRA_OUTPUT, photoUri);
                captureIntent.addFlags(Intent.FLAG_GRANT_WRITE_URI_PERMISSION);
                startActivityForResult(captureIntent, 1);
            }
        }
    }

    /**
     * 创建图片地址uri,用于保存拍照后的照片 Android 10以后使用这种方法
     */
    private Uri createImageUri() {
        String status = Environment.getExternalStorageState();
        // 判断是否有SD卡,优先使用SD卡存储,当没有SD卡时使用手机存储
        if (status.equals(Environment.MEDIA_MOUNTED)) {
            return getContentResolver().insert(MediaStore.Images.Media.EXTERNAL_CONTENT_URI, new ContentValues());
        } else {
            return getContentResolver().insert(MediaStore.Images.Media.INTERNAL_CONTENT_URI, new ContentValues());
        }
    }
//
//    /**
//     * 创建保存图片的文件
//     */
    private File createImageFile() throws IOException {
        String imageName = "temp.png"; // new SimpleDateFormat("yyyyMMdd_HHmmss", Locale.getDefault()).format(new Date());
        File storageDir = getExternalFilesDir(Environment.DIRECTORY_PICTURES);
        if (!storageDir.exists()) {
            storageDir.mkdir();
        }
        File tempFile = new File(storageDir, imageName);
        if (!Environment.MEDIA_MOUNTED.equals(EnvironmentCompat.getStorageState(tempFile))) {
            return null;
        }
        return tempFile;
    }




    public void TakeCircleClipPhoto(String go, String method) {
        this.gameobject = go;
        this.method = method;
        checkPermissionAndCamera();
//        Intent intent = new Intent("android.media.action.IMAGE_CAPTURE");
////        intent.putExtra("output", Uri.fromFile(new File(Environment.getExternalStorageDirectory(), "temp.png")));
//        File file = new File(Environment.getExternalStorageDirectory() + "/temp.png");
//        Uri uri;
//        if (Build.VERSION.SDK_INT < Build.VERSION_CODES.N) {
//            uri = Uri.fromFile(file);
//        } else {
//            uri = FileProvider.getUriForFile(mContext, mContext.getPackageName() + ".provider", file);
//        }
//        intent.putExtra(MediaStore.EXTRA_OUTPUT, uri);
//        Log.i("TakeCircleClipPhoto", uri.toString());
//        this.startActivityForResult(intent, 1);
    }

    public void TakeCircleClipPotolFromAlbum(String go, String method) {
        this.gameobject = go;
        this.method = method;
        Intent intent = new Intent("android.intent.action.PICK", (Uri)null);
        intent.setDataAndType(Media.EXTERNAL_CONTENT_URI, "image/*");
        this.startActivityForResult(intent, 2);
    }

    public void TakeRectClipPhoto(String go, String method) {
        this.gameobject = go;
        this.method = method;
        checkPermissionAndCamera();
        // Intent intent = new Intent("android.media.action.IMAGE_CAPTURE");
        // intent.putExtra("output", Uri.fromFile(new File(Environment.getExternalStorageDirectory(), "temp.png")));
        // this.startActivityForResult(intent, 3);
    }

    public void TakeRectClipPotolFromAlbum(String go, String method) {
        this.gameobject = go;
        this.method = method;
        Intent intent = new Intent("android.intent.action.PICK", (Uri)null);
        intent.setDataAndType(Media.EXTERNAL_CONTENT_URI, "image/*");
        this.startActivityForResult(intent, 4);
    }

    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        Log.i("onActivityResult", requestCode + " " + resultCode);
        if (resultCode != 0) {
            File picture;
            if (requestCode == 1) {
//                picture = new File(Environment.getExternalStorageDirectory() + "/temp.png");
//                this.startPhotoZoom(Uri.fromFile(picture));
                this.startPhotoZoom(mCameraUri);
            }

            if (requestCode == 3) {
                picture = new File(Environment.getExternalStorageDirectory() + "/temp.png");
                this.StartClipActivity(Uri.fromFile(picture));
            }

            if (data != null) {
                if (requestCode == 2) {
                    this.startPhotoZoom(data.getData());
                }

                Uri uri;
                if (requestCode == 4) {
                    uri = data.getData();
                    this.StartClipActivity(uri);
                }

                Bitmap photo;
                if (requestCode == 6) {
                    Bundle extras = data.getExtras();
                    if (extras != null) {
                        photo = (Bitmap)extras.getParcelable("data");

                        try {
                            this.SaveBitmap(photo);
                        } catch (IOException var8) {
                            var8.printStackTrace();
                        }
                    }

                    UnityPlayer.UnitySendMessage(this.gameobject, this.method, this.imagePath);
                }

                if (requestCode == 5) {
                    uri = data.getData();

                    try {
                        photo = Media.getBitmap(this.getContentResolver(), uri);
                        this.SaveBitmap(photo);
                    } catch (IOException var7) {
                        var7.printStackTrace();
                    }

                    UnityPlayer.UnitySendMessage(this.gameobject, this.method, this.imagePath);
                }

                super.onActivityResult(requestCode, resultCode, data);
            }
        }
    }

    public void startPhotoZoom(Uri uri) {
        Intent intent = new Intent("com.android.camera.action.CROP");
        intent.setDataAndType(uri, "image/*");
        intent.putExtra("crop", "true");
        intent.putExtra("aspectX", 1);
        intent.putExtra("aspectY", 1);
        intent.putExtra("outputX", 300);
        intent.putExtra("outputY", 300);
        intent.putExtra("return-data", true);
        this.startActivityForResult(intent, 6);
    }

    public void StartClipActivity(Uri uri) {
        Intent intent = new Intent(this, ClipActivity.class);
        intent.setData(uri);
        this.startActivityForResult(intent, 5);
    }

    public void SaveBitmap(Bitmap bitmap) throws IOException {
        FileOutputStream fOut = null;
//        String path = "/mnt/sdcard/Android/data/com/unity/InvokeAlbum/files";
        String path = getExternalCacheDir().toString();

        try {
            File destDir = new File(path);
            if (!destDir.exists()) {
                destDir.mkdirs();
            }

            fOut = new FileOutputStream(path + "/" + "image.png");
            this.imagePath = path + "/" + "image.png";
        } catch (FileNotFoundException var7) {
            var7.printStackTrace();
        }

        bitmap.compress(CompressFormat.PNG, 100, fOut);

        try {
            fOut.flush();
        } catch (IOException var6) {
            var6.printStackTrace();
        }

        try {
            fOut.close();
        } catch (IOException var5) {
            var5.printStackTrace();
        }

    }
}
