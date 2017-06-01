using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float destroyTime;
    public float damage;

    public GameObject player;

	void Start ()
    {
        //Destroy the bullet after a set amount of time to prevent bloating.
        Destroy(gameObject, destroyTime);
        //Ignore collisions with player.
        player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
    }
	
	void Update ()
    {
        //Getting the damage value from the player.
        if(player != null)
            damage = player.GetComponent<playerController>().damage;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Getting the object that the bullet collided with.
        GameObject target = collision.gameObject;

        //Checking if it's an enemy.
        if (target.tag == "Enemy")
        {
            //Causing damage based on the player's damage value.
            target.GetComponent<baseEnemy>().TakeDamage(damage);
        }

        if (target.name == "normalCube" ||
                target.name == "normalCube (1)" ||
                target.name == "normalCube (2)" ||
                target.name == "normalCube (3)")
        {

            //Causing damage based on the player's damage value.
            Debug.Log("damage");
            target.GetComponent<baseEnemy>().TakeDamage(damage);

        }
        else if (target.name == "StrongCube" ||
                 target.name == "StrongCube (1)" ||
                 target.name == "StrongCube (2)" ||
                 target.name == "StrongCube (3)")
        {
            if (damage == 20)
            {
                //Causing damage based on the player's damage value.
                Debug.Log("damage");
                target.GetComponent<baseEnemy>().TakeDamage(damage);
            }

        }

        //Also destroying the bullet on collision.
        Destroy(this.gameObject);
    }
}
