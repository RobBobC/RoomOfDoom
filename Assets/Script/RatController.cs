using UnityEngine;
using System.Collections;

public class RatController : BaseEnemy {
	float lookAtAngle;

	void Start()
	{
		base.Start();
	}

    void Update()
    {
        if (IsDead)
        {
            return;
        }

        base.Update();

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        direction = player.transform.position - transform.position;

        lookAtAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(lookAtAngle - 90, Vector3.forward);
    }
}
