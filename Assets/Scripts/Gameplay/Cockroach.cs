using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cockroach : MonoBehaviour
{
    public Transform player = null;

    public GameObject explosion = null;
    public float speed = 0.1f;
    private Rigidbody m_rigidbody = null;
    private Vector3 m_target = Vector3.zero;

    public static int squishedRoaches;

    private bool moving = true;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        // Search for the player
        player =  GameObject.FindWithTag("Player").transform;
        // Set the first target
        SetRandomTarget();
        //Start the coroutine
        StartCoroutine(GetRandomTarget());
    }
    
    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            // lerp towards the target
            // m_rigidbody.position = Vector3.Lerp(m_rigidbody.position, m_target, speed);
            m_rigidbody.AddForce((m_target - m_rigidbody.position).normalized * speed * 10, ForceMode.VelocityChange);
            //transform.LookAt(m_target);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag.Equals("Player"))
            StartCoroutine(Squish());
    }
    private IEnumerator GetRandomTarget()
    {
        if(Vector3.Distance(m_target, transform.position) < 1.0f)
            yield return null;
        else
            yield return new WaitForSeconds(3.0f);
        SetRandomTarget();
        StartCoroutine(GetRandomTarget());
    }

    public void SetRandomTarget()
    {
        m_target = new Vector3( player.position.x + Random.Range(-10.0f,10.0f), transform.position.y, 
                                player.position.z + Random.Range(-10.0f,10.0f) ); 
    }
    private IEnumerator Squish()
    {
        squishedRoaches++;
        moving = false;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y/2.0f, transform.localScale.z);
        yield return new WaitForSeconds(2.0f);
        Instantiate(explosion,transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
