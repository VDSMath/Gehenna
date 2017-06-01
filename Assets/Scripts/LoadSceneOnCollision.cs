using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnCollision : MonoBehaviour {

    private GameObject target;
    public GameObject fade;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        target = other.gameObject;
        if (target.name == "player")
        {
            fade.GetComponent<FadeObjectInOut>().FadeIn(fade.GetComponent<FadeObjectInOut>().fadeTime);
            GameObject canvas = GameObject.Find("Canvas");
            canvas.SetActive(false);
            yield return new WaitForSeconds(fade.GetComponent<FadeObjectInOut>().fadeTime);
            SceneManager.LoadScene(2);
        }
    }
}
