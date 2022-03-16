using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public Animator animator;

    public float speed;
    public float Player;
    public Canvas pauseCanvas;
    public Animator transition;
    public float loadPauseTime;
    private Rigidbody2D body; 
    private SpriteRenderer sprite;
    private bool isGround = true;
    private float prevVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        // pauseCanvas = GameObject.Find("pauseMenuCanvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")* speed));
        prevVelocity = body.velocity.x;
        body.velocity = new Vector2(Input.GetAxis("Horizontal")* speed, body.velocity.y);
        if(Input.GetAxis("Horizontal") != 0) {
            FindObjectOfType<AudioManager>().Play("walk_bush");
            Debug.Log("horizontal true");
        }
        else {
            FindObjectOfType<AudioManager>().Pause("walk_bush");
        }

        if (Input.GetKey(KeyCode.Space) && isGround){
            body.velocity = new Vector2(body.velocity.x, speed);
            FindObjectOfType<AudioManager>().Play("jump_bush");
            isGround = false;
        }

        if (Mathf.Sign(prevVelocity) != Mathf.Sign(body.velocity.x)){
            sprite.flipX = !sprite.flipX;
        }
        // if (Input.GetKeyDown(KeyCode.Escape)){
        //     Debug.Log("pressed esc");
        //     if(!pauseCanvas.enabled) {
        //         Debug.Log("should play tital screen");
        //         FindObjectOfType<AudioManager>().Play("title_screen");
        //     }
        //     else {
        //         FindObjectOfType<AudioManager>().Pause("title_screen");
        //     }
        //     // transition.SetTrigger("Start");
        //     pauseCanvas.enabled = !pauseCanvas.enabled;
        //     // StartCoroutine(loadPause());
        // }
    }

/*  Old simultaneous control
        if (Player == 1 && Input.GetMouseButton(0)){
            body.velocity = new Vector2(Input.GetAxis("Horizontal")* speed, body.velocity.y);
            if (Input.GetKey(KeyCode.Space)){
                body.velocity = new Vector2(body.velocity.x, speed);
                isGround = false;
            }
            if (Mathf.Sign(prevVelocity) != Mathf.Sign(body.velocity.x)){
                sprite.flipX = !sprite.flipX;
            }
        }
        if (Player == 2 && Input.GetMouseButton(1)){
            body.velocity = new Vector2(Input.GetAxis("Horizontal")* speed, body.velocity.y);
            if (Input.GetKey(KeyCode.Space)){
                body.velocity = new Vector2(body.velocity.x, speed);
                isGround = false;
            }
            if (body.velocity.x < 0){
                sprite.flipX = true;
            } else{
                sprite.flipX = false;
            }
        }
*/


    // private bool isGrounded(){
    //     RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxcollider.bounds.size, 0, Vector2,down, 0.1f, ground);
    // }
    // private IEnumerator loadPause() {
    //     // Play Animation
    //     transition.SetTrigger("Start");
    //     // set wait time
    //     yield return new WaitForSeconds(loadPauseTime);
    //     pauseCanvas.enabled = !pauseCanvas.enabled;
    //     Debug.Log("yolo");
    // }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag =="Ground"){
            isGround = true;
        }
        
    }
    void OnCollisionStay2D(Collision2D collision){
        if (collision.gameObject.tag =="Wall"){
            Debug.Log("hit");
            animator.SetFloat("Speed", 0);
        }else{
            animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")* speed));
        }
    }
    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.tag == "Ground"){
            isGround = false;
        }
    }
}