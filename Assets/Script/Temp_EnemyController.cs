using UnityEngine;
using System.Collections;

public class Temp_EnemyController : BaseEnemy {
	//variables here
	
	void Start()
	{
		base.Start(); //call BaseEnemy's start function
	}
	
	void Update()
	{
		if(IsDead) //check if enemy has been killed before updating
			return;
		//enemy behavior defined here
	}
}
