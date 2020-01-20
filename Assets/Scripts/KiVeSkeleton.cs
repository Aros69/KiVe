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
        bone.transform.position = endJoint.position - startJoint.position;
        bone.transform.LookAt(endJoint.position);
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
        }
        Debug.Log(joints.Count);
        // TODO better code because it's irreadable:( 
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
        for(int i=0;i<14;++i){
            if(i == 0) {
                joints[i].transform.position = headset.position;  
            } else if(i==4){
                joints[i].transform.position = leftHand.position;  
            } else if(i==7){
                joints[i].transform.position = rightHand.position;  
            }
        }
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
