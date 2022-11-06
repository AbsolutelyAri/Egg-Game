using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movemment : MonoBehaviour
{
    private CharacterController Player_CC;
    public float Player_Speed;
    
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
        Vector3 PlayerMove = new Vector3(Input.GetAxis("Vertical") * Player_Speed / Time.deltaTime * -1, 0, Input.GetAxis("Horizontal") * Player_Speed / Time.deltaTime);
        Player_CC.SimpleMove(PlayerMove * Time.deltaTime);
    }
}
