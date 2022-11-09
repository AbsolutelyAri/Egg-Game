/*
 * Created by: Krieger
 * Created on: 11/8/2022
 * 
 * Last Modified: n/a
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public string startScene; //Start menu scene
    public string[] gameScenes; //cutscenes and levels
    private string currentSceneName;
    private int currentLevelIndex = 0; //current index in the gameScenes array


    #region SceneTransitionManagerSingleton
    static private SceneTransitionManager sceneManager; //refence GameManager
    static public SceneTransitionManager SCENE_MANAGER { get { return sceneManager; } } //public access to read only gm 

    //Check to make sure only one gm of the GameManager is in the scene
    void CheckSceneTransitionManagerIsInScene()
    {

        //Check if instnace is null
        if (sceneManager == null)
        {
            sceneManager = this; //set gm to this gm of the game object
            Debug.Log(sceneManager);
        }
        else //else if gm is not null a Game Manager must already exsist
        {
            Destroy(this.gameObject); //In this case you need to delete this gm
        }
        DontDestroyOnLoad(this); //Do not delete the GameManager when scenes load
        Debug.Log(sceneManager);
    }//end CheckGameManagerIsInScene()
    #endregion

    void Awake()
    {
        CheckSceneTransitionManagerIsInScene();
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Going to quit game");
            QuitGame();
        }
    }

    //Exits the game
    public void QuitGame()
    {
        Debug.Log("Exited Game");
        Application.Quit();
        
    }

    public void NextScene()
    {
        SceneManager.LoadScene(gameScenes[currentLevelIndex]);
        currentLevelIndex++;
        Debug.Log(currentLevelIndex + " is the current level number\nLoading " + gameScenes[currentLevelIndex-1]);
    }
}
