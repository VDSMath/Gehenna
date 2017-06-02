using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadByIndex (float seconds)
	{
        StartCoroutine(WaitAndLoad(seconds));
	}

    public IEnumerator WaitAndLoad(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(1);
    }

}
