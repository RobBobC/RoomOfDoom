using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject player;
	public BoxCollider2D cameraBounds;

	private float halfCamWidth;
	private float halfCamHeight;
	private float x;
	private float y;
	private Vector3 min;
	private Vector3 max;

	void Start()
	{
		//the orthographic size is half of the camera view's height
		halfCamHeight = camera.orthographicSize;
		//calculate half of the camera view's width
		halfCamWidth = halfCamHeight * ((float) Screen.width / Screen.height);

		//max and min values are based on the bounds of the cameraBounds collider box area (disabled collision physics)
		min = cameraBounds.bounds.min;
		max = cameraBounds.bounds.max;
	}

	void LateUpdate ()
	{
		x = player.transform.position.x;
		y = player.transform.position.y;

		//ensure that the camera stays within its bounds
		x = Mathf.Clamp(x, min.x + halfCamWidth, max.x - halfCamWidth);
		y = Mathf.Clamp(y, min.y + halfCamHeight, max.y - halfCamHeight);

		//update camera position
		transform.position = new Vector3 (x, y, transform.position.z);
	}
}
