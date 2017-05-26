using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour {

    public float destroyTime,
                 speed;
    public float damage;
    public Vector3 target;

    public GameObject minion;

    void Start()
    {
        transform.LookAt(target);

        //Destroy the bullet after a set amount of time to prevent bloating.
        Destroy(gameObject, destroyTime);
        if (minion != null)
        {
            if(gameObject.tag == "Enemy")
                damage = minion.GetComponent<AIDisc>().damage;
            if(gameObject.tag == "Acedia")
                damage = minion.GetComponent<Acedia>().damage;
        }
    }

    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime);
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
        }

        //Also destroying the bullet on collision.
        Destroy(this.gameObject);
    }
}
