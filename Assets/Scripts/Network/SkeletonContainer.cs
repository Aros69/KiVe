using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SkeletonContainer : NetworkBehaviour
{
    /// <summary>
    /// Contains the data of a joint specified by the vrpn
    /// </summary>
    public struct Kine_joint
    {
        public Vector3 position;
        public Quaternion rotation;
    }
    
    [SerializeField] private GameObject skeletonContainerObj;
    [SerializeField] private GameObject UVRPN_Manager;
    public bool m_debug = true;
    public List<Transform> m_skeleton;
    private List<Kine_joint> m_Kine_joints;
    public List<Kine_joint> GetJoints()
    {
        return m_Kine_joints;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(isServer && !isLocalPlayer)
            Destroy(gameObject);
        if(isServer)
        {
            InitJointsFromSkeleton();
            Debug.Log("Number of Joints in the skeleton : " + m_skeleton.Count);
        }
        else // If the object is not the server, we want to disable de UVRPN
        {
            skeletonContainerObj.GetComponent<ShowKiSkeleton>().DisableVRPN();
            UVRPN_Manager.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isServer) // Update the position of all the joints
        {
        }
        else if(isLocalPlayer)
            return;
        else if(m_debug)
        {
        }
        UpdateJoints();
    }

    /// <summary>
    /// Search for all the joints present in the gameobject and store their references in m_skeleton
    /// Also initialize the list of the actual joints
    /// </summary>
    public void InitJointsFromSkeleton()
    {
        m_Kine_joints = new List<Kine_joint>();
        foreach(Transform child in skeletonContainerObj.transform)
        {
            m_skeleton.Add(child);
            Kine_joint j = new Kine_joint(); j.position = Vector3.zero; j.rotation = Quaternion.identity;
            m_Kine_joints.Add(j);
        }
    }
    
    public void UpdateJoints()
    {
        for(int i = 0; i < m_skeleton.Count; i++)
        {
            Kine_joint j = new Kine_joint(); j.position = m_skeleton[i].position; j.rotation = m_skeleton[i].rotation;
            m_Kine_joints[i] = j;
        }
    }
}
