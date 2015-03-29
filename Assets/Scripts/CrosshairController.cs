using UnityEngine;
using System.Collections;

public class CrosshairController : MonoBehaviour {

	public Texture crosshairImage;
	
	void Start ()
	{
		Screen.showCursor = false;
	}

	/*void Update ()
	{
		
	}*/
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Input.mousePosition.x - (crosshairImage.width / 2), (Screen.height - Input.mousePosition.y) - (crosshairImage.height / 2), crosshairImage.width, crosshairImage.height), crosshairImage);
	}
}
