using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStartPage : MonoBehaviour
{

    public LevelLoaderScript levelLoader;

    private void Start() {
        levelLoader = GameObject.FindGameObjectWithTag("TransitionCF").GetComponent<LevelLoaderScript>();
    }

    public void GoToMain(){

        Debug.Log("Going to main");

        levelLoader.LoadNextScene(0);

    }
}
