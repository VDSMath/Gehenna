using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDisc : baseEnemy {

    public float nonAggroSpeed,
                 aggroSpeed,
                 aggroRadius,
                 step,
                 shootSpeed,
                 shotOffset,
                 damage,
                 distance,
                 maxLife;
    private float timeCounter,
                  angle,
                  shotTimer;
    public GameObject player,
                      projectile;
    private bool aggro;

    public AIDisc(float maxLife): base(maxLife)
    {
        health = maxLife;
    }

	// Use this for initialization
	void Start () {
        aggro = false;
        angle = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        shotTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if(health <= 0)
        {
            KillEnemy();
        }

        if (player != null)
        {
            if (!aggro)
            {
                FollowPlayer();
                CheckAggro();
            }
            else
            {
                CircleAroundPlayer();
                transform.LookAt(player.transform);

                shotTimer += Time.deltaTime;
                if (shotTimer >= 3)
                {
                   Shoot();
                   shotTimer = 0;
                }
            }
        }
	}

    void FollowPlayer()
    {
        step = nonAggroSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    void CheckAggro()
    {
        float currentDistance = Mathf.Sqrt(Mathf.Pow((transform.position.x - player.transform.position.x), 2) +
                                           Mathf.Pow((transform.position.y - player.transform.position.y), 2) +
                                           Mathf.Pow((transform.position.z - player.transform.position.z), 2));
            
        if(Mathf.Abs(currentDistance) <= distance)
        {
            aggro = true;
        }
        else
        {
            aggro = false;
        }
    }

    void CircleAroundPlayer()
    {
        // timeCounter += Time.deltaTime;
        // float x = Mathf.Cos(timeCounter) * aggroRadius;
        //float y = Mathf.Sin(timeCounter) * aggroRadius;
        //float z = Mathf.Cos(timeCounter) * aggroRadius;
        // transform.position = new Vector3(player.transform.position.x + x,
        //                                player.transform.position.y,
        //                                player.transform.position.z + z); 

        angle += aggroSpeed * Time.deltaTime;

        Vector3 offset = new Vector3(Mathf.Sin(angle), 0,Mathf.Cos(angle)) * aggroRadius;
        transform.position = player.transform.position + offset;
    }

    void Shoot()
    {
        Vector3 off = new Vector3(Random.Range(-shotOffset, shotOffset), Random.Range(-shotOffset, shotOffset), Random.Range(-shotOffset, shotOffset));
        GameObject bullet = Instantiate(projectile, this.transform.position , transform.rotation);
        bullet.GetComponent<enemyBullet>().target = player.transform.position + off;
    }
}
