using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone{
    private Transform startJoint = null;
    private Transform endJoint = null;
    private GameObject bone = null;

    public Bone(Transform start, Transform end){
        startJoint = start;
        endJoint = end;
        bone = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        bone.transform.localScale = new Vector3(0.1f, 1f, 0.1f);
    }

    public void update(){
        bone.transform.position = (startJoint.position + endJoint.position )/2;
        bone.transform.up = startJoint.position - endJoint.position;
        bone.transform.localScale = new Vector3(bone.transform.localScale.x, 
            Vector3.Distance(startJoint.position, endJoint.position)/2,
            bone.transform.localScale.z);
        /*bone.transform.position = endJoint.position - startJoint.position;
        bone.transform.LookAt(endJoint.position);*/
    }
}


/****************
Joints structure :
    0
    |
  2_1_5
 /|   |\
3 |   | 6
| |   | |
4 |   | 7
  8___11
  |   |
  9   12
  |   |
  10  13

Bones structure :

     *
     0
  *1_*_2*
 3 |   | 6
*  7   10 *
3  |   |  6
*  |   |  *
   *13_*  
   8   11
   *   *
   9   12  

****************/
public class KiVeSkeleton : MonoBehaviour
{
    private Transform headset = null;
    private Transform leftHand = null;
    private Transform rightHand = null;
    //private KinectVRPN kinect = null;

    const int HEAD = 0, NECK = 1, RIGHT_SHOULDER = 2, RIGHT_ELBOW = 3, 
        RIGHT_HAND=4, LEFT_SHOULDER = 5, LEFT_ELBOW = 6, LEFT_HAND = 7,
        RIGHT_HIPS = 8, RIGHT_KNEE = 9, RIGHT_FOOT = 10, LEFT_HIPS = 11,
        LEFT_KNEE = 12, LEFT_FOOT = 13;

    private List<GameObject> joints = new List<GameObject>();
    private List<Bone> bones = new List<Bone>();

    private void getVRComponent(){
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

    // TODO
    private void getKinectVRPN(){}

    // Start is called before the first frame update
    void Start()
    {
        getVRComponent();   
        //getKinectVRPN();

        for(int i=0;i<14;++i){
            joints.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            joints[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            switch(i){
                case HEAD:              {joints[i].name = "Head";           break;}
                case NECK:              {joints[i].name = "Neck";           break;}
                case RIGHT_SHOULDER:    {joints[i].name = "Right Shoulder"; break;}
                case RIGHT_ELBOW:       {joints[i].name = "Right Elbow";    break;}
                case RIGHT_HAND:        {joints[i].name = "Right Hand";     break;}
                case LEFT_SHOULDER:     {joints[i].name = "Left Shoulder";  break;}
                case LEFT_ELBOW:        {joints[i].name = "Left Elbow";     break;}
                case LEFT_HAND:         {joints[i].name = "Left Hand";      break;}
                case RIGHT_HIPS:        {joints[i].name = "Right Hip";      break;}
                case RIGHT_KNEE:        {joints[i].name = "Right Knee";     break;}
                case RIGHT_FOOT:        {joints[i].name = "Right Foot";     break;}
                case LEFT_HIPS:         {joints[i].name = "Left Hip";       break;}
                case LEFT_KNEE:         {joints[i].name = "Left Knee";      break;}
                case LEFT_FOOT:         {joints[i].name = "Left Foot";      break;}
                default : break;
            }
        }
        //Debug.Log(joints.Count);
        // TODO better code because it's irreadable :( 
        // or leave it that way, it works

        // TODO Erreur !! les deux épaules ont l'air d'être lié à la tête et pas au bas du coup !!!!
        // A CORRIGER !!!!!

        for(int i=0;i<14;++i){
            if(i==4){
                bones.Add(new Bone(joints[1].transform, joints[i+1].transform));
            } else if(i==7){
                bones.Add(new Bone(joints[2].transform, joints[i+1].transform));
            } else if(i==10){
                bones.Add(new Bone(joints[5].transform, joints[i+1].transform));
            } else if(i==13){
                bones.Add(new Bone(joints[8].transform, joints[11].transform));
            } else {
                bones.Add(new Bone(joints[i].transform, joints[i+1].transform));
            }
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

    private void asICanSkeletonUpdate(){
            joints[HEAD].transform.position = headset.position;  
            joints[LEFT_HAND].transform.position = leftHand.position;  
            joints[RIGHT_HAND].transform.position = rightHand.position;

            Transform cameraRig = GameObject.Find("/[CameraRig]").transform;
            Vector3 playerPos = headset.position;
            float playerHeigth = headset.localPosition.y;
            joints[LEFT_FOOT].transform.position        = playerPos + new Vector3(0.3f, 0, 0);
            joints[RIGHT_FOOT].transform.position       = playerPos + new Vector3(-0.3f, 0, 0);
            joints[LEFT_KNEE].transform.position        = playerPos + new Vector3(0.3f, -3*playerHeigth/4, 0);
            joints[RIGHT_KNEE].transform.position       = playerPos + new Vector3(-0.3f, -3*playerHeigth/4, 0);
            joints[LEFT_HIPS].transform.position        = playerPos + new Vector3(0.3f, -2*playerHeigth/4, 0);
            joints[RIGHT_HIPS].transform.position       = playerPos + new Vector3(-0.3f, -2*playerHeigth/4, 0);
            joints[LEFT_SHOULDER].transform.position    = playerPos + new Vector3(0.3f, -playerHeigth/4, 0);
            joints[RIGHT_SHOULDER].transform.position   = playerPos + new Vector3(-0.3f, -playerHeigth/4, 0);
            joints[NECK].transform.position             = playerPos + new Vector3(0, -playerHeigth/4, 0);
            joints[LEFT_ELBOW].transform.position       = (leftHand.position + joints[LEFT_SHOULDER].transform.position)/2;
            joints[RIGHT_ELBOW].transform.position      = (rightHand.position + joints[RIGHT_SHOULDER].transform.position)/2;

        for(int i=0;i<14;++i){
            bones[i].update();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(headset == null || leftHand == null || rightHand == null) {
            getVRComponent();
        }
        //if(kinect == null){getKinectVRPN();}

        //if(kinect == null){
            asICanSkeletonUpdate();
        //} else {
            // kinectSkeletonUpdate()
        //}
        
    }
}
