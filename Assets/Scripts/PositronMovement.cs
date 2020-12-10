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
        if (other.name == electron.name)
        {
            StartCoroutine(SpawnJpsi());
            showExplosion();

        }
    }

    private void showJpsi()
    {
        jpsi.SetActive(true);
    }

    private void showExplosion()
    {
        explosion.SetActive(true);
    }

    IEnumerator SpawnJpsi()
    {
        yield return new WaitForSeconds(1); 
        electron.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(1.25f);
        showJpsi();
    }
}
