using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PushScript : MonoBehaviour
{

    public float distance = 1f;

    public Transform movePoint;

    public Rigidbody2D rb;

    public LayerMask obstacles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Player") {

            if (other.transform.position.x > movePoint.position.x) {
                
                Vector3 move = new Vector3(-distance, 0f, 0f);
                if (checkObstacle(move)) {
                    movePoint.position += move;
                }
                
            } else if (other.transform.position.x < movePoint.position.x) {

                Vector3 move = new Vector3(distance, 0f, 0f);
                if (checkObstacle(move)) {
                    movePoint.position += move;
                }

            } else if (other.transform.position.y < movePoint.position.y) {

                Vector3 move = new Vector3(0f, distance, 0f);
                if (checkObstacle(move)) {
                    movePoint.position += move;
                }

            } else if (other.transform.position.y > movePoint.position.y) {

                Vector3 move = new Vector3(0f, -distance, 0f);
                if (checkObstacle(move)) {
                    movePoint.position += move;
                }
            }

        }
    }

    private bool checkObstacle(Vector3 vector3) {
        return !Physics2D.OverlapCircle(movePoint.position + vector3, 0.2f, obstacles);
    }

}
