using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public void StartButton ()
	{
		Application.LoadLevel (1);
	}

	public void MainMenu ()
	{
		Application.LoadLevel (0);
	}
}
