using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    static public GameManager GameManager_Script;
    
    public bool Choosing_Difficult;
    
    private GameObject Start_BTN;
    private GameObject Difficult_Panel;
    private DifficultGame Difficult;
    
    private GameObject MG_Panel;
    public GameObject MG_Cam;
    public GameObject[] MG_Buttons = new GameObject[4];
    public GameObject[] MG_Buttons_Pos;
    public GameObject[] MG_Buttons_InGame = new GameObject[8];
    private int[] MG_Buttons_Info = new int[8];
    
    private int TemporaryInt;
    
    public enum DifficultGame
    {
        Easy,
        Medium,
        Hard
    }
    
    private void Awake()
    {
        Start_BTN = GameObject.FindGameObjectWithTag("Start Button");
        Difficult_Panel = GameObject.FindGameObjectWithTag("Difficult Panel");
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
    }

    public void Number1()
    {
        if (!Input.GetKey(KeyCode.Keypad1) || !Input.GetKey(KeyCode.Alpha1))
            return;
        Minigame(1);
        
    }
    public void Number2()
    {
        if (!Input.GetKey(KeyCode.Keypad2) || !Input.GetKey(KeyCode.Alpha2))
            return;
        
        Minigame(2);
    }
    public void Number3()
    {
        if (!Input.GetKey(KeyCode.Keypad3) || !Input.GetKey(KeyCode.Alpha3))
            return;
        
        Minigame(3);
    }
    public void Number4()
    {
        if (!Input.GetKey(KeyCode.Keypad4) || !Input.GetKey(KeyCode.Alpha4))
            return;
        
        Minigame(4);
    }

    private void Minigame(int num)
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
            QuickSequence.Append(MG_Buttons_InGame[TemporaryInt].GetComponent<Image>().DOFade(0,1f))
                .Join(MG_Buttons_InGame[TemporaryInt].transform.DOScale(2, 1f))
                .OnComplete(() => { MG_Buttons_InGame[TemporaryInt].SetActive(false); });
        }
        else
        {
            for (int i = 7; i >= 0; i--)
            {
                MG_Buttons_InGame[i].SetActive(true);
                MG_Buttons_InGame[TemporaryInt].transform.DOScale(1, .1f);
                MG_Buttons_InGame[TemporaryInt].GetComponent<Image>().DOFade(1, .1f);
            }
        }
    }

    public void DifficultPanel()
    {
        MG_Cam.SetActive(true);
        Start_BTN.transform.DOMoveY(-35, 1f);
        Difficult_Panel.transform.DOMoveX(Difficult_Panel.transform.position.x - (160 * 2), 1f);
        Choosing_Difficult = true;
    }

    public void CancelMinigame()
    {
        Difficult_Panel.transform.DOMoveX(Difficult_Panel.transform.position.x + (160 * 2), 1f);
        Start_BTN.transform.DOMoveY(35, 1f);
        Choosing_Difficult = false;
        MG_Cam.SetActive(false);
    }

    public void EasyMode()
    {
        Difficult = DifficultGame.Easy;
        StartMinigame();
    }

    public void MediumMode()
    {
        Difficult = DifficultGame.Medium;
        StartMinigame();
    }

    public void HardMode()
    {
        Difficult = DifficultGame.Hard;
        StartMinigame();
    }

    private void StartMinigame()
    {
        Difficult_Panel.transform.DOMoveX(Difficult_Panel.transform.position.x + (160 * 2), 1f);
        MG_Panel.GetComponent<Image>().DOFade(1, 1f);
        RandomButton();
    }

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
}
