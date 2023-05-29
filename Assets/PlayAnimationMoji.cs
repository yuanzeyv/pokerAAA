using DragonBones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationMoji : MonoBehaviour {
    private DragonBones.UnityArmatureComponent unityArmature;//UnityArmatureComponent对象
                                                             // Use this for initialization
    public UnityDragonBonesData dragonBoneData;

    public void TestPlayAni()
    {
        Debug.Log("测试，点击导弹");
        UnityFactory.factory.LoadData(this.dragonBoneData);
        var armatureComponent = UnityFactory.factory.BuildArmatureComponent("Sprite", "daodanboom");
        armatureComponent.animation.Play("Sprite");
    }
    void Start () {
        Debug.Log("测试，点击导弹");
        UnityFactory.factory.LoadData(this.dragonBoneData);
        var armatureComponent = UnityFactory.factory.BuildArmatureComponent("Sprite", "daodanboom");
        armatureComponent.animation.Play("Sprite");
        //  unityArmature = GetComponent<DragonBones.UnityArmatureComponent>();//获得UnityArmatureComponent对象
        //   unityArmature.animation.Play("Sprite");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
