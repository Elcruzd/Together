using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public float speed;
    public float rightSpeed;
    public float leftSpeed;
    public float jumpVelocity;
    public Canvas pauseCanvas;
    public Animator transition;
    public float loadPauseTime;
    public float camDist = 25f;
    public float camLerpSpeed = 5f;
    private Rigidbody body; 
    private SpriteRenderer sprite;
    private bool isGround = true;
    private GameObject leftLight;
    private GameObject rightLight;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();
        // pauseCanvas = GameObject.Find("pauseMenuCanvas").GetComponent<Canvas>();
        leftLight = GameObject.Find("LeftLight");
        rightLight = GameObject.Find("RightLight");
        leftLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetAxis("Horizontal") != 0) {
            FindObjectOfType<AudioManager>().Play("walk_bush");
        }
        else {
            FindObjectOfType<AudioManager>().Stop("walk_bush");
        }

        if (Input.GetKey(KeyCode.Space) && isGround){
            body.velocity = new Vector2(body.velocity.x, jumpVelocity);
        }
        if (Input.GetAxis("Horizontal") < 0){
            body.velocity = new Vector3(Input.GetAxis("Horizontal")* leftSpeed, body.velocity.y, body.velocity.z);
            sprite.flipX = true;
            leftLight.SetActive(true);
            rightLight.SetActive(false);
        }else if (Input.GetAxis("Horizontal") > 0){
            body.velocity = new Vector3(Input.GetAxis("Horizontal")* rightSpeed, body.velocity.y, body.velocity.z);
            sprite.flipX = false;
            leftLight.SetActive(false);
            rightLight.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("pressed esc");
            Time.timeScale = 0;
            if(!pauseCanvas.enabled) {
                FindObjectOfType<AudioManager>().Play("title_screen");
            }
            else {
                Time.timeScale = 1;
                FindObjectOfType<AudioManager>().Stop("title_screen");
            }
            //transition.SetTrigger("Start");
            pauseCanvas.enabled = !pauseCanvas.enabled;
            //StartCoroutine(loadPause());
        }
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

    private IEnumerator loadPause() {
        // Play Animation
        transition.SetTrigger("Start");
        // set wait time
        yield return new WaitForSeconds(loadPauseTime);
        pauseCanvas.enabled = !pauseCanvas.enabled;
        Debug.Log("yolo");
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag =="Ground"){
            isGround = true;
        }
        
    }
    void OnCollisionStay(Collision collision){
        if (collision.gameObject.tag =="Wall"){
            Debug.Log("hit");
            animator.SetFloat("Speed", 0);
        }else{
            if (Input.GetAxis("Horizontal") < 0){ 
                animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")* leftSpeed));
            } else{
                animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")* rightSpeed));
            }
        }
    }
    void OnCollisionExit(Collision collision){
        if(collision.gameObject.tag == "Ground"){
            isGround = false;
        }
    }
}
