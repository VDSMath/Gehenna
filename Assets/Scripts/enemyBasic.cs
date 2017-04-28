using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBasic : MonoBehaviour {

    public float health;
    public float startingHealth;

    public GameObject explosion;

	// Use this for initialization
	void Start ()
    {
        //Setting the health to the initial amount.
        health = startingHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Destroying the enemy if health gets below or equal to zero.
        if (health <= 0)
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
	}

    //Function for taking damage. Shoul be called by the damage dealer. Set to public to be accessible by other scripts.
    public void takeDamage (float amount)
    {
        health -= amount;
        Debug.Log("Damage");
    }

}
