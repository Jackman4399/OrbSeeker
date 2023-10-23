using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask obstacles;

    public Animator anim;

    public Rigidbody2D rb;

    private Vector2 movement;

    private PushScript box;
    
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        //box = GameObject.FindGameObjectWithTag("Box").GetComponent<PushScript>();
        box = null;
    }

    // Update is called once per frame
    void Update() {
        
        if(!KeyPressed()) {
            if (movement.y == 0) {
                movement.x = Input.GetAxisRaw("Horizontal");
            }
            if (movement.x == 0) {
                movement.y = Input.GetAxisRaw("Vertical");
            }

            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);
        }

    }


    private bool KeyPressed(){
        bool output = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) 
                    || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow);
        
        return output;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed* Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) == 0) {

            if(Mathf.Abs(movement.x) == 1f) {

                if(checkObs(new Vector3(movement.x, 0f, 0f))){
                    movePoint.position += new Vector3(movement.x, 0f, 0f);
                    
                } 
                
            } else if(Mathf.Abs(movement.y) == 1f) {
                
                if(checkObs(new Vector3(0f, movement.y, 0f))){
                    movePoint.position += new Vector3(0f, movement.y, 0f);
               
                }
            }

            
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        box = other.gameObject.GetComponent<PushScript>();
    }   

    private bool checkObs(Vector3 vector) {

        bool temp = !Physics2D.OverlapCircle(movePoint.position + vector, 0.2f, obstacles);
        bool temp1 = true;
        if (box != null) {
            temp1 = !box.isBlocked();
        }
        return temp && temp1;
    }

    
}
