using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Text;
using System.IO;

public class UnionCreatePanel : BasePlane
{

	private InputField unionNameInput;
	private Toggle[] typeToggles;
	private Text diamondText;
    private Text toggle1Name;
    private Text toggle2Name;
    private GameObject helpPanel;

    private Color color1 = new Color(255/255f, 193/255f, 37/255f);
    private Color color2 = new Color(171/255f, 171/255f, 171/255f);

	private int ptDiamond = 0;
	private int cjDiamond = 0;

	void Awake(){
		Button backBtn = transform.Find("ToggleGroup/Back/Share").GetComponent<Button>();
		backBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/backBtn");
            StopAllCoroutines();
            UnionManager.GetSingleton().planeManager.RemoveTopPlane();
        });

        unionNameInput = transform.Find("name").GetComponent<InputField>();
        diamondText = transform.Find("Coin/diamond1").GetComponent<Text>();

        typeToggles = transform.Find("type").GetComponentsInChildren<Toggle>();
        toggle1Name = transform.Find("type/Toggle1/Label").GetComponent<Text>();
        toggle2Name = transform.Find("type/Toggle2/Label").GetComponent<Text>();

        helpPanel = transform.Find("helpPanel").gameObject;
        helpPanel.SetActive(false);
        Button closeBtn = helpPanel.transform.Find("closeBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(()=>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            helpPanel.SetActive(false);
        });

        Button wenhaoBtn = transform.Find("type/wenhao").GetComponent<Button>();
        wenhaoBtn.onClick.AddListener(()=>{
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
            helpPanel.SetActive(true);
        });
        
        typeToggles[0].onValueChanged.AddListener(
            (bool s) =>
            {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                if(typeToggles[0].isOn){
                	diamondText.text = ptDiamond + "";
                    toggle1Name.color = color1;
                    toggle2Name.color = color2;   
                }
        	});

       	typeToggles[1].onValueChanged.AddListener(
            (bool s) =>
            {
                SoundManager.GetSingleton().PlaySound("Audio/clickBtn");
                if(typeToggles[1].isOn){
                	diamondText.text = cjDiamond + "";
                    toggle1Name.color = color2;
                    toggle2Name.color = color1;   
                }
        	});

        Button createBtn = transform.Find("createBtn").GetComponent<Button>();
		createBtn.onClick.AddListener(() => {
            SoundManager.GetSingleton().PlaySound("Audio/clickBtn");

            int type = 1;
            if(typeToggles[0].isOn){
            	type = 1;
            } else if(typeToggles[1].isOn){
            	type = 2;
            }

			NetMngr.GetSingleton().Send(InterfaceUnion.CreateUnion, 
				new object[] { UnionManager.GetSingleton().clubId, unionNameInput.text, type });
        });
       	
       	gameObject.SetActive(false);
	}

	public void SetDiamond(Hashtable data){
		ptDiamond = int.Parse(data["pt"].ToString());
		cjDiamond = int.Parse(data["cj"].ToString());

		if(typeToggles[0].isOn){
			diamondText.text = ptDiamond + "";
		} else if(typeToggles[1].isOn){
			diamondText.text = cjDiamond + "";
		}
	}


	void Update()
    {
        if (unionNameInput.text.Length >12)
        {
            unionNameInput.text = unionNameInput.text.Substring(0, 12);
        }
    }

	public override void OnAddComplete()
    {
        transform.Find("Coin/diamond2").GetComponent<Text>().text = StaticData.diamond + "";
    }

    public override void OnAddStart()
    {
        NetMngr.GetSingleton().Send(InterfaceUnion.GetCreateUnionDiamond, new object[] {});
    }

    public override void OnRemoveComplete()
    {
        unionNameInput.text = "";
    }

    public override void OnRemoveStart()
    {

    }

}