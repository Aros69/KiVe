using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealCharacterKiVe : MonoBehaviour
{

    private Transform headset = null;
    private Transform leftHand = null;
    private Transform rightHand = null;

    
    const int HEAD = 0, NECK = 1, RIGHT_SHOULDER = 2, RIGHT_ELBOW = 3, 
        RIGHT_HAND=4, LEFT_SHOULDER = 5, LEFT_ELBOW = 6, LEFT_HAND = 7,
        RIGHT_HIPS = 8, RIGHT_KNEE = 9, RIGHT_FOOT = 10, LEFT_HIPS = 11,
        LEFT_KNEE = 12, LEFT_FOOT = 13;

    private List<Transform> joints = new List<Transform>();

    private void getVRComponent()
    {
        // Search and hopefully find the CameraRig component of SteamVR plugin (the component should be at root of scene)
        Transform cameraRig = GameObject.Find("/[CameraRig]").transform;
        if(cameraRig == null){
            Debug.LogError("Can't find CameraRig (of the steamVR plugin)");
        } else {
            Debug.Log("CameraRig of steamVR found");
            headset = cameraRig.Find("Camera");
            leftHand = cameraRig.Find("Controller (left)");
            rightHand = cameraRig.Find("Controller (right)");
            if(leftHand == null){Debug.LogError("No left controller found");}
            if(rightHand == null){Debug.LogError("No right controller found");}
        }
    }

    public Transform getHeadset(){
        return headset;
    }

    public Transform getLeftHand(){
        return leftHand;
    }

    public Transform getRightHand(){
        return rightHand;
    }

    // Start is called before the first frame update
    void Start()
    {
        getVRComponent();

        // Get all transform we need for kiVe
        for(int i=0;i<14;++i){
            switch(i){
                case HEAD:              {joints.Add(GameObject.Find("mixamorig:Head").transform);           break;}
                case NECK:              {joints.Add(GameObject.Find("mixamorig:Neck").transform);           break;}
                case RIGHT_SHOULDER:    {joints.Add(GameObject.Find("mixamorig:RightShoulder").transform);  break;}
                case RIGHT_ELBOW:       {joints.Add(GameObject.Find("mixamorig:RightForeArm").transform);   break;}
                case RIGHT_HAND:        {joints.Add(GameObject.Find("mixamorig:RightHand").transform);      break;}
                case LEFT_SHOULDER:     {joints.Add(GameObject.Find("mixamorig:LeftForeArm").transform);    break;}
                case LEFT_ELBOW:        {joints.Add(GameObject.Find("mixamorig:RightShoulder").transform);  break;}
                case LEFT_HAND:         {joints.Add(GameObject.Find("mixamorig:LeftHand").transform);       break;}
                case RIGHT_HIPS:        {joints.Add(GameObject.Find("mixamorig:RightUpLeg").transform);     break;}
                case RIGHT_KNEE:        {joints.Add(GameObject.Find("mixamorig:RightLeg").transform);       break;}
                case RIGHT_FOOT:        {joints.Add(GameObject.Find("mixamorig:RightFoot").transform);      break;}
                case LEFT_HIPS:         {joints.Add(GameObject.Find("mixamorig:LeftUpLeg").transform);      break;}
                case LEFT_KNEE:         {joints.Add(GameObject.Find("mixamorig:LeftLeg").transform);        break;}
                case LEFT_FOOT:         {joints.Add(GameObject.Find("mixamorig:LeftFoot").transform);       break;}
                default : break;
            }
        }
    }

    private void asICanSkeletonUpdate(){
        joints[HEAD].transform.position = headset.position;  
        joints[LEFT_HAND].transform.position = leftHand.position;  
        joints[RIGHT_HAND].transform.position = rightHand.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(headset == null || leftHand == null || rightHand == null) {
            getVRComponent();
        }
        asICanSkeletonUpdate();
    }
}
