using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Acedia : MonoBehaviour {

    public float speed;
    private float step,
                  timer;
    public GameObject launchPads,
                      minion;
    private GameObject target;
    private Rigidbody rb;

	// Use this for initialization
	void Start ()
    { 
        timer = 0;
        target = GameObject.FindGameObjectWithTag("Base");
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        Move();
        
        if(timer >= 10)
        {
            timer = 0;
            LaunchMinions();
        }	
	}

    void LaunchMinions()
    {
        Transform[] allChildren = launchPads.GetComponentsInChildren<Transform>();      
        foreach (Transform child in allChildren)
        {
            if (child.transform.parent.name != "Player")
            {
                Instantiate(minion, child.transform.position + new Vector3(0, 10), Quaternion.identity, child);
            }
        }
    }

    void Move()
    {
        step = speed * Time.deltaTime;

        //Segue o objeto de referência(target) apenas nas coordenadas X e Z.
        transform.position = Vector3.MoveTowards(transform.position, 
                                                 new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z),
                                                 step);
    }
}
