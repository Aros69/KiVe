using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour
{
    public SteamVR_Action_Single squeezeAction;

    public SteamVR_Action_Vector2 touchPadAction;

    private GameObject currentObj;
    private bool isChoosingItem;

    private int currentObjId = -1;

    private bool hasChangeItem(Vector2 touchPadValue){
        if(touchPadValue.x<0 && touchPadValue.y<0 && currentObjId == 1){
            return false;
        } else if(touchPadValue.x<0 && touchPadValue.y>0 && currentObjId == 2) {
            return false;
        } else if(touchPadValue.x>0 && touchPadValue.y<0 && currentObjId == 3){
            return false;
        } else if(touchPadValue.x>0 && touchPadValue.y>0 && currentObjId == 4){
            return false;
        } else  {
            return true;
        }
    }

    private void chooseItem(Vector2 touchPadValue) {
        if(touchPadValue.x<0 && touchPadValue.y<0){
            currentObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            currentObjId = 1;
        } else if(touchPadValue.x<0 && touchPadValue.y>0) {
            currentObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            currentObjId = 2;
        } else if(touchPadValue.x>0 && touchPadValue.y<0){
            currentObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            currentObjId = 3;
        } else if(touchPadValue.x>0 && touchPadValue.y>0){
            currentObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            currentObjId = 4;
        }
        currentObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        currentObj.transform.position = new Vector3(0, 2, 0);
        // TODO Maybe add script for item spawned during the game (concept of the game)
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 touchPadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);
        if(touchPadValue != Vector2.zero){
            //print(touchPadValue);
            if(!isChoosingItem) {
                isChoosingItem = true;
                chooseItem(touchPadValue);
            } else if(hasChangeItem(touchPadValue)) {
                Destroy(currentObj);
                chooseItem(touchPadValue);
            }
        } else {
            isChoosingItem = false;
            if(currentObj != null){
                Destroy(currentObj);
            }
        }

        float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.Any);
        if(triggerValue > 0f){
            Rigidbody t = currentObj.AddComponent<Rigidbody>();
            currentObj = null;
            //print(triggerValue);
        }
    }
}
