using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PedestrianRoute : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetWP = 0;
    private Rigidbody rb;
    public bool go = false;
    public float initialDelay;
    private AudioSource pedaudio;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.speed = 1.0f;
        pedaudio = this.gameObject.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        wps = new List<Transform>();
        GameObject wp;
        wp = GameObject.Find("WP1");
        wps.Add(wp.transform);
        wp = GameObject.Find("WP2");
        wps.Add(wp.transform);
        wp = GameObject.Find("WP3");
        wps.Add(wp.transform);
        wp = GameObject.Find("WP4");
        wps.Add(wp.transform);
        wp = GameObject.Find("WP5");
        wps.Add(wp.transform);
        wp = GameObject.Find("WP6");
        wps.Add(wp.transform);
        wp = GameObject.Find("WP7");
        wps.Add(wp.transform);
        wp = GameObject.Find("WP8");
        wps.Add(wp.transform);
        SetRoute();
        initialDelay = Random.Range(2.0f, 12.0f);
        transform.position = new Vector3(0.0f, -5.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!go)
        {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0.0f)
            {
                go = true;
                pedaudio.Play();
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
                SetRoute();
                return;
            }
        }
        Vector3 velocity = displacement;
        velocity.Normalize();
        velocity *= 2.5f;
        Vector3 newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;
        rb.MovePosition(newPosition);
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 10.0f * Time.deltaTime, 0f);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);
    }
    void SetRoute()
    {
        //randomise the next route
        routeNumber = Random.Range(0, 12);
        //set the route waypoints
        if (routeNumber == 0) route = new List<Transform> { wps[0], wps[4], wps[5], wps[6] };
        else if (routeNumber == 1) route = new List<Transform> { wps[0], wps[4], wps[5], wps[7] };
        else if (routeNumber == 2) route = new List<Transform> { wps[2], wps[1], wps[4], wps[5], wps[6] };
        else if (routeNumber == 3) route = new List<Transform> { wps[2], wps[1], wps[4], wps[5], wps[7] };
        else if (routeNumber == 4) route = new List<Transform> { wps[3], wps[4], wps[5], wps[6] };
        else if (routeNumber == 5) route = new List<Transform> { wps[3], wps[4], wps[5], wps[7] };
        else if (routeNumber == 6) route = new List<Transform> { wps[6], wps[5], wps[4], wps[0] };
        else if (routeNumber == 7) route = new List<Transform> { wps[6], wps[5], wps[4], wps[3] };
        else if (routeNumber == 8) route = new List<Transform> { wps[6], wps[5], wps[4], wps[1], wps[2] };
        else if (routeNumber == 9) route = new List<Transform> { wps[7], wps[5], wps[4], wps[0] };
        else if (routeNumber == 10) route = new List<Transform> { wps[7], wps[5], wps[4], wps[3] };
        else if (routeNumber == 11) route = new List<Transform> { wps[7], wps[5], wps[4], wps[1], wps[2] };
        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.0f, route[0].position.z);
        targetWP = 1;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Light") return;
        else
        {
            pedaudio.Pause();
            rb.constraints = RigidbodyConstraints.FreezePosition;
            anim.speed = -0.1f;
            if (other.gameObject.tag == "Car")
            {
                Invoke("WalkAgain", 1);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        pedaudio.Play();
        anim.speed = 1.0f;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    void WalkAgain()
    {
        pedaudio.Play();
        anim.speed = 1.0f;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Car")
        {
            Debug.Log("Collided");
            pedaudio.Pause();
            rb.constraints = RigidbodyConstraints.FreezePosition;
            anim.speed = -0.1f;
        }
    }
    /*
    void OnCollisionExit(Collision collision)
    {
        pedaudio.Play();
        anim.speed = 1.0f;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    */
}
