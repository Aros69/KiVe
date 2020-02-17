using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UVRPN.Core;
public class ShowKiSkeleton : MonoBehaviour
{
    [SerializeField] private bool m_showSkeleton = true;

    [SerializeField] private float scale = 2.0f;
    
    public void DisableVRPN()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<VRPN_Tracker>().enabled = false;
        }
    }

    public void SetScale()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<VRPN_Tracker>().scale = scale;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetScale();
        if(!m_showSkeleton)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<MeshRenderer>().enabled = false;
            }

        }else
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
