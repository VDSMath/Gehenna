using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour {

    public float destroyTime,
                 speed;
    public float damage;
    public Vector3 target;

    void Start()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //Destroy the bullet after a set amount of time to prevent bloating.
        Destroy(gameObject, destroyTime);
        //Physics.IgnoreCollision(this.GetComponent<Collider>(), this.transform.parent.GetComponent<Collider>()); 
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }
        
    void OnCollisionEnter(Collision collision)
    {
        //Getting the object that the bullet collided with.
        GameObject target = collision.gameObject;

        //Checking if it's the player.
        if (target.tag == "Player")
        {
            //Causing damage based on the minion's damage value.
            target.GetComponent<playerController>().TakeDamage(damage);

            Destroy(this.gameObject);
        }
    }
}
