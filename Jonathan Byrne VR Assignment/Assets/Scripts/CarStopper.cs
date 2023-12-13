using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStopper : MonoBehaviour
{
    public GameObject red;
    public GameObject green;
    private Transform colliderTransform;
    private float x;
    private float y;
    private float z;
    void Start()
    {
        colliderTransform = this.GetComponent<Transform>();
        x = colliderTransform.position.x;
        y = colliderTransform.position.y;
        z = colliderTransform.position.z;
    }
    void Update()
    {
        if(green.active == true)
        {
            colliderTransform.position = new Vector3(x, -100f, z);
        }
        else if(red.active == true)
        {
            colliderTransform.position = new Vector3(x, y, z);
        }
    }
}
