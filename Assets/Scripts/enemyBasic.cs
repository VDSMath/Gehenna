using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBasic : baseEnemy {

    public float startingHealth;

	// Use this for initialization
	public enemyBasic (float startingHealth): base(startingHealth)
    {
        //Setting the health to the initial amount.
        health = startingHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(health <= 0)
        {
            KillEnemy();
        }
	}
}
