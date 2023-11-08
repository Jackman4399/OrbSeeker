using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishOrbDoor : MonoBehaviour
{   
    public LevelLoaderScript levelLoader;

    private void Start() {
        levelLoader = GameObject.FindGameObjectWithTag("TransitionCF").GetComponent<LevelLoaderScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {    
        
        if (collision.gameObject.name == "Player" && collision.gameObject.GetComponent<CollectorScript>().HasOrb()){
            Invoke("CompleteLevel", 0.5f);
        } else {
            Debug.Log("No orb has been collected.");
        }
    }

    public void CompleteLevel(){
        levelLoader.LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
