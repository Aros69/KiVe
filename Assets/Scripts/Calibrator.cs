using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibrator
{
    public Matrix4x4 kiveMat;

    private Vector3 m_headset, m_leftCtrl, m_rightCtrl, m_head, m_leftHand, m_rightHand;

    private float m_unitKi = 0.0f, m_unitVi = 0.0f;
    // Compute the transformation matrix
    public void computeKive(Vector3 leftCtrl, Vector3 rightCtrl, Transform headset,
         Vector3 leftHand, Vector3 rightHand, Vector3 head, float kinectSkeletonHeight)
    {
        m_leftCtrl = leftCtrl; m_rightCtrl = rightCtrl; m_headset = headset.position;
        m_head = head; m_leftHand = leftHand; m_rightHand = rightHand;

        Matrix4x4 posKiMat = Matrix4x4.identity;
        Matrix4x4 posViveMat = Matrix4x4.identity;

        posKiMat.SetRow(0, new Vector4(leftHand.x, leftHand.y, leftHand.z, 1));
        posKiMat.SetRow(1, new Vector4(rightHand.x, rightHand.y, rightHand.z, 1));
        posKiMat.SetRow(2, new Vector4(head.x, head.y, head.z, 1));
        // posKiMat.SetRow(3, new Vector4(head.x, head.y - kinectSkeletonHeight, head.z + Vector3.Distance(rightHand, head), 1));

        posViveMat.SetRow(0, new Vector4(leftCtrl.x, leftCtrl.y, leftCtrl.z, 1));
        posViveMat.SetRow(1, new Vector4(rightCtrl.x, rightCtrl.y, rightCtrl.z, 1));
        posViveMat.SetRow(2, new Vector4(headset.position.x, headset.position.y, headset.position.z, 1));
        // posViveMat.SetRow(3, new Vector4(headset.position.x, headset.position.y - headset.localPosition.y, headset.position.z + Vector3.Distance(rightCtrl, headset.position), 1));

        /* posKiMat.SetColumn(0, new Vector4(leftHand.x, leftHand.y, leftHand.z, 0));
        posKiMat.SetColumn(1, new Vector4(rightHand.x, rightHand.y, rightHand.z, 0));
        posKiMat.SetColumn(2, new Vector4(head.x, head.y, head.z, 0));

        posViveMat.SetColumn(0, new Vector4(leftCtrl.x, leftCtrl.y, leftCtrl.z, 0));
        posViveMat.SetColumn(1, new Vector4(rightCtrl.x, rightCtrl.y, rightCtrl.z, 0));
        posViveMat.SetColumn(2, new Vector4(headset.x, headset.y, headset.z, 0));*/

        kiveMat = posKiMat.inverse * posViveMat;
        //kiveMat = posViveMat * posKiMat.inverse;
    }

    // Return the joint position in Vive coordinates
    public Vector3 kinect2VivePos(Vector3 jointPos)
    {
        return kiveMat.MultiplyVector(jointPos);
    }

    public Vector3 getViveJoint(Vector3 joint)
    {
        // float unitKi = Vector3.Distance(m_head, m_rightHand);
        // float unitVi = Vector3.Distance(m_headset, m_rightCtrl);

        if(Vector3.Distance(m_head, m_rightHand) > m_unitKi)
            m_unitKi = Vector3.Distance(m_head, m_rightHand);
        if(Vector3.Distance(m_head, m_leftHand) > m_unitKi)
            m_unitKi = Vector3.Distance(m_head, m_leftHand);

        if(Vector3.Distance(m_headset, m_rightCtrl) > m_unitVi)
            m_unitVi = Vector3.Distance(m_headset, m_rightCtrl);
        if(Vector3.Distance(m_headset, m_leftCtrl) > m_unitVi)
            m_unitVi = Vector3.Distance(m_headset, m_leftCtrl);

        // Recuperer les distances
        float headset2Joint = (Vector3.Distance(joint, m_head) / m_unitKi) * m_unitVi; // Depend de head
        float rightCtrl2Joint = (Vector3.Distance(joint, m_rightHand) / m_unitKi) * m_unitVi;; // Depend de right
        float leftCtrl2Joint = (Vector3.Distance(joint, m_leftCtrl) / m_unitKi) * m_unitVi;;  // Depend de left
        // Recuperer les directions
        Vector3 headset2JointV = (-m_head + joint).normalized; // Depend de headset
        Vector3 rightCtrl2JointV = (-m_rightHand + joint).normalized; // Depend de rightCtrl
        Vector3 leftCtrl2JointV = (-m_leftHand + joint).normalized;  // Depend de leftCtrl 
        // Recuperer la somme ponderee

        return ((m_rightCtrl + rightCtrl2JointV * rightCtrl2Joint) +
            (m_leftCtrl + leftCtrl2JointV * leftCtrl2Joint) +
            (m_headset + headset2JointV * headset2Joint))/3.0f;
    }
}
