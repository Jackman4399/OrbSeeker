using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask obstacles;

    public Animator anim;

    public Rigidbody2D rb;

    private Vector2 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update() {
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

    }


    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed* Time.deltaTime);


        if(Vector3.Distance(transform.position, movePoint.position) == 0) {

            if(Mathf.Abs(movement.x) == 1f) {

                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, 0f, 0f), 0.2f, obstacles)){
                    movePoint.position += new Vector3(movement.x, 0f, 0f);
                }
                
            } else if(Mathf.Abs(movement.y) == 1f) {
                
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movement.y, 0f), 0.2f, obstacles)){
                    movePoint.position += new Vector3(0f, movement.y, 0f);
                }
            }

            
        }

        
    }
}
