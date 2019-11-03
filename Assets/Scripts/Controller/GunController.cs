using Pvr_UnitySDKAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    public PicoUI picoUI;

    private void Update()
    {
        if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TOUCHPAD) ||
                Controller.UPvr_GetKeyDown(1, Pvr_KeyCode.TOUCHPAD))
        {
            picoUI.SetText("TOUCHPAD");
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            picoUI.SetText("JoystickButton0");
        }


    }
}
