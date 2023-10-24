using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    public float moveSpeed = 5f;
    public LayerMask obstacles;
    public LayerMask moveable;

    public Animator anim;

    public Rigidbody2D rb;

    private Vector2 movement;

    private bool isMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        //box = GameObject.FindGameObjectWithTag("Box").GetComponent<PushScript>();
    }

    // Update is called once per frame
    void Update() {
        
        
        if (!isMoving) {

            if(movement.y == 0) {
                movement.x = Input.GetAxisRaw("Horizontal");
            }
            
            if(movement.x == 0) {
                movement.y = Input.GetAxisRaw("Vertical");
            }
            
            if (movement != Vector2.zero){

                var targetPos = transform.position;
                targetPos.x += movement.x;
                targetPos.y += movement.y;

                Vector3 diff = targetPos - transform.position;

                if (CanMove(targetPos, diff)) {
                    StartCoroutine(Move(targetPos));
                }

            }
        }

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
        
    }


    IEnumerator Move(Vector3 targetPos) {

        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon){
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed* Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool CanMove(Vector3 targetPos, Vector3 dir) {

        if (Physics2D.OverlapCircle(targetPos, 0.2f, obstacles) != null) {
            Debug.Log("Found Obstacle");
            return false;
        } else if (Physics2D.OverlapCircle(targetPos, 0.2f, moveable) != null){

            Debug.Log("Found Box");

            var obs = Physics2D.OverlapCircle(targetPos, 0.2f, moveable);
            var gameObj = obs.gameObject.GetComponent<PushScript>();

            Vector3 direction = Vector3.zero;

            if (dir == Vector3.up) {

                direction = Vector3.up;
                Debug.Log("Checking up...");

            } else if (dir == Vector3.down) {

                direction = Vector3.down;
                Debug.Log("Checking down...");

            } else if (dir == Vector3.right) {

                direction = Vector3.right;
                Debug.Log("Checking right...");

            } else if (dir == Vector3.left) {

                direction = Vector3.left;
                Debug.Log("Checking left...");

            }

            if (gameObj.Blocked(direction)) {
                Debug.Log("Can't move... blocked path...");
                return false;
            }


        }
        return true;
    }

    
}
