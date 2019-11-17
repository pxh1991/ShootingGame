using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting.Content;
using System;
using Pvr_UnitySDKAPI;
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            AssetsManager.Instance.CreateAssetRondom(InitGameObject);
        }
        if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TOUCHPAD) ||
                Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TOUCHPAD))
        {
            print("space key was pressed");
            AssetsManager.Instance.CreateAssetRondom(InitGameObject);
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            print("space key was pressed");
            AssetsManager.Instance.CreateAssetRondom(InitGameObject);
        }
    }

    

    private void InitGameObject(GameObject go,Character chara)
    {
        if(go == null)
            return;
        go.transform.position = new Vector3(UnityEngine.Random.Range(0f,10f),0f,0f);
    }
}
