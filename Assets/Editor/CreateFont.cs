using UnityEngine;
using UnityEditor;
using System.IO;
 
public class CreateFont : EditorWindow
{
	[MenuItem("Tools/CreateFont(sprite)")]
    public static void Open()
    {
		GetWindow<CreateFont>("CreateFont");
    }
 
    private Texture2D tex;
    private string fontName;
    private string fontPath;
 
    private void OnGUI()
    {
        GUILayout.BeginVertical();
 
        GUILayout.BeginHorizontal();
        GUILayout.Label("FontImage:");
        tex = (Texture2D)EditorGUILayout.ObjectField(tex, typeof(Texture2D), true);
        GUILayout.EndHorizontal();
 
        GUILayout.BeginHorizontal();
		GUILayout.Label("FontName:");
        fontName = EditorGUILayout.TextField(fontName);
        GUILayout.EndHorizontal();
 
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(string.IsNullOrEmpty(fontPath) ? "SelectPath" : fontPath))
        {
            fontPath = EditorUtility.OpenFolderPanel("FontPath", Application.dataPath, "");
            if (string.IsNullOrEmpty(fontPath))
            {
				Debug.Log("UnSelectPath");
            }
            else
            {
                fontPath = fontPath.Replace(Application.dataPath, "") + "/";
            }
        }
        GUILayout.EndHorizontal();
 
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            Create();
        }
        GUILayout.EndHorizontal();
 
        GUILayout.EndVertical();
    }
 
    private void Create()
    {
        if (tex == null)
        {
			Debug.LogWarning("Failed to create, Image is null");
            return;
        }
 
        if (string.IsNullOrEmpty(fontPath))
        {
			Debug.LogWarning("FontPath is null");
            return;
        }
        if (fontName == null)
        {
			Debug.LogWarning("Failed to create, FontName is null");
            return;
        }
        else
        {
            if (File.Exists(Application.dataPath + fontPath + fontName + ".fontsettings"))
            {
				Debug.LogError("Failed to create, A font file with the same name already exists");
                return;
            }
            if (File.Exists(Application.dataPath + fontPath + fontName + ".mat"))
            {
				Debug.LogError("Failed to create. A material file with the same name already exists");
                return;
            }
        }
 
        string selectionPath = AssetDatabase.GetAssetPath(tex);
        if (selectionPath.Contains("/Resources/"))
        {
            string selectionExt = Path.GetExtension(selectionPath);
            if (selectionExt.Length == 0)
            {
				Debug.LogError("Failed to create");
                return;
            }
            
            string fontPathName = fontPath + fontName + ".fontsettings";
            string matPathName = fontPath + fontName + ".mat";
            float lineSpace = 0.1f;
            //string loadPath = selectionPath.Remove(selectionPath.Length - selectionExt.Length).Replace("Assets/Resources/", "");
            string loadPath = selectionPath.Replace(selectionExt, "").Substring(selectionPath.IndexOf("/Resources/") + "/Resources/".Length);
            Sprite[] sprites = Resources.LoadAll<Sprite>(loadPath);
            if (sprites.Length > 0)
            {
                Material mat = new Material(Shader.Find("GUI/Text Shader"));
                mat.SetTexture("_MainTex", tex);
                Font m_myFont = new Font();
                m_myFont.material = mat;
                CharacterInfo[] characterInfo = new CharacterInfo[sprites.Length];
                for (int i = 0; i < sprites.Length; i++)
                {
                    if (sprites[i].rect.height > lineSpace)
                    {
                        lineSpace = sprites[i].rect.height;
                    }
                }
                for (int i = 0; i < sprites.Length; i++)
                {
                    Sprite spr = sprites[i];
                    CharacterInfo info = new CharacterInfo();
                    try
                    {
                        info.index = System.Convert.ToInt32(spr.name);
                    }
                    catch
                    {
						Debug.LogError("Failed to create£¬Sprite name error");
                        return;
                    }
                    Rect rect = spr.rect;
                    float pivot = spr.pivot.y / rect.height - 0.5f;
                    if (pivot > 0)
                    {
                        pivot = -lineSpace / 2 - spr.pivot.y;
                    }
                    else if (pivot < 0)
                    {
                        pivot = -lineSpace / 2 + rect.height - spr.pivot.y;
                    }
                    else
                    {
                        pivot = -lineSpace / 2;
                    }
                    int offsetY = (int)(pivot + (lineSpace - rect.height) / 2);
                    info.uvBottomLeft = new Vector2((float)rect.x / tex.width, (float)(rect.y) / tex.height);
                    info.uvBottomRight = new Vector2((float)(rect.x + rect.width) / tex.width, (float)(rect.y) / tex.height);
                    info.uvTopLeft = new Vector2((float)rect.x / tex.width, (float)(rect.y + rect.height) / tex.height);
                    info.uvTopRight = new Vector2((float)(rect.x + rect.width) / tex.width, (float)(rect.y + rect.height) / tex.height);
                    info.minX = 0;
                    info.minY = -(int)rect.height - offsetY;
                    info.maxX = (int)rect.width;
                    info.maxY = -offsetY;
                    info.advance = (int)rect.width;
                    characterInfo[i] = info;
                }
                AssetDatabase.CreateAsset(mat, "Assets" + matPathName);
                AssetDatabase.CreateAsset(m_myFont, "Assets" + fontPathName);
                m_myFont.characterInfo = characterInfo;
                EditorUtility.SetDirty(m_myFont);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();//Ë¢ÐÂ×ÊÔ´
				Debug.Log("Font creation successful");
 
            }
            else
            {
				Debug.LogError("Atlas error");
            }
        }
        else
        {
			Debug.LogError("Failed to create. The selected image is not in the Resources folder");
        }
    }
}