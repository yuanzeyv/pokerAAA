//
// Source code recreated from a .class file by IntelliJ IDEA
// (powered by Fernflower decompiler)
//

package com.unity.InvokeAlbum;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Paint.Style;
import android.util.AttributeSet;
import android.util.TypedValue;
import android.view.View;

public class ClipImageBorderView extends View {
    private int mHorizontalPadding;
    private int mVerticalPadding;
    private int mWidth;
    private int mBorderColor;
    private int mBorderWidth;
    private Paint mPaint;

    public ClipImageBorderView(Context context) {
        this(context, (AttributeSet)null);
    }

    public ClipImageBorderView(Context context, AttributeSet attrs) {
        this(context, attrs, 0);
    }

    public ClipImageBorderView(Context context, AttributeSet attrs, int defStyle) {
        super(context, attrs, defStyle);
        this.mBorderColor = Color.parseColor("#FFFFFF");
        this.mBorderWidth = 1;
        this.mBorderWidth = (int)TypedValue.applyDimension(1, (float)this.mBorderWidth, this.getResources().getDisplayMetrics());
        this.mPaint = new Paint();
        this.mPaint.setAntiAlias(true);
    }

    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        this.mWidth = this.getWidth() - 2 * this.mHorizontalPadding;
        this.mVerticalPadding = (this.getHeight() - this.mWidth) / 2;
        this.mPaint.setColor(Color.parseColor("#aa000000"));
        this.mPaint.setStyle(Style.FILL);
        canvas.drawRect(0.0F, 0.0F, (float)this.mHorizontalPadding, (float)this.getHeight(), this.mPaint);
        canvas.drawRect((float)(this.getWidth() - this.mHorizontalPadding), 0.0F, (float)this.getWidth(), (float)this.getHeight(), this.mPaint);
        canvas.drawRect((float)this.mHorizontalPadding, 0.0F, (float)(this.getWidth() - this.mHorizontalPadding), (float)this.mVerticalPadding, this.mPaint);
        canvas.drawRect((float)this.mHorizontalPadding, (float)(this.getHeight() - this.mVerticalPadding), (float)(this.getWidth() - this.mHorizontalPadding), (float)this.getHeight(), this.mPaint);
        this.mPaint.setColor(this.mBorderColor);
        this.mPaint.setStrokeWidth((float)this.mBorderWidth);
        this.mPaint.setStyle(Style.STROKE);
        canvas.drawRect((float)this.mHorizontalPadding, (float)this.mVerticalPadding, (float)(this.getWidth() - this.mHorizontalPadding), (float)(this.getHeight() - this.mVerticalPadding), this.mPaint);
    }

    public void setHorizontalPadding(int mHorizontalPadding) {
        this.mHorizontalPadding = mHorizontalPadding;
    }
}
