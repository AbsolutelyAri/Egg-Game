/*
 * Created by: MrKamikazeee
 * Created on: 11/7/2022
 * 
 * Last Modified: 11/8/2022
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MelodyMechanic : MonoBehaviour
{
    public GameObject Start_BTN;

    public TextMeshProUGUI Wait_Text;

    public int Wall_Number;

    private void Awake()
    {
        Start_BTN = GameObject.FindGameObjectWithTag("Start Button");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.GameManager_Script.CanPlay)
        {
            Start_BTN.transform.DOMoveY(35, 1f);
        }
        else
        {
            Wait_Text.text = "You can't play now, just wait " + GameManager.GameManager_Script.Time_left + " seconds to play again.";
            Sequence WT_Sequence = DOTween.Sequence();
            WT_Sequence.Append(Wait_Text.DOFade(1, 1f))
                .AppendInterval(3f)
                .Append(Wait_Text.DOFade(0, 1f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Start_BTN.transform.DOMoveY(-35, 1f);
        }
    }
    
    // Move the difficuly panel into the screen
    public void DifficultyPanel()
    {
        GameManager.GameManager_Script.MG_Cam.SetActive(true);
        Start_BTN.transform.DOMoveY(-35, 1f);
        GameManager.GameManager_Script.Difficulty_Panel.transform.DOMoveX(GameManager.GameManager_Script.Difficulty_Panel.transform.position.x - (160 * 2.5f), 1f);
        GameManager.GameManager_Script.Difficulty = GameManager.DifficultyGame.Choosing;
    }

    // Roll back the difficult panel
    public void CancelMinigame()
    {
        GameManager.GameManager_Script.Difficulty_Panel.transform.DOMoveX(GameManager.GameManager_Script.Difficulty_Panel.transform.position.x + (160 * 2.5f), 1f);
        Start_BTN.transform.DOMoveY(35, 1f);
        GameManager.GameManager_Script.Difficulty = GameManager.DifficultyGame.none;
        GameManager.GameManager_Script.MG_Cam.SetActive(false);
    }
    
    public void EasyMode()
    {
        GameManager.GameManager_Script.Difficulty = GameManager.DifficultyGame.Easy;
        GameManager.GameManager_Script.StartMinigame();
        GameManager.GameManager_Script.Wall_Number = Wall_Number;
    }

    public void MediumMode()
    {
        GameManager.GameManager_Script.Difficulty = GameManager.DifficultyGame.Medium;
        GameManager.GameManager_Script.StartMinigame();
        GameManager.GameManager_Script.Wall_Number = Wall_Number;
    }

    public void HardMode()
    {
        GameManager.GameManager_Script.Difficulty = GameManager.DifficultyGame.Hard;
        GameManager.GameManager_Script.StartMinigame();
        GameManager.GameManager_Script.Wall_Number = Wall_Number;
    }
}
