using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    SceneTransitionManager sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = SceneTransitionManager.SCENE_MANAGER;
    }

    private void Update()
    {

        //Developer cheat code to make my life easier
        if (Input.GetKey(KeyCode.Backslash))
        {
            Debug.Log("Skipping to end");
            sm.NextScene();
        } 
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            sm.NextScene();
        }
    }
}
