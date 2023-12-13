using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CarController : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetWP = 0;
    public float yPos;
    public Rigidbody rb;
    public bool go = false;
    public float initialDelay;
    public float colliderDelay;
    public bool turn = false;
    public AudioSource carSound;
    public CollisionsCheck collCheck;
    // Start is called before the first frame update
    void Start()
    {
        routeNumber = 0;
        rb = GetComponent<Rigidbody>();
        wps = new List<Transform>();
        GameObject wp;
        wp = GameObject.Find("CWP1");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP2");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP3");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP4");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP5");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP7");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP8");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP6");
        wps.Add(wp.transform);
        transform.position = new Vector3(0.0f, yPos, 0.0f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!go)
        {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0.0f)
            {
                go = true;
                carSound.Play();
                SetRoute();
            }
            else return;
        }
        Vector3 displacement = route[targetWP].position - transform.position;
        displacement.y = 0;
        float dist = displacement.magnitude;
        if (dist < 0.1f)
        {
            targetWP++;
            if (targetWP >= route.Count)
            {
                routeNumber++;
                if ((routeNumber > 3) && (turn == false)) routeNumber = 0;
                else if ((routeNumber > 7) && (turn == true)) routeNumber = 0;
                SetRoute();
                return;
            }
        }
        Vector3 velocity = displacement;
        velocity.Normalize();
        velocity *= 10.0f;
        Vector3 newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;
        rb.MovePosition(newPosition);
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 10.0f * Time.deltaTime, 0f);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);
        
        if(collCheck.currentCollisions.Count > 0)
        {
            carSound.Pause();
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        else if(collCheck.currentCollisions.Count == 0) 
        {
            carSound.Play();
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }
    void SetRoute()
    {
        //randomise the next route
        //set the route waypoints
        
        if(!turn)
        {
            if (routeNumber == 0) route = new List<Transform> { wps[0], wps[1] };
            else if (routeNumber == 1) route = new List<Transform> { wps[1], wps[2] };
            else if (routeNumber == 2) route = new List<Transform> { wps[2], wps[3] };
            else if (routeNumber == 3) route = new List<Transform> { wps[3], wps[0] };
        }
        else if(turn)
        {
            if (routeNumber == 0) route = new List<Transform> { wps[0], wps[1] };
            else if (routeNumber == 1) route = new List<Transform> { wps[1], wps[2] };
            else if (routeNumber == 2) route = new List<Transform> { wps[2], wps[4] };
            else if (routeNumber == 3) route = new List<Transform> { wps[4], wps[5] };
            else if (routeNumber == 4) route = new List<Transform> { wps[5], wps[6] };
            else if (routeNumber == 5) route = new List<Transform> { wps[6], wps[7] };
            else if (routeNumber == 6) route = new List<Transform> { wps[7], wps[3] };
            else if (routeNumber == 7) route = new List<Transform> { wps[3], wps[0] };
        }
        transform.position = new Vector3(route[0].position.x, 0.5f, route[0].position.z);
        targetWP = 0;
    }
}
