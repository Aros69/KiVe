using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoreGameplay : MonoBehaviour
{
    [SerializeField]
    private GameObject capsuleTarget;
    [SerializeField]
    private GameObject cylinderTarget;
    [SerializeField]
    private GameObject cubeTarget;
    [SerializeField]
    private GameObject sphereTarget;

    TextMeshPro text;

    // Start is called before the first frame update
     void Start()
    {
        text = (TextMeshPro)FindObjectOfType(typeof(TextMeshPro));
        
        //TextMeshPro mText = gameObject.GetComponent<TextMeshPro>();
    }

    public int getScore(){
        return capsuleTarget.GetComponent<BasicTarget>().score + 
            cubeTarget.GetComponent<BasicTarget>().score +
            cylinderTarget.GetComponent<BasicTarget>().score +
            sphereTarget.GetComponent<BasicTarget>().score; 
    }
    // Update is called once per frame
    void Update()
    {
        text.SetText("Score : "+ getScore());
    }
}
