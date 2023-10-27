using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Orb")){

            Destroy(collision.gameObject);
            Debug.Log("Orb collected!");

        }

    }

}
