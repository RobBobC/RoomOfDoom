using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
	
	void Start ()
	{
	}

	void LateUpdate ()
	{
        transform.position = player.transform.position;
	}
}
