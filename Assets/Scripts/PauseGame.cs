using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	public GameObject pauseBackground, pausePanel, helpPanel, player, popupPanel, text1, text2, text3, text4, text5;
	bool popup1, popup2, popup3, popup4, popup5;

	void Start(){

		player = GameObject.Find ("player");
		popup1 = false;
		popup2 = false;
		popup3 = false;
		popup4 = false;
		popup5 = false;

	}

	void Update () {

		//if (player.GetComponent<Transform> ().position.z > 200 && popup1 == false) {

		//	pause (1);
		//	popup1 = true;

		//}

		//if (player.GetComponent<Transform> ().position.z > 3300 && popup2 == false) {

		//	pause (2);
		//	popup2 = true;

		//}

		//if (player.GetComponent<Transform> ().position.z > 5000 && popup3 == false) {

		//	pause (3);
		//	popup3 = true;

		//}

		//if (player.GetComponent<Transform> ().position.y > 1900 && popup4 == false) {

		//	pause (4);
		//	popup4 = true;

		//}

		//if (player.GetComponent<Transform> ().position.y > 1950 && popup5 == false) {

		//	pause (5);
		//	popup5 = true;

		//}
			

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{

			if (pauseBackground.activeInHierarchy == false) {

				pause (0);

			} else if(helpPanel.activeInHierarchy == true)
				{
				
				helpPanel.gameObject.SetActive (false);
				pausePanel.gameObject.SetActive (true);

				} else unpause ();

		}
	}

	public void pause(int panel) 
	{

		switch (panel) {
			
		case 1:

			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0;
			Cursor.visible = true;
			pauseBackground.SetActive (true);
			popupPanel.SetActive (true);
			text1.SetActive (true);
			player.GetComponent<playerController> ().enabled = false;
			break;

		case 2: 

			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0;
			Cursor.visible = true;
			pauseBackground.SetActive (true);
			popupPanel.SetActive (true);
			text2.SetActive (true);
			player.GetComponent<playerController> ().enabled = false;
			break;

		case 3:

			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0;
			Cursor.visible = true;
			pauseBackground.SetActive (true);
			popupPanel.SetActive (true);
			text3.SetActive (true);
			player.GetComponent<playerController> ().enabled = false;
			break;

		case 4:

			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0;
			Cursor.visible = true;
			pauseBackground.SetActive (true);
			popupPanel.SetActive (true);
			text4.SetActive (true);
			player.GetComponent<playerController> ().enabled = false;
			break;

		case 5:

			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0;
			Cursor.visible = true;
			pauseBackground.SetActive (true);
			popupPanel.SetActive (true);
			text5.SetActive (true);
			player.GetComponent<playerController> ().enabled = false;
			break;

		default:
			
			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0;
			Cursor.visible = true;
			pauseBackground.SetActive (true);
			pausePanel.SetActive (true);
			player.GetComponent<playerController> ().enabled = false;
			break;

		}
			
	}

	public void unpause()
	{

		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;
		Cursor.visible = false;
		pauseBackground.SetActive (false);
		pausePanel.SetActive (false);
		popupPanel.SetActive (false);
		text1.SetActive (false);
		text2.SetActive (false);
		text3.SetActive (false);
		text4.SetActive (false);
		text5.SetActive (false);
		player.GetComponent<playerController>().enabled = true ;

	}
		

}
