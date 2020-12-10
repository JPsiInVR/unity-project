using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronMovement : MonoBehaviour
{
    [SerializeField]
    GameObject positron;

    private float speed = 0.5f;

    void LateUpdate()
    {
        speed *= 1.02f;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, positron.transform.position, step);
        if (Vector3.Distance(transform.position, positron.transform.position) < 0.001f)
        {
            positron.transform.position *= -1.0f;
        }
    }
}
