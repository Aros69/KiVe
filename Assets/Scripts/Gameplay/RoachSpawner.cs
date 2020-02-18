using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoachSpawner : MonoBehaviour
{
    public GameObject roach = null;
    public GameObject score = null;

    public float spawnInterval = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cockroach.squishedRoaches = 0;
        StartCoroutine(SpawnBadBois());
    }

    void Update()
    {
        score.GetComponent<UnityEngine.UI.Text>().text = Cockroach.squishedRoaches.ToString();
    }

    private IEnumerator SpawnBadBois()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y/2.0f, transform.localScale.z);
        yield return new WaitForSeconds(spawnInterval);
        Instantiate(roach,transform.position + new Vector3(Random.Range(-10.0f,10.0f),0,0), transform.rotation);
        StartCoroutine(SpawnBadBois());
    }
}
