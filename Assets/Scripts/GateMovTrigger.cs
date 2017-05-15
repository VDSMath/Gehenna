using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMovTrigger : MonoBehaviour {

	public GameObject gate1, gate2;

	void OnDisable(){

		gate1.gameObject.GetComponent<GateMovement>().enabled = true;
		gate2.gameObject.GetComponent<GateMovement>().enabled = true;

	}
}
