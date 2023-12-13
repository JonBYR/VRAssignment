using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsCheck : MonoBehaviour
{
    public List<GameObject> currentCollisions = new List<GameObject>();
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        currentCollisions.Add(other.gameObject);
    }
    void OnTriggerExit(Collider other)
    {
        currentCollisions.Remove(other.gameObject);
        Debug.Log(currentCollisions.Count);
    }
}
