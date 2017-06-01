using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMovement : MonoBehaviour {

	public GameObject gate;
    private GameObject cube1, cube2, cube3, cube4;

	private Vector3 gateFinalPos;
	public float XFinalPos;
	public float speed;

	void Start () {

		gateFinalPos = new Vector3( XFinalPos , gate.transform.position.y , gate.transform.position.z);
        cube1 = GameObject.Find("normalCube");
        cube2 = GameObject.Find("normalCube (1)");
        cube3 = GameObject.Find("normalCube (2)");
        cube4 = GameObject.Find("normalCube (3)");


    }
	
	void Update () {

		if (this.name == "Gate1" || this.name == "Gate2") {

			if (cube1 == null && cube2 == null && cube3 == null && cube4 == null) {
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
