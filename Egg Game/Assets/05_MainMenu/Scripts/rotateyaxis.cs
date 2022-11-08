using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateyaxis : MonoBehaviour
{

    public GameObject note;
    public float speed = 40.0f;

    void Start()
    {

        
    }

    void Update()
    {
        //transform.Rotate(10.0f * Time.deltaTime, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime *speed);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.Rotate(Vector3.back * Time.deltaTime * speed);
        }

    }
    
}

