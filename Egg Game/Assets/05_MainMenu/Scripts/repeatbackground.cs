using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatbackground : MonoBehaviour
{

    //position that object starts at. Where to reset background
    private Vector3 startPos;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        // start position to original in transform
        startPos = transform.position;
        // repeat widtch accesses the boxcollider, size, and grabs the x axis and divides it by two, meaning we find the halfway point of the object
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // if inside of component "transform", the position of x is "repeatwidth/half size of the image" less than the original starting position, restart to original starting position
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
