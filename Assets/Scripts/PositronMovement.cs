using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositronMovement : MonoBehaviour
{
    [SerializeField]
    GameObject electron;

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    GameObject jpsi;

    private float speed = 0.5f;

    void Update()
    {
        speed *= 1.02f;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, electron.transform.position, step);
        if (Vector3.Distance(transform.position, electron.transform.position) < 0.001f)
        {
            electron.transform.position *= -1.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == electron.name){
            StartCoroutine(SpawnJpsi());
            Instantiate(explosion, new Vector3(5, 1, 0), Quaternion.identity);
            electron.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    IEnumerator SpawnJpsi(){
        yield return new WaitForSeconds(2.0f);
        GameObject jpsiObject = Instantiate(jpsi, new Vector3(5, 0.2f, 0), Quaternion.Euler(-90, 90, 0));
        jpsiObject.transform.localScale = new Vector3(25f, 25f, 25f);
        Destroy(gameObject);
        Destroy(electron);
    }
}
