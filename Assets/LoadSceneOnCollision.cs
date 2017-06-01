using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnCollision : MonoBehaviour {

    private GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        target = other.gameObject;
        if (target.name == "player")
        {
            SceneManager.LoadScene(2);
        }
    }
}
