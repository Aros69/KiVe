using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTarget : MonoBehaviour
{
    [SerializeField] private string projectileName; 
    public int score = 0;

    public void OnTriggerEnter(Collider other){
        //Debug.Log("Je suis dans le trigger");
        if(!other.name.Contains("Player"))
        {
            if(other.name.Contains(projectileName)){
                //Debug.Log("Victoire");
                score++;
            } else {
                //Debug.Log("Defaite");
                score--;
            }
            Destroy(other.gameObject);
        }
        //Destroy(objectCollided.parent);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
