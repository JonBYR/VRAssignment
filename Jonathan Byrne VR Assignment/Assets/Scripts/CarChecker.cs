using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChecker : MonoBehaviour
{
    CarController Cara;
    CarController Carb;
    CarController Carc;
    // Start is called before the first frame update
    void Awake()
    {
        Cara = GameObject.Find("Car").GetComponent<CarController>();
        Carb = GameObject.Find("Car (1)").GetComponent<CarController>();
        Carc = GameObject.Find("Car (2)").GetComponent<CarController>();
        Cara.yPos = -5.0f;
        Carb.yPos = -10.0f;
        Carc.yPos = -15.0f;
    }
    void Start()
    {
        Cara.initialDelay = 3.0f;
        Carb.initialDelay = 6.0f;
        Carc.initialDelay = 12.0f;
        Cara.turn = false;
        Carb.turn = true;
        Carc.turn = false;
    }

    
}
