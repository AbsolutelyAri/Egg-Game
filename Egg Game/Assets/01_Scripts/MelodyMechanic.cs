using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MelodyMechanic : MonoBehaviour
{
    public GameObject Start_BTN;

    private void Awake()
    {
        Start_BTN = GameObject.FindGameObjectWithTag("Start Button");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Start_BTN.transform.DOMoveY(35, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Start_BTN.transform.DOMoveY(-35, 1f);
        }
    }
}
