using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject pauseImage;

    bool paused;
    GameObject player;
    PlayerController playerController;
    
    void Start()
    {
        paused = false;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (!paused)
            {
                pauseImage.SetActive(true);
                Time.timeScale = 0;
                paused = true;
            }
            else
            {
                pauseImage.SetActive(false);
                Time.timeScale = 1;
                paused = false;
            }
        }
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
