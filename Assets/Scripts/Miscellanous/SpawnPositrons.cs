using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositrons : MonoBehaviour
{
    private float radius = 9f;
    
    [SerializeField]
    GameObject positron;
    [SerializeField]
    GameObject shelvesCenter;

    private GameObject particle;

    private float particlesCount = 54;
    private float angle = 0;
    private Vector3 newPos;
    
    void Start()
    {
        for (int i = 0; i < particlesCount; i++)
        {
            angle = i * Mathf.PI*2f / particlesCount;
            newPos = new Vector3(Mathf.Cos(angle)*radius + shelvesCenter.transform.position.x, 0.8f + this.transform.position.y, Mathf.Sin(angle)*radius + shelvesCenter.transform.position.z);
            particle = Instantiate(positron, newPos, Quaternion.Euler(0f, -i*Mathf.PI*2.12f, 0f));
            particle.transform.SetParent(this.transform);
        }
    }
}
