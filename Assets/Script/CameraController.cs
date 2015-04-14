using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject player;

	void LateUpdate ()
	{
		transform.position = new Vector3 (Mathf.Clamp(player.transform.position.x, -60, 64), Mathf.Clamp(player.transform.position.y, -77, 66), transform.position.z);
	}
}
