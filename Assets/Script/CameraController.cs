using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject player;

	void LateUpdate ()
	{
        transform.position = player.transform.position;
	}
}
