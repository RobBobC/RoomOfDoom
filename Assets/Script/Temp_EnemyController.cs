using UnityEngine;
using System.Collections;

public class Temp_EnemyController : BaseEnemy {
		

	void Start()
	{
		base.Start ();
	}
	
	void Update()
	{
		if(IsDead)
		{
			return;
		}

		base.Update();


	}
}
