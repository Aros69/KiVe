using Valve.VR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean showHeadPositionAction;
    public SteamVR_Action_Boolean showHandPositionAction;
    /*private bool isShowingHeadPosition;
    private bool isShowingLeftHandPosition;
    private bool isShowingRightHandPosition;*/

    private KiVeSkeleton playerSkeleton = null;

    void Start() {
        playerSkeleton = GameObject.Find("/Player").GetComponent<KiVeSkeleton>();
        if(playerSkeleton == null){Debug.LogError("Can't find player object");}
    }

    // Update is called once per frame
    void Update() {
        if (GetHeadShowPosition()){
            print("Show Head Position : "+playerSkeleton.getHeadset().position);
        }
        if (GetHandShowPosition()){
            if(handType == SteamVR_Input_Sources.RightHand){
                print("Show Position "+handType+" : "+playerSkeleton.getRightHand().position);
            } else {
                print("Show Position "+handType+" : "+playerSkeleton.getLeftHand().position);
            }
        }


    }

    public bool GetHeadShowPosition() {
        return showHeadPositionAction.GetState(handType);
    }

    public bool GetHandShowPosition() {
        return showHandPositionAction.GetState(handType);
    }


}
