using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMovement : MonoBehaviour {

	public GameObject gate;

	private Vector3 gateFinalPos;
	public float XFinalPos;
	public float speed;

	void Start () {

		gateFinalPos = new Vector3( XFinalPos , gate.transform.position.y , gate.transform.position.z);

	}
	
	void Update () {

		if (this.name == "Gate1" || this.name == "Gate2") {

			if (!GameObject.Find ("normalCube") && !GameObject.Find ("normalCube (1)") && !GameObject.Find ("normalCube (2)") && !GameObject.Find ("normalCube (3)")) {
		
				gate.transform.position = Vector3.Lerp (gate.transform.position, gateFinalPos, Time.deltaTime * speed);

			}
		} else if (this.name == "Gate3" || this.name == "Gate4") {
		
			if (!GameObject.Find ("StrongCube") && !GameObject.Find ("StrongCube (1)") && !GameObject.Find ("StrongCube (2)") && !GameObject.Find ("StrongCube (3)")) {

				gate.transform.position = Vector3.Lerp (gate.transform.position, gateFinalPos, Time.deltaTime * speed);

			}
		
		} else if (this.name == "Gate5" || this.name == "Gate6") {
		
			if (GameObject.Find ("player").GetComponent<Transform> ().position.y > 5150) {

				gate.transform.position = Vector3.Lerp (gate.transform.position, gateFinalPos, Time.deltaTime * speed);

			}

		}

	}
}
