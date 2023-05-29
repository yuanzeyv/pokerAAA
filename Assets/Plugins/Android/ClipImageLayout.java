//
// Source code recreated from a .class file by IntelliJ IDEA
// (powered by Fernflower decompiler)
//

package com.unity.InvokeAlbum;

import android.content.Context;
import android.graphics.Bitmap;
import android.util.AttributeSet;
import android.util.TypedValue;
import android.view.ViewGroup.LayoutParams;
import android.widget.RelativeLayout;

public class ClipImageLayout extends RelativeLayout {
    private ClipZoomImageView mZoomImageView;
    private ClipImageBorderView mClipImageView;
    private int mHorizontalPadding = 20;

    public ClipImageLayout(Context context, AttributeSet attrs) {
        super(context, attrs);
        this.mZoomImageView = new ClipZoomImageView(context);
        this.mClipImageView = new ClipImageBorderView(context);
        LayoutParams lp = new android.widget.RelativeLayout.LayoutParams(-1, -1);
        this.mZoomImageView.setImageDrawable(this.getResources().getDrawable(2130837505));
        this.addView(this.mZoomImageView, lp);
        this.addView(this.mClipImageView, lp);
        this.mHorizontalPadding = (int)TypedValue.applyDimension(1, (float)this.mHorizontalPadding, this.getResources().getDisplayMetrics());
        this.mZoomImageView.setHorizontalPadding(this.mHorizontalPadding);
        this.mClipImageView.setHorizontalPadding(this.mHorizontalPadding);
    }

    public void SetClipImage(Bitmap photo) {
        this.mZoomImageView.setImageBitmap(photo);
    }

    public void setHorizontalPadding(int mHorizontalPadding) {
        this.mHorizontalPadding = mHorizontalPadding;
    }

    public Bitmap clip() {
        return this.mZoomImageView.clip();
    }
}
