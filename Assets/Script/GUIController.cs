using UnityEngine;
using System.Collections;

public class GUIController : GameController {

	void StopEditorPlayback ()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	public void MainMenuButtonPressed ()
	{
		Application.LoadLevel ("MainMenu");
	}

	public void StartButtonPressed ()
	{
		Application.LoadLevel ("Room");
	}

	public void QuitButtonPressed ()
	{
		Application.Quit();
		StopEditorPlayback ();
	}
    
	void Start ()
	{
		if (Application.loadedLevelName == "MainMenu" || Application.loadedLevelName == "Lose" || Application.loadedLevelName == "Win")
		{
			Screen.showCursor = true;
		}
	}
}
