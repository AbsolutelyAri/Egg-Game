/*
 * Created by: Krieger
 * Created on: 11/10/2022
 * 
 * Modified: n/a
 * 
 * Description: 
 *  Makes calls to the SceneTransitionManager from within the scene. 
 *  Mainly used for buttons that change the scene
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTransitionManager : MonoBehaviour
{
    SceneTransitionManager sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = SceneTransitionManager.SCENE_MANAGER;
    }

    public void RestartLevel()
    {
        sm.StartFromLevelOne();
    }

    public void ReturnToMenu()
    {
        sm.ReturnToStartScene();
    }

    public void NextScene()
    {
        sm.NextScene();
    }
}
