using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    //Movement speed of the avatar
    public float moveSpeed = 5f;

    //A layer mask representing the obstacles
    public LayerMask obstacles;

    //A layer mask representing the movable objects
    public LayerMask boxes;

    //Animator for the movement animations
    public Animator anim;

    public Rigidbody2D rb;

    //Used for animation and to limit player movement
    private Vector2 movement;

    //A boolean that states if the player is moving or not
    private bool isMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        
        //Player has to be idle
        if (!isMoving) {

            //There two if statements limit diagonal movement
            if(movement.y == 0) {
                movement.x = Input.GetAxisRaw("Horizontal");
            }
            
            if(movement.x == 0) {
                movement.y = Input.GetAxisRaw("Vertical");
            }
            
            //If no movement, move
            if (movement != Vector2.zero){

                var targetPos = transform.position;
                targetPos.x += movement.x;
                targetPos.y += movement.y;

                Vector3 diff = targetPos - transform.position;

                //Checks if player can move towards that direction
                if (CanMove(targetPos, diff)) {
                    StartCoroutine(Move(targetPos));
                }

            }
        }

        //used for animation
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
        
    }


    //Move method for avatar, allows smooth transition
    IEnumerator Move(Vector3 targetPos) {

        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon){
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed* Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    //Method checking if player is able to move
    private bool CanMove(Vector3 targetPos, Vector3 dir) {

        if (Physics2D.OverlapCircle(targetPos, 0.2f, obstacles) != null) { //Checks for immovable obstacles

            return false;

        } else if (Physics2D.OverlapCircle(targetPos, 0.2f, boxes) != null){ //Checks for boxes

            var obs = Physics2D.OverlapCircle(targetPos, 0.2f, boxes);
            var gameObj = obs.gameObject.GetComponent<PushScript>();

            if (gameObj.Blocked(dir)) { // See if box's path is blocked

                return false;

            } else {

                gameObj.Move(dir);

            }

        }

        return true;
        
    }

    
}
