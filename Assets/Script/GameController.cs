using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	void Start ()
	{
		if (Application.loadedLevel == 0 || Application.loadedLevel == 2 || Application.loadedLevel == 3)
		{
			Screen.showCursor = true;
		}
	}

	public void MainMenu ()
	{
		Application.LoadLevel (0);
	}

	public void StartButton ()
	{
		Application.LoadLevel (1);
	}
}
