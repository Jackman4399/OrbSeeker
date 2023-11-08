using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectorScript : MonoBehaviour
{
    //Does the player have the orb?
    private bool orbAcquired = false;

    [SerializeField] private AudioSource acquireOrb;

    //Collector function for the orb
    private void OnTriggerEnter2D(Collider2D collision) {

     if (collision.gameObject.CompareTag("RealOrb") && !orbAcquired){

            acquireOrb.Play();

            Destroy(collision.gameObject);

            orbAcquired = true;

        } else if (collision.gameObject.CompareTag("FakeOrb")) {

            //Uncomment to reset game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }

    //Will return true if player has the orb
    public bool HasOrb() {
        return orbAcquired;
    }

}
