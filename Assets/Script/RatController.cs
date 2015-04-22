using UnityEngine;
using System.Collections;

public class RatController : BaseEnemy {
    bool avoidingCollision = false;
    float lookAtAngle;
    float timeToAvoidCollision = 2f;

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

        direction = player.transform.position - transform.position;
        lookAtAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(lookAtAngle - 90, Vector3.forward);

        //if (Physics2D.Raycast(this.transform.position, direction, 2f).collider.tag == "Obstacle")
        //{
        //    avoidingCollision = true;
        //    Quaternion angleFacing = transform.rotation;
        //    transform.rotation = new Quaternion(angleFacing.x, angleFacing.y, angleFacing.z+90, angleFacing.w);
        //}

        //if (avoidingCollision)
        //{
        //    timeToAvoidCollision -= Time.deltaTime;
        //    if(timeToAvoidCollision <= 0)
        //    {
        //        avoidingCollision = false;
        //        timeToAvoidCollision = 2f;
        //    }
        //    else
        //    {
        //        Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        //    }
        //}
        //else
        //{
        //}
    }

}
