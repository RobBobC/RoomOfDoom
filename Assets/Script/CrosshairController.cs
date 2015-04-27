using UnityEngine;
using System.Collections;

public class CrosshairController : MonoBehaviour {
    
	void Start ()
	{
		Screen.showCursor = false;
	}

    void Update ()
    {
        transform.position = Input.mousePosition;
    }
}
