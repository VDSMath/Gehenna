using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	public Transform pauseBackground, pausePanel, helpPanel;

	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{

			if (pauseBackground.gameObject.activeInHierarchy == false) {

				pause ();

			} else if(helpPanel.gameObject.activeInHierarchy == true)
				{
				
				helpPanel.gameObject.SetActive (false);
				pausePanel.gameObject.SetActive (true);

				} else unpause ();

		}
	}

	public void pause() 
	{

		Time.timeScale = 0;
		Cursor.visible = true;
		pauseBackground.gameObject.SetActive (true);
		pausePanel.gameObject.SetActive (true);
		GameObject.Find("player").GetComponent<playerController>().enabled = false;

	}

	public void unpause()
	{

		Time.timeScale = 1;
		Cursor.visible = false;
		pauseBackground.gameObject.SetActive (false);
		pausePanel.gameObject.SetActive (false);
		GameObject.Find("player").GetComponent<playerController>().enabled = true ;

	}
		

}
