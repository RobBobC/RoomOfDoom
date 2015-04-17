using UnityEngine;
using System.Collections;

public class DemonController : BaseEnemy {


	// Use this for initialization
	void Start ()
	{
        base.Start();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (IsDead)
        {
            return;
        }

        base.Update();
	}
}
