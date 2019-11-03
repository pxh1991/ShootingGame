using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pvr_UnitySDKAPI;

namespace ShootCartoon
{
    public class HandControllerModuleInit : MonoBehaviour
    {
        public ControllerVariety variety;

        [SerializeField]
        private GameObject hand;
        [SerializeField]
        private GameObject rayLine;
        [SerializeField]
        private GameObject aim;

        private int mainHand = 0;
        private bool moduleState = true;

        void Awake()
        {
            Pvr_ControllerManager.PvrServiceStartSuccessEvent += ServiceStartSuccess;
            Pvr_ControllerManager.SetControllerAbilityEvent += CheckControllerStateOfAbility;
            Pvr_ControllerManager.ControllerStatusChangeEvent += CheckControllerStateForGoblin;

            if (Pvr_ControllerManager.Instance.LengthAdaptiveRay)
            {
                rayLine = transform.GetComponentInChildren<LineRenderer>(true).gameObject;
#if UNITY_2017_1_OR_NEWER
                rayLine.GetComponent<LineRenderer>().startWidth = 0.003f;
                rayLine.GetComponent<LineRenderer>().endWidth = 0.0015f;
#else
            rayLine.GetComponent<LineRenderer>().SetWidth(0.003f, 0.0015f);
#endif
            }
        }

        void OnDestroy()
        {
            Pvr_ControllerManager.PvrServiceStartSuccessEvent -= ServiceStartSuccess;
            Pvr_ControllerManager.SetControllerAbilityEvent -= CheckControllerStateOfAbility;
            Pvr_ControllerManager.ControllerStatusChangeEvent -= CheckControllerStateForGoblin;
        }

        private void ServiceStartSuccess()
        {
            mainHand = Controller.UPvr_GetMainHandNess();
            if (Pvr_ControllerManager.controllerlink.controller0Connected ||
                Pvr_ControllerManager.controllerlink.controller1Connected)
            {
                moduleState = true;
                hand.transform.localScale = Vector3.one;
            }
            if (variety == ControllerVariety.Controller0)
            {
                StartCoroutine(ShowAndHideRay(mainHand == 0 && Pvr_ControllerManager.controllerlink.controller0Connected));

            }
            if (variety == ControllerVariety.Controller1)
            {
                StartCoroutine(ShowAndHideRay(mainHand == 1 && Pvr_ControllerManager.controllerlink.controller1Connected));
            }
        }

        private void CheckControllerStateForGoblin(string state)
        {

        }

        private void CheckControllerStateOfAbility(string data)
        {

        }

        private IEnumerator ShowAndHideRay(bool state)
        {
            yield return null;
            yield return null;
            if (moduleState)
            {
                aim.SetActive(state);
                rayLine.SetActive(state);
            }
        }

    }

}
