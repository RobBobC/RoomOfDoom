using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject player;

	void LateUpdate ()
	{
        //transform.position = player.transform.position;
		transform.position = new Vector3 (Mathf.Clamp(player.transform.position.x, -64, 68), Mathf.Clamp(player.transform.position.y, -77, 65), transform.position.z);
	}
}
