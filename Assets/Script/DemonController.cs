using UnityEngine;
using System.Collections;

public class DemonController : BaseEnemy {
    public GameObject fireball;
    public int shotSpeed = 1000;
    public GameObject launchBox;
    public GameObject[] circleLaunchBoxes;
    public int launchBoxRotatationSpeed;

    private bool coolDown;
    private float coolDownDuration;
    private float lookAtAngle;
    private float fireBallDuration;

    

    // Use this for initialization
    void Start()
    {
        base.Start();
        coolDownDuration = 1.5f;
        fireBallDuration = .75f;
        coolDown = false;
    }

    // Update is called once per frame
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

        if (coolDown)
        {
            if (coolDownDuration < 0)
            {
                coolDown = false;
                coolDownDuration = 1.0f;
            }
            else
                coolDownDuration -= Time.deltaTime;
        }
        else
        {
            FireBallCircle();
        }

        if(fireBallDuration > 0)
        {
            fireBallDuration -= Time.deltaTime;
        }
        else
        {
            fireBallDuration = .75f;
            FireBall();
        }
    }

    void FireBall()
    {
        direction.Normalize();
        GameObject shot = Instantiate(fireball, launchBox.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        shot.rigidbody2D.AddForce(6 * direction * shotSpeed);
        coolDown = true;
    }

    void FireBallCircle()
    {
        Vector3 circleDirection = new Vector3();
        direction.Normalize();

        for (int i = 0; i < circleLaunchBoxes.Length; i++)
        {
            GameObject shot = Instantiate(fireball, circleLaunchBoxes[i].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            circleDirection = circleLaunchBoxes[i].transform.position - transform.position;
            shot.rigidbody2D.AddForce(circleDirection * shotSpeed);
            coolDown = true;
        }
    }
}
