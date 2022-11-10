using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

public class GameManager : MonoBehaviour
{
    static public GameManager GameManager_Script;

    public bool CanPlay = true;
    public int Time_left;

    private GameObject Start_BTN;
    private GameObject Difficulty_Panel;
    public DifficultyGame Difficulty;
    public Slider Time_Slider;
    private int MG_Repetitions;

    private GameObject MG_Panel;
    public GameObject MG_Cam;
    public GameObject[] MG_Buttons = new GameObject[4];
    public GameObject[] MG_Buttons_Pos;
    public GameObject[] MG_Buttons_InGame = new GameObject[8];
    private int[] MG_Buttons_Info = new int[8];

    private int TemporaryInt;

    public enum DifficultyGame
    {
        none,
        Choosing,
        Fail,
        Win,
        Easy,
        Medium,
        Hard
    }

    private void Awake()
    {
        GameManager_Script = this;
        Difficulty = DifficultyGame.none;
        Start_BTN = GameObject.FindGameObjectWithTag("Start Button");
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
        CheckWin();
        CheckFail();
    }

    // Prepare the mini-game
    private void StartMinigame()
    {
        Difficulty_Panel.transform.DOMoveX(Difficulty_Panel.transform.position.x + (160 * 2), 1f);
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
        for (int i = 0; i < 8; i++)
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
        for (int i = 0; i < 8; i++)
        {
            if (MG_Buttons_InGame[i].activeSelf)
            {
                TemporaryInt = i;
                i = 8;
            }
        }

        if (MG_Buttons_Info[TemporaryInt] == num)
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
                    {
                        MG_Repetitions--;
                        for (int i = 0; i < 8; i++)
                        {
                            Destroy(MG_Buttons_InGame[i]);
                            MG_Buttons_InGame[i] = null;
                        }
                        RandomButton();
                    }
                    else if (!MG_Buttons_InGame[7].activeSelf && MG_Repetitions == 0)
                        Difficulty = DifficultyGame.Win;
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

    // Move the difficuly panel into the screen
    public void DifficultyPanel()
    {
        MG_Cam.SetActive(true);
        Start_BTN.transform.DOMoveY(-35, 1f);
        Difficulty_Panel.transform.DOMoveX(Difficulty_Panel.transform.position.x - (160 * 2), 1f);
        Difficulty = DifficultyGame.Choosing;
    }

    // Roll back the difficult panel
    public void CancelMinigame()
    {
        Difficulty_Panel.transform.DOMoveX(Difficulty_Panel.transform.position.x + (160 * 2), 1f);
        Start_BTN.transform.DOMoveY(35, 1f);
        Difficulty = DifficultyGame.none;
        MG_Cam.SetActive(false);
    }

    public void EasyMode()
    {
        Difficulty = DifficultyGame.Easy;
        StartMinigame();
    }

    public void MediumMode()
    {
        Difficulty = DifficultyGame.Medium;
        StartMinigame();
    }

    public void HardMode()
    {
        Difficulty = DifficultyGame.Hard;
        StartMinigame();
    }

    public void CheckWin()
    {
        if (Difficulty != DifficultyGame.Win)
            return;
        
        MG_Cam.SetActive(false);
        DOTween.Clear();
        ResetValues();
    }

    public void ResetValues()
    {
        MG_Panel.GetComponent<Image>().DOFade(0, 1f);
        GameObject[] SliderComponents;
        SliderComponents = GameObject.FindGameObjectsWithTag("Slider Component");
        for (int i = 0; i < SliderComponents.Length; i++)
            SliderComponents[i].GetComponent<Image>().DOFade(0, 1f);
        Time_Slider.value = 20;
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
