/*
 * Created by: MrKamikazeee
 * Created on: 11/6/2022
 * 
 * Last Modified: 11/8/2022
 * 
 */

using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

public class GameManager : MonoBehaviour
{
    static public GameManager GameManager_Script { get; set; }

    public bool CanPlay = true;
    public int Time_left;

    [Header("Difficulty Panel")]
    public GameObject Difficulty_Panel;
    public DifficultyGame Difficulty;
    public Slider Time_Slider;
    private int MG_Repetitions;

    [Header("Minigame")]
    public GameObject MG_Cam;
    private GameObject MG_Panel;
    public GameObject[] MG_Buttons = new GameObject[4];
    public GameObject[] MG_Buttons_Pos;
    public GameObject[] MG_Buttons_InGame = new GameObject[8];
    private int[] MG_Buttons_Info = new int[8];
    public TextMeshProUGUI FailText;

    private int TemporaryInt;

    [Header("Walls")]
    public int Wall_Number;
    public GameObject[] Wall1_States;

    public enum DifficultyGame
    {
        none,
        Choosing,
        Fail,
        Easy,
        Medium,
        Hard
    }

    private void Awake()
    {
        if (GameManager_Script == null)
            GameManager_Script = this;
        else
            Destroy(this);
        DontDestroyOnLoad(this);
        Difficulty = DifficultyGame.none;
        Difficulty_Panel = GameObject.FindGameObjectWithTag("Difficulty Panel");
        MG_Panel = GameObject.FindGameObjectWithTag("Minigame Panel");
    }

    void Start()
    {
        DOTween.Init();
    }

    private void Update()
    {
        Number1();
        Number2();
        Number3();
        Number4();
        CheckFail();
    }

    // Prepare the mini-game
    public void StartMinigame()
    {
        Difficulty_Panel.transform.DOMoveX(Difficulty_Panel.transform.position.x + (160 * 2.5f), 1f);
        MG_Panel.GetComponent<Image>().DOFade(1, 1f);
        GameObject[] SliderComponents;
        SliderComponents = GameObject.FindGameObjectsWithTag("Slider Component");
        for (int i = 0; i < SliderComponents.Length; i++)
            SliderComponents[i].GetComponent<Image>().DOFade(1, 1f);
        StartCoroutine(Timer());
        switch (Difficulty)
        {
            case DifficultyGame.Easy:
                MG_Repetitions = 2;
                break;
            
            case DifficultyGame.Medium:
                MG_Repetitions = 3;
                break;
            
            case DifficultyGame.Hard:
                MG_Repetitions = 4;
                break;
        }
        RandomButton();
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        Time_Slider.DOValue(0, 20f);
    }

    // Select a random number for the mini-game
    private void RandomButton()
    {
        for (int i = 0; i < MG_Buttons_Pos.Length; i++)
        {
            MG_Buttons_Info[i] = 0;
            int Random_Number = Random.Range(0, 4);
            GameObject Minigame_Temporary_Button = Instantiate(MG_Buttons[Random_Number], MG_Panel.transform);
            Minigame_Temporary_Button.transform.position = MG_Buttons_Pos[i].transform.position;
            Minigame_Temporary_Button.GetComponent<Image>().DOFade(1, 1f);
            MG_Buttons_InGame[i] = Minigame_Temporary_Button;
            MG_Buttons_Info[i] = Random_Number + 1;
        }
    }

    // Reed which number was pressed
    public void Number1()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            StartCoroutine(Minigame(1));
    }

    public void Number2()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            StartCoroutine(Minigame(2));
    }

    public void Number3()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            StartCoroutine(Minigame(3));
    }

    public void Number4()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            StartCoroutine(Minigame(4));
    }

    // Runs the mini-game
    IEnumerator Minigame(int num)
    {
        for (int i = 0; i < 8; i++) // Search the first active number in the sequence
        {
            if (MG_Buttons_InGame[i].activeSelf)
            {
                TemporaryInt = i;
                i = 8;
            }
        }

        if (MG_Buttons_Info[TemporaryInt] == num) // Controls if the player press the correct key
        {
            Sequence QuickSequence = DOTween.Sequence();
            QuickSequence.Append(MG_Buttons_InGame[TemporaryInt].transform.DOScaleX(.8f * 2, .1f))
                .Join(MG_Buttons_InGame[TemporaryInt].transform.DOScaleY(2.7f * 2, .1f))
                .Join(MG_Buttons_InGame[TemporaryInt].transform.DOScaleZ(2.7f * 2, .1f))
                .Join(MG_Buttons_InGame[TemporaryInt].GetComponent<Image>().DOColor(new Color(166, 166, 166), .1f))
                .Join(MG_Buttons_InGame[TemporaryInt].GetComponent<Image>().DOFade(0, .1f))
                .OnComplete(() =>
                {
                    MG_Buttons_InGame[TemporaryInt].SetActive(false);
                    if (!MG_Buttons_InGame[7].activeSelf && MG_Repetitions > 0) 
                        ResetSequence();
                    else if (!MG_Buttons_InGame[7].activeSelf && MG_Repetitions == 0) // If the player won the game
                        FinishGame();
                });
        }
        else
            ResetNumbers();
        yield return null;
    }
    
    // Reset numbers
    public void ResetNumbers()
    {
        for (int i = 0; i < 8; i++)
        {
            if (!MG_Buttons_InGame[i].activeSelf)
            {
                MG_Buttons_InGame[i].SetActive(true);
                MG_Buttons_InGame[i].transform.DOScaleX(.8f, .3f);
                MG_Buttons_InGame[i].transform.DOScaleY(2.7f, .3f);
                MG_Buttons_InGame[i].transform.DOScaleZ(2.7f, .3f);
                MG_Buttons_InGame[i].GetComponent<Image>().DOFade(1, .3f);
            }

        }
    }
    
    // Reset the sequences to continue the mini-game
    private void ResetSequence()
    {
        MG_Repetitions--;
        for (int i = 0; i < 8; i++)
        {
            Destroy(MG_Buttons_InGame[i]);
            MG_Buttons_InGame[i] = null;
        }
        RandomButton();
    }

    // Finishes the game depending on the difficulty
    private void FinishGame()
    {
        DOTween.PauseAll();
        switch (Wall_Number)
        {
            case 1:
                StartCoroutine(Wall1());
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
        MG_Cam.SetActive(false);
        ResetValues();
    }

    IEnumerator Wall1()
    {
        switch (Difficulty)
        {
            case DifficultyGame.Easy:
                FailText.text = "Nothing happens";
                CanPlay = false;
                Sequence WT_Sequence = DOTween.Sequence();
                WT_Sequence.Append(FailText.DOFade(1, 1f))
                    .AppendInterval(3f)
                    .Append(FailText.DOFade(0, 1f));
                break;
            case DifficultyGame.Medium:
                    for (int i = 0; i < Wall1_States.Length; i++)
                        Wall1_States[i].SetActive(false);
                    Wall1_States[1].SetActive(true);
                break;
            case DifficultyGame.Hard:
                    for (int i = 0; i < Wall1_States.Length; i++)
                        Wall1_States[i].SetActive(false);
                    
                    Wall1_States[1].SetActive(true);
                    yield return new WaitForSeconds(.5f);
                    for (int i = 0; i < Wall1_States.Length; i++)
                        Wall1_States[i].SetActive(false);
                    
                    Wall1_States[2].SetActive(true);
                    yield return new WaitForSeconds(.5f);
                    for (int i = 0; i < Wall1_States.Length; i++)
                        Wall1_States[i].SetActive(false);
                    
                    Wall1_States[3].SetActive(true);
                break;
        }

        yield return new WaitForSeconds(1);
        CanPlay = true;
    }
    
    public void ResetValues()
    {
        GameObject[] SliderComponents = GameObject.FindGameObjectsWithTag("Slider Component");
        Sequence FinishSequence = DOTween.Sequence();
        FinishSequence.Append(MG_Panel.GetComponent<Image>().DOFade(0, 1f))
            .Join(SliderComponents[0].GetComponent<Image>().DOFade(0, 1f))
            .Join(SliderComponents[1].GetComponent<Image>().DOFade(0,1f))
            .AppendInterval(5f)
            .OnComplete(() => DOTween.Clear());
        Time_Slider.value = 15;
        for (int i = 0; i < 8; i++)
        {
            Destroy(MG_Buttons_InGame[i]);
            MG_Buttons_InGame[i] = null;
        }
        Difficulty = DifficultyGame.none;
    }
    
    public void CheckFail()
    {
        if (Time_Slider.value > 0)
            return;

        Difficulty = DifficultyGame.Fail;
        MG_Cam.SetActive(false);
        ResetValues();
        StartCoroutine(StartWaiting());
    }

    IEnumerator StartWaiting()
    {
        CanPlay = false;
        Time_left = 3;
        yield return new WaitForSeconds(1f);
        Time_left = 2;
        yield return new WaitForSeconds(1f);
        Time_left = 1;
        yield return new WaitForSeconds(1f);
        CanPlay = true;
    }
}