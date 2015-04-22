using UnityEngine;
using System.Collections;

public class GargantController : BaseEnemy {
    
	new void Start()
	{
		base.Start ();
	}

	new void Update()
	{
		if(IsDead)
		{
			return;
		}

		base.Update();
	}
}
