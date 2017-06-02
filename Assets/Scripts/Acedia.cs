using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Acedia : baseEnemy {

    public float speed,
                 lifeTotal,
                 damage,
                 minionLaunchInterval;
    private float step,
                  timer;
    public GameObject launchPads,
                      minion,
                      lifeBar,
                      nameText;
    private GameObject target;
    private Rigidbody rb;
    private Color temp;
    private bool healthFilled;

    public Acedia(float lifeTotal): base(lifeTotal)
    {
        health = 0;
    }

	// Use this for initialization
	void Start ()
    {
        healthFilled = false;
        timer = 0;
        target = GameObject.FindGameObjectWithTag("Base");
        StartCoroutine(FillLife());
        temp = nameText.GetComponent<Text>().color;
        temp.a = 0;
        nameText.GetComponent<Text>().color = temp;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(health <= -1)
        {
            KillEnemy();
        }

        timer += Time.deltaTime;

        Move();
        
        if(timer >= minionLaunchInterval)
        {
            timer = 0;
            LaunchMinions();
        }

        if (healthFilled)
        {
            UpdateLife();
        }	
	}

    private void UpdateLife()
    {
        lifeBar.GetComponent<Slider>().value = health;
    }

    private IEnumerator FillLife()
    {
        
        for (; health <= lifeTotal; health++)
        {
            yield return new WaitForSeconds(Time.deltaTime);           
            lifeBar.GetComponent<Slider>().value = health;  
            temp.a = health / lifeTotal;
            nameText.GetComponent<Text>().color = temp;
            
        }

        healthFilled = true;
    }

    void LaunchMinions()
    {
        Transform[] allChildren = launchPads.GetComponentsInChildren<Transform>();      
        foreach (Transform child in allChildren)
        {
            if (child.transform.parent.name == "Launch Pad")
            {
                Instantiate(minion, child.transform.position + new Vector3(0, 10), Quaternion.identity, child);
            }
        }
    }

    void Move()
    {
        transform.LookAt(target.transform);
        step = speed * Time.deltaTime;

        //Segue o objeto de referência(target) apenas nas coordenadas X e Z.
        transform.position = Vector3.MoveTowards(transform.position, 
                                                 new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z),
                                                 step);
    }
}
