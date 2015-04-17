using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject[] players;
    private GameObject currentPlayer;
    private Vector2 currentPosition;
    //public void DestroyPlayer(int index)
    //{
    //    Destroy(player[index]);
    //}
	
	// Use this for initialization
	void Start ()
	{
		// Initialize necessary game objects
        currentPlayer = players[0];
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Game logic
        if (Input.GetKeyDown("p"))
            ChangePlayer(1);

	}

    void ChangePlayer(int playerType)
    {
        currentPlayer = players[playerType];
        currentPosition = (Vector2)currentPlayer.transform.position;
        //Destroy(currentPlayer.gameObject);
        //Instantiate(currentPlayer, currentPosition, Quaternion.Euler(new Vector2(0, 0)));
    }
}
