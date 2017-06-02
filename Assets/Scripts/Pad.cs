using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour {

    public GameObject acedia;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;

        if(target.tag == "Bullet")
        {
            acedia.GetComponent<Acedia>().TakeDamage(target.GetComponent<bullet>().damage);
        }
    }
}
