using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update() {
        GameObject.Find("mixamorig:Hips").transform.position = new Vector3(3f, 2f, 0f);
    }
}
