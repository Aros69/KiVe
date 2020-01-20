using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibrator : MonoBehaviour
{
    Matrix4x4 kiveMat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Compute the transformation matrix
    void computeKive(Vector3 leftCtrl, Vector3 rightCtrl, Vector3 headset,
         Vector3 leftHand, Vector3 rightHand, Vector3 head)
    {
        Matrix4x4 posKiMat = Matrix4x4.identity;
        Matrix4x4 posViveMat = Matrix4x4.identity;

        posKiMat.SetRow(0, new Vector4(leftHand.x, leftHand.y, leftHand.z, 0));
        posKiMat.SetRow(1, new Vector4(rightHand.x, rightHand.y, rightHand.z, 0));
        posKiMat.SetRow(2, new Vector4(head.x, head.y, head.z, 0));

        posViveMat.SetRow(0, new Vector4(leftCtrl.x, leftCtrl.y, leftCtrl.z, 0));
        posViveMat.SetRow(1, new Vector4(rightCtrl.x, rightCtrl.y, rightCtrl.z, 0));
        posViveMat.SetRow(2, new Vector4(headset.x, headset.y, headset.z, 0));

        kiveMat = posViveMat * posViveMat.inverse;
    }

    // Return the joint position in Vive coordinates
    Vector3 kinect2VivePos(Vector3 jointPos)
    {
        return kiveMat.MultiplyVector(jointPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
