using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    GameObject player;
    PlayerController playerController;
    
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
        }
    }
    
    void Update()
    {
        if (playerController != null)
        {
            if (playerController.dead)
            {
                Application.LoadLevel("Lose");
                Time.timeScale = 1;
            }
        }
    }
}
