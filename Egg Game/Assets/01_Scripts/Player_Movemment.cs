using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movemment : MonoBehaviour
{
    private CharacterController Player_CC;
    public float Player_Speed;

    public GameObject[] Cameras = new GameObject[4];

    private void Awake()
    {
        Player_CC = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        ChangeCameras();
    }

    private void Move()
    {
        if (GameManager.GameManager_Script.Difficulty != GameManager.DifficultyGame.none)
            return;
        
        Vector3 PlayerMove = new Vector3(Input.GetAxis("Vertical") * Player_Speed / Time.deltaTime * -1, 0, Input.GetAxis("Horizontal") * Player_Speed / Time.deltaTime);
        Player_CC.SimpleMove(PlayerMove * Time.deltaTime);
    }

    private void ChangeCameras()
    {
        if (GameManager.GameManager_Script.Difficulty != GameManager.DifficultyGame.none)
            return;
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < Cameras.Length; i++)
            {
                if (Cameras[i].activeSelf && (i - 1) < 0)
                {
                    Cameras[3].SetActive(true);
                    Cameras[i].SetActive(false);
                    return;
                }
                if (Cameras[i].activeSelf && (i - 1) >= 0)
                {
                    Cameras[i - 1].SetActive(true);
                    Cameras[i].SetActive(false);
                    return;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < Cameras.Length; i++)
            {
                if (Cameras[i].activeSelf && (i + 1) > 3)
                {
                    Cameras[0].SetActive(true);
                    Cameras[i].SetActive(false);
                    return;
                }
                if (Cameras[i].activeSelf && (i + 1) <= 3)
                {
                    Cameras[i + 1].SetActive(true);
                    Cameras[i].SetActive(false);
                    return;
                }
            }
        }
    }
}
