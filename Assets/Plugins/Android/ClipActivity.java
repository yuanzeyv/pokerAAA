//
// Source code recreated from a .class file by IntelliJ IDEA
// (powered by Fernflower decompiler)
//

package com.unity.InvokeAlbum;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore.Images.Media;
import android.view.Menu;
import android.view.MenuItem;
import java.io.FileNotFoundException;
import java.io.IOException;

public class ClipActivity extends Activity {
    private ClipImageLayout mClipImageLayout;

    public ClipActivity() {
    }

    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.setContentView(2130903040);
        this.mClipImageLayout = (ClipImageLayout)this.findViewById(2131165184);
        Uri uri = this.getIntent().getData();

        try {
            Bitmap photo = Media.getBitmap(this.getContentResolver(), uri);
            if (photo != null) {
                this.mClipImageLayout.SetClipImage(photo);
            }
        } catch (FileNotFoundException var5) {
            var5.printStackTrace();
        } catch (IOException var6) {
            var6.printStackTrace();
        }

    }

    public boolean onCreateOptionsMenu(Menu menu) {
        this.getMenuInflater().inflate(2131099648, menu);
        return true;
    }

    public boolean onOptionsItemSelected(MenuItem item) {
        switch(item.getItemId()) {
            case 2131165185:
                Bitmap bitmap = this.mClipImageLayout.clip();
                Intent intent = new Intent();
                Uri uri = Uri.parse(Media.insertImage(this.getContentResolver(), bitmap, (String)null, (String)null));
                intent.setData(uri);
                this.setResult(6, intent);
                this.finish();
            default:
                return super.onOptionsItemSelected(item);
        }
    }
}
