using UnityEngine;
using System.Collections;

public class baseEnemy : MonoBehaviour
{
    public float health;
    public GameObject explosion;

    public baseEnemy()
    {
        health = 20;
    }

    public baseEnemy(float newHealth)
    {
        health = newHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public void KillEnemy()
    {
        GameObject e = Instantiate(explosion);
        e.transform.position = transform.position;

        GameObject.Destroy(this.gameObject);
    }
}