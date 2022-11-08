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
}
