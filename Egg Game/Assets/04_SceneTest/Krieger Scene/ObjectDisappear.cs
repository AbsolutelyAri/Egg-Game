/*
 * Created by: Krieger
 * Created on: 11/8/2022
 * 
 * Last Modified: n/a
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisappear : MonoBehaviour
{
    public GameObject disappearingObject;
    public ParticleSystem smoke;
    
    private bool done;
    
    // Start is called before the first frame update
    void Start()
    {
        disappearingObject.SetActive(true);
        done = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Disappear()
    {
        if (!done)
        {
            smoke.Play();
            disappearingObject.SetActive(false);
            done = true;
        }
    }
}
