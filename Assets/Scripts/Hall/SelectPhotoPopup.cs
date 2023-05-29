using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;
using LitJson;
using System;
using UnityEngine.Networking;

public class SelectPhotoPopup : BasePlane {

    private Button photo;
    private Button album;
    private Button cancel;
    private Button mask;
    private string url = "";


    private void Awake()
    {
        photo = transform.Find("Photo").GetComponent<Button>();
        album = transform.Find("Album").GetComponent<Button>();
        cancel = transform.Find("Cancel").GetComponent<Button>();
        mask = transform.Find("Mask").GetComponent<Button>();

        mask.onClick.AddListener(() =>
        {
        	SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.RemoveSidePlane();
        });
        photo.onClick.AddListener(() =>
        {
        	SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().OpenCamera(url, StaticData.ID.ToString(), 1, "SelectPhotoPopup");
        });
        album.onClick.AddListener(() =>
        {
        	SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().OpenAlbum(url, StaticData.ID.ToString(), 1, "SelectPhotoPopup");
        });
        cancel.onClick.AddListener(() =>
        {
        	SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            HallManager.GetSingleton().planeManager.RemoveSidePlane();
        });
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }

	byte[] compressPng(Texture2D tex)
	{
		//缩略图(256*256)
		Texture2D texThumb = new Texture2D(64, 64);

		//压缩图片
		Color[] destPix = new Color[texThumb.width * texThumb.height];
		float warpFactor = 1.0f;
		int y = 0;
		while (y < texThumb.height)
		{
			int x = 0;
			while (x < texThumb.width)
			{
				float xFrac = x * 1.0F / (texThumb.width - 1);
				float yFrac = y * 1.0F / (texThumb.height - 1);
				float warpXFrac = Mathf.Pow(xFrac, warpFactor);
				float warpYFrac = Mathf.Pow(yFrac, warpFactor);
				Color c0 = tex.GetPixelBilinear(warpXFrac, warpYFrac);

				xFrac = (x - 1) * 1.0F / (texThumb.width - 1);
				yFrac = y * 1.0F / (texThumb.height - 1);
				warpXFrac = Mathf.Pow(xFrac, warpFactor);
				warpYFrac = Mathf.Pow(yFrac, warpFactor);
				Color c1 = tex.GetPixelBilinear(warpXFrac, warpYFrac);

				xFrac = (x + 1) * 1.0F / (texThumb.width - 1);
				yFrac = y * 1.0F / (texThumb.height - 1);
				warpXFrac = Mathf.Pow(xFrac, warpFactor);
				warpYFrac = Mathf.Pow(yFrac, warpFactor);
				Color c2 = tex.GetPixelBilinear(warpXFrac, warpYFrac);

				xFrac = (x + 1) * 1.0F / (texThumb.width - 1);
				yFrac = (y - 1) * 1.0F / (texThumb.height - 1);
				warpXFrac = Mathf.Pow(xFrac, warpFactor);
				warpYFrac = Mathf.Pow(yFrac, warpFactor);
				Color c3 = tex.GetPixelBilinear(warpXFrac, warpYFrac);

				xFrac = (x - 0) * 1.0F / (texThumb.width - 1);
				yFrac = (y - 1) * 1.0F / (texThumb.height - 1);
				warpXFrac = Mathf.Pow(xFrac, warpFactor);
				warpYFrac = Mathf.Pow(yFrac, warpFactor);
				Color c4 = tex.GetPixelBilinear(warpXFrac, warpYFrac);

				xFrac = (x - 1) * 1.0F / (texThumb.width - 1);
				yFrac = (y - 1) * 1.0F / (texThumb.height - 1);
				warpXFrac = Mathf.Pow(xFrac, warpFactor);
				warpYFrac = Mathf.Pow(yFrac, warpFactor);
				Color c5 = tex.GetPixelBilinear(warpXFrac, warpYFrac);

				xFrac = (x + 1) * 1.0F / (texThumb.width - 1);
				yFrac = (y + 1) * 1.0F / (texThumb.height - 1);
				warpXFrac = Mathf.Pow(xFrac, warpFactor);
				warpYFrac = Mathf.Pow(yFrac, warpFactor);
				Color c6 = tex.GetPixelBilinear(warpXFrac, warpYFrac);

				xFrac = (x - 0) * 1.0F / (texThumb.width - 1);
				yFrac = (y + 1) * 1.0F / (texThumb.height - 1);
				warpXFrac = Mathf.Pow(xFrac, warpFactor);
				warpYFrac = Mathf.Pow(yFrac, warpFactor);
				Color c7 = tex.GetPixelBilinear(warpXFrac, warpYFrac);

				xFrac = (x - 1) * 1.0F / (texThumb.width - 1);
				yFrac = (y + 1) * 1.0F / (texThumb.height - 1);
				warpXFrac = Mathf.Pow(xFrac, warpFactor);
				warpYFrac = Mathf.Pow(yFrac, warpFactor);
				Color c8 = tex.GetPixelBilinear(warpXFrac, warpYFrac);

				Color cr = c0 * 0.25f + c1 * 0.125f + c2 * 0.125f + c3 * 0.0625f + c4 * 0.125f + c5 * 0.0625f + c6 * 0.0625f + c7 * 0.125f + c8 * 0.0625f;

				destPix[y * texThumb.width + x] = cr;

				x++;
			}
			y++;
		}
		texThumb.SetPixels(destPix);
		texThumb.Apply();
		byte[] bytesThumb = texThumb.EncodeToPNG();

		Destroy(tex);
		Destroy(texThumb);

		return bytesThumb;
	}

    public void SetRequestUrl(Hashtable data)
    {
        if (data["success"].ToString() == "1")
        {
            //上传图片
            url = data["url"].ToString();
        }
        else if (data["success"].ToString() == "0")
        {
            Debug.Log("获取服务器文件地址失败");
        }
    }

    public override void OnAddComplete()
    {
        NetMngr.GetSingleton().Send(InterfaceMain.RequestUpDataUrl, new object[] { });
    }

    public override void OnAddStart()
    {
        
    }

    public override void OnRemoveComplete()
    {

    }

    public override void OnRemoveStart()
    {

    }
    public void AndroidMessage(string imgPath)
    {
        HallManager.GetSingleton().AndroidMessage(imgPath);
    }

}
