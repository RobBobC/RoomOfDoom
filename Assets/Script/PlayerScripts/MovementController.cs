using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
    public float moveSpeed = 1.0f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey("a") && Input.GetKey("w"))
        {
            transform.Translate(new Vector3(-(1 / Mathf.Sqrt(2)), (1 / Mathf.Sqrt(2)), 0) * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey("a") && Input.GetKey("s"))
        {
            transform.Translate(new Vector3(-(1 / Mathf.Sqrt(2)), -(1 / Mathf.Sqrt(2)), 0) * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey("d") && Input.GetKey("w"))
        {
            transform.Translate(new Vector3((1 / Mathf.Sqrt(2)), (1 / Mathf.Sqrt(2)), 0) * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey("d") && Input.GetKey("s"))
        {
            transform.Translate(new Vector3((1 / Mathf.Sqrt(2)), -(1 / Mathf.Sqrt(2)), 0) * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }	
	}
}
