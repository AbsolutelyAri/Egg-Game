using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followmouse : MonoBehaviour
{
    public Vector3 turn;
    public float speed = 40.0f;

    void Update()
    {
        
        {
            turn.x += Input.GetAxis("Mouse X");
            turn.y = Input.GetAxis("Mouse Y");

            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.Rotate(Vector3.back * Time.deltaTime * speed);
        }
    }

}
