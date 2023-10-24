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

    //public MovementScript avatar; 

    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        //avatar = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>();
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if (other.transform.tag == "Player") {

    //         //var target = movePoint.position;

    //         if (other.transform.position.x > movePoint.position.x) {
                
    //             target += new Vector3(-distance, 0f, 0f);
                
    //         } else if (other.transform.position.x < movePoint.position.x) {

    //             target += new Vector3(distance, 0f, 0f);

    //         } else if (other.transform.position.y < movePoint.position.y) {

    //             target += new Vector3(0f, distance, 0f);

    //         } else if (other.transform.position.y > movePoint.position.y) {

    //             target += new Vector3(0f, -distance, 0f);

    //         }

    //         if (Physics2D.Linecast(movePoint.position, target)) {
    //             Debug.Log("BOX MOVE");
    //             //Move(target);
    //         }

    //     }
    // }

     private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Player") {

            if (other.transform.position.x > movePoint.position.x) {
                
                Vector3 move = new Vector3(-distance, 0f, 0f);
                if (CheckObstacle(move) && Blocked(move)) {
                    Debug.Log("1");
                    movePoint.position += move;
                }
                
            } else if (other.transform.position.x < movePoint.position.x) {

                Vector3 move = new Vector3(distance, 0f, 0f);
                if (CheckObstacle(move) && Blocked(move)) {
                    Debug.Log("2");
                    movePoint.position += move;
                }

            } else if (other.transform.position.y < movePoint.position.y) {

                Vector3 move = new Vector3(0f, distance, 0f);
                if (CheckObstacle(move) && Blocked(move)) {
                    Debug.Log("3");
                    movePoint.position += move;
                }

            } else if (other.transform.position.y > movePoint.position.y) {

                Vector3 move = new Vector3(0f, -distance, 0f);
                if (CheckObstacle(move) && Blocked(move)) {
                    Debug.Log("4");
                    movePoint.position += move;
                }
            }

        }
    }

    
    // IEnumerator Move(Vector3 targetPos) {

    //     isMoving = true;

    //     while ((targetPos - movePoint.position).sqrMagnitude > Mathf.Epsilon){
    //         movePoint.position = Vector3.MoveTowards(movePoint.position, targetPos, 5f * Time.deltaTime);
    //         yield return null;
    //     }
    //     movePoint.position = targetPos;

    //     isMoving = false;
    //     Debug.Log("Stopped moving box");
    // }

    private bool CheckObstacle(Vector3 target) {
        bool temp = !Physics2D.OverlapCircle(target, 0.2f, obstacles);

        return temp;
    }

    public bool Blocked(Vector3 direction) {

        //Could possibly use Linecast for simplicity
        RaycastHit2D hit = Physics2D.Raycast(movePoint.position, direction, distance);

        Debug.Log("Looking towards: " + direction + " and " + movePoint.position);

        if(hit.collider != null) {
            return true;
        }

        return false;

    }

}
