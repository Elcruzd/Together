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
    private Rigidbody body; 
    private SpriteRenderer sprite;
    private bool isGround = true;
    public GameObject leftLight;
    private GameObject rightLight;
    public GameObject crossfade;
    private GameObject previewCam;
    public GameObject brightenLight;
    public Animator transition;
    private bool canNormalMove = true;
    public GameObject small;
    public bool isInMud = false;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();
        // leftLight = GameObject.Find("LeftLight");
        rightLight = GameObject.Find("RightLight");
        // leftLight.SetActive(false);
        crossfade.SetActive(true);
        previewCam = GameObject.Find("preview cam");
        brightenLight = GameObject.Find("preview light");
        previewCam.SetActive(false);
        brightenLight.SetActive(false);
        small = GameObject.Find("LittleBrother");
        StartCoroutine("startScene");
    }

    // Update is called once per frame
    void Update()
    {
        
        // if(Input.GetAxis("Horizontal") != 0) {
        //     FindObjectOfType<AudioManager>().Play("walk_bush");
        // }
        // else {
        //     FindObjectOfType<AudioManager>().Stop("walk_bush");
        // }
        if(checkCanMove()){
            if (Input.GetButton("Jump") && isGround){
                body.velocity = new Vector2(body.velocity.x, jumpVelocity);
                FindObjectOfType<AudioManager>().Play("jump_bush");
            }
            if (Input.GetAxis("Horizontal") < 0){
                body.velocity = new Vector3(Input.GetAxis("Horizontal")* leftSpeed, body.velocity.y, body.velocity.z);
                sprite.flipX = true;
                leftLight.SetActive(true);
                rightLight.SetActive(false);
                Debug.Log("walkingLeft");
            }else if (Input.GetAxis("Horizontal") > 0){
                body.velocity = new Vector3(Input.GetAxis("Horizontal")* rightSpeed, body.velocity.y, body.velocity.z);
                sprite.flipX = false;
                leftLight.SetActive(false);
                rightLight.SetActive(true);
                Debug.Log("walkingRight");
            }else {
                FindObjectOfType<AudioManager>().Stop("walk_bush");
            }
            if (Input.GetKeyDown(KeyCode.U)){
                Debug.Log("cross lah");
                transition.SetTrigger("Start");
            }
            if (Input.GetKeyDown(KeyCode.O)){
                Debug.Log("cross dsjadnsakdsnald");
                transition.SetTrigger("Start2");
            }
        }
            RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1f)){
            if (hit.transform.gameObject.tag == "Interact" && Input.GetButtonDown("InteractiveLeft")){
                stump(hit.transform.gameObject);
                Debug.Log("hitttttttttttttt");
            }
            if (hit.transform.gameObject.tag == "HoldInteract"){
                if (hit.transform.gameObject.name == "Wheel"){

                }
                if (Input.GetKey(KeyCode.Mouse0)){
                    FindObjectOfType<AudioManager>().Play("walk_bush");
                    
                    canNormalMove = false;
                    Debug.Log("holdingggggg");
                    if (Input.GetAxis("Horizontal") > 0){
                        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")* rightSpeed));
                        hit.transform.position = new Vector3(hit.transform.position.x+0.01f, hit.transform.position.y, hit.transform.position.z);
                        transform.position = new Vector3(transform.position.x+0.01f, transform.position.y, transform.position.z);
                        if (hit.transform.gameObject.name == "Wheelbarrow (1)" ) {
                            FindObjectOfType<AudioManager>().Play("wheel_barrow");
                        }
                    }
                }else{
                    canNormalMove = true;
                    FindObjectOfType<AudioManager>().Stop("wheel_barrow");
                }

            }else {
                canNormalMove = true;
                FindObjectOfType<AudioManager>().Stop("wheel_barrow");
            }
        }
        Debug.DrawRay(transform.position, Vector3.right*1, Color.green);
        if(Input.GetKey(KeyCode.I)){
            if (leftSpeed == 3){
                leftSpeed = 1;
            } else if (leftSpeed == 1){
                leftSpeed = 3;
            }
        }
        Debug.Log(rightSpeed);
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

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag =="Ground"){
            isGround = true;
        }
        
    }
    void OnCollisionStay(Collision collision){
        if (collision.gameObject.tag == "Wall" | collision.gameObject.tag == "Interact" | collision.gameObject.tag == "HoldInteract"){
            // Debug.Log("hit");
            animator.SetFloat("Speed", 0);
            FindObjectOfType<AudioManager>().Stop("walk_bush");
        }else{
            if (Input.GetAxis("Horizontal") < 0){ 
                animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")* leftSpeed));
                animator.speed = 0.5F;
                FindObjectOfType<AudioManager>().Play("walk_bush");
            } else{
                animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")* rightSpeed));
                animator.speed = 1F;
                FindObjectOfType<AudioManager>().Play("walk_bush");
            }
        }
        
    }
    void OnCollisionExit(Collision collision){
        if(collision.gameObject.tag == "Ground"){
            isGround = false;
        }
    }

    public void inMud(){
        isInMud = true;
        rightSpeed = 1.5f;
    }
    public void outMud(){
        isInMud = false;
        rightSpeed = 3f;
    }

    void stump(GameObject obj){
        this.transform.position = new Vector3(this.transform.position.x + 3f, this.transform.position.y, this.transform.position.z);
        small.transform.position = new Vector3(small.transform.position.x + 3f, small.transform.position.y, small.transform.position.z);
    }
    public void stopPlayerMove(){
        canNormalMove = false;
        animator.Play("Strong_Idle");
    }
    private IEnumerator startScene() {
        Debug.Log("dsadja");
        yield return new WaitForSeconds(0f);

        transition.SetTrigger("Start2");
        Debug.Log("sadsadsadsa");
    }
    public bool checkCanMove(){
        return canNormalMove; 
    }
}
