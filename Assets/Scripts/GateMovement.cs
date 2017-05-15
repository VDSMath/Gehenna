using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMovement : MonoBehaviour {

	public GameObject gate;

	private Vector3 gateFinalPos;
	public float XFinalPos;

	//These will take care of the movement
	public float speed;
	//private float distCovered;
	//private float fracJourney;
	//private float startTime;
	//private float length;

	// Use this for initialization
	void Start () {

		//startTime = Time.time;
		gateFinalPos = new Vector3( XFinalPos , gate.transform.position.y , gate.transform.position.z);
		//length = Vector3.Distance (gate.transform.position, gateFinalPos);

	}
	
	// Update is called once per frame
	void Update () {

		//float distCovered = (Time.time - startTime) * speed;
		//float fracJourney = distCovered / length;
		gate.transform.position = Vector3.Lerp( gate.transform.position, gateFinalPos, Time.deltaTime * speed);

	}
}
