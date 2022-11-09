using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveleft : MonoBehaviour
{
    public float speed = 10f;
    //private PlayerController playerControllerScript;
    private float leftbound = -150;
    // Start is called before the first frame update
    void Start()
    {
        // at the start of the game, anything with the MOVELEFT SCRIPT will look for the player, access the playercontroller script
        //playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
            transform.Translate(Vector3.left * Time.deltaTime * speed);

        //boundary, important to put tag so that it doesn't destroy everything
        if (transform.position.x < leftbound)
        {
            Destroy(this.gameObject);
        }

    }
}
