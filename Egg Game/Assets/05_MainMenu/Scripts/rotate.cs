using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public MeshRenderer Renderer;
    public float speed = 40.0f;

    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;

        InvokeRepeating("ColorRange", 1f, 1.5f);

    }

    void Update()
    {
        transform.Rotate(10.0f * Time.deltaTime, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
        }
    }
    void ColorRange()
    {
        // Pick a random, saturated and not-too-dark color
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
