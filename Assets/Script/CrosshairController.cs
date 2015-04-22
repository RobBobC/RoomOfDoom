using UnityEngine;
using System.Collections;

public class CrosshairController : MonoBehaviour {
	public Texture crosshairImage;
    
	void Start ()
	{
		Screen.showCursor = false;
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Input.mousePosition.x - (crosshairImage.width / 4), (Screen.height - Input.mousePosition.y) - (crosshairImage.height / 4), crosshairImage.width / 2, crosshairImage.height / 2), crosshairImage);
	}
}
