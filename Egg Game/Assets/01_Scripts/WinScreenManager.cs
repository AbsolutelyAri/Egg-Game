/*
 * Created by: Krieger
 * Created on: 11/10/2022
 * 
 * Modified: 11/10/2022
 * 
 * Description: Handles the buttons for the PlayerWinScene by making calls to the SceneTransitionManager
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenManager : MonoBehaviour
{
    private SceneTransitionManager sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = SceneTransitionManager.SCENE_MANAGER;
    }

    public void ReturnToMenu()
    {
        sm.ReturnToStartScene();
    }

    public void ReplayLevel()
    {
        sm.StartFromLevelOne();
    }

    public void QuitGame()
    {
        sm.QuitGame();
    }
}
