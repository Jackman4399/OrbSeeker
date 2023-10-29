using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorScript : MonoBehaviour
{
    //Does the player have the orb?
    private bool orbAcquired = false;

    [SerializeField] private AudioSource acquireOrb;

    //Collector function for the orb
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Orb")){

            acquireOrb.Play();

            Destroy(collision.gameObject);
            Debug.Log("Orb collected!");

            orbAcquired = true;
            Debug.Log("Acquired: " + orbAcquired);

        }

    }

    //Will return true if player has the orb
    public bool HasOrb() {

        if (orbAcquired) {

            return true;

        }

        return false;
    }

}
