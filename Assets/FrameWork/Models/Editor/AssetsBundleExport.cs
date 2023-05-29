using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class AssetsBundleExport : Editor {

    [MenuItem("FrameWork/Export AssetsBundle/Platform/WIN64")]
    public static void ExporWIN64AB()
    {
        string outputPath = Application.persistentDataPath + "/WIN64/";

        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        BuildPipeline.BuildAssetBundles(outputPath,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows64);

        Debug.Log("导出WIN64平台AB完成，目录: " + outputPath);
    }

    [MenuItem("FrameWork/Export AssetsBundle/Platform/WIN")]
    public static void ExporWINAB()
    {
        string outputPath = Application.persistentDataPath + "/WIN/";

        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        BuildPipeline.BuildAssetBundles(outputPath,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows);

        Debug.Log("导出WIN平台AB完成，目录: " + outputPath);
    }

    [MenuItem("FrameWork/Export AssetsBundle/Platform/Android")]
    public static void ExportAndroidAB()
    {
        string outputPath = Application.persistentDataPath + "/Android/";

        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        BuildPipeline.BuildAssetBundles(outputPath,
            BuildAssetBundleOptions.None,
            BuildTarget.Android);

        Debug.Log("导出Android平台AB完成，目录: " + outputPath);
    }

    [MenuItem("FrameWork/Export AssetsBundle/Platform/IOS")]
    public static void ExportIOSAB()
    {
        string outputPath = Application.persistentDataPath + "/IOS/";

        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        BuildPipeline.BuildAssetBundles(outputPath,
            BuildAssetBundleOptions.None,
            BuildTarget.iOS);

        Debug.Log("导出IOS平台AB完成，目录: " + outputPath);
    }
}
