using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Acedia : MonoBehaviour {

    public float speed,
                 lifeTotal,
                 currentLife,
                 damage;
    private float step,
                  timer;
    public GameObject launchPads,
                      minion,
                      lifeBar,
                      nameText;
    private GameObject target;
    private Rigidbody rb;
    private Color temp;

	// Use this for initialization
	void Start ()
    { 
        timer = 0;
        target = GameObject.FindGameObjectWithTag("Base");
        currentLife = 0;
        StartCoroutine(FillLife());
        temp = nameText.GetComponent<Text>().color;
        temp.a = 0;
        nameText.GetComponent<Text>().color = temp;
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

    private IEnumerator FillLife()
    {
        for (; currentLife <= lifeTotal; currentLife++)
        {
            yield return new WaitForSeconds(Time.deltaTime);           
            lifeBar.GetComponent<Image>().fillAmount = currentLife/lifeTotal;  
            temp.a = currentLife / lifeTotal;
            nameText.GetComponent<Text>().color = temp;
            
        }
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
        step = speed * Time.deltaTime;

        //Segue o objeto de referência(target) apenas nas coordenadas X e Z.
        transform.position = Vector3.MoveTowards(transform.position, 
                                                 new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z),
                                                 step);
    }
}
