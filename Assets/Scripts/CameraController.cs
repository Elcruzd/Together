using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraController : MonoBehaviour
{

    // Q, E camera pan control
    public float movementSpeed;
    public float movementTime;
    public Vector3 newPosition;
    public Vector3 playerPosition;
    public GameObject player;
    public Rigidbody rb;
    public float cameraOffset;
    public GameObject rightWall;
    public GameObject leftWall;
    public float leftWallOffset;
    public float rightWallOffset;

    // left and right camera stuff
    private GameObject leftCam;
    private GameObject rightCam;
    private GameObject mainCam;
    private GameObject mainCam2;
    private bool firstTurnLeft = true;
    private bool firstTurnRight = true;
    private bool isTurning = false;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        rb = player.GetComponent<Rigidbody>();
        leftCam = GameObject.Find("Left Camera");
        rightCam = GameObject.Find("Right Camera");
        mainCam = GameObject.Find("Main Camera");
        mainCam2 = GameObject.Find("Main Camera2");
        leftWall = GameObject.Find("LeftWall");
        rightWall = GameObject.Find("RightWall");
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        if (rb.velocity.x == 0){
            if (Input.GetKey(KeyCode.E) || Input.GetAxis("Look") > 0) {
                Debug.Log("im here");
                if(isTurning){
                    StopAllCoroutines();
                }
                firstTurnRight = true;
                firstTurnLeft = true;
                newPosition += (transform.right * movementSpeed);
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
            }

            if (Input.GetKey(KeyCode.Q) || Input.GetAxis("Look") < 0) {
                if(isTurning){
                    StopAllCoroutines();
                }
                firstTurnRight = true;
                firstTurnLeft = true;
                newPosition += (transform.right * -movementSpeed);
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
            }
        }

        newPosition = transform.position;

        // if (transform.position.x < playerX + cameraOffset && transform.position.x > playerX - cameraOffset){
        //     if (Input.GetKey(KeyCode.E)) {
        //     newPosition += (transform.right * movementSpeed);
        //     transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        //     newPosition = transform.position;
        //     }

        //     if (Input.GetKey(KeyCode.Q)) {
        //         newPosition += (transform.right * -movementSpeed);
        //         transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        //         newPosition = transform.position;
        //     }
        // } 
        if (newPosition.x > playerPosition.x + cameraOffset){
            transform.position = new Vector3(playerPosition.x + cameraOffset, transform.position.y, transform.position.z);
        }  
        if (newPosition.x < playerPosition.x - cameraOffset){
            transform.position = new Vector3(playerPosition.x - cameraOffset, transform.position.y, transform.position.z);
        }
        if (Input.GetAxis("Horizontal") < 0){
            if(!firstTurnRight){
                StopCoroutine("turnRight");
                firstTurnRight = true;
            }
            if (firstTurnLeft && FindObjectOfType<PlayerMovement>().checkCanMove()){
                StartCoroutine("turnLeft");
            }
        }else if (Input.GetAxis("Horizontal") > 0){
            if(!firstTurnLeft){
                StopCoroutine("turnLeft");
                firstTurnLeft = true;
            }
            if (firstTurnRight && FindObjectOfType<PlayerMovement>().checkCanMove()){
                StartCoroutine("turnRight");
            }
        }

        if (newPosition.x > rightWall.transform.position.x - rightWallOffset){
            transform.position = new Vector3(rightWall.transform.position.x - rightWallOffset, transform.position.y, transform.position.z);
        }
        if (newPosition.x < leftWall.transform.position.x + leftWallOffset){
            transform.position = new Vector3(leftWall.transform.position.x + leftWallOffset, transform.position.y, transform.position.z);
        }
        if (transform.position.y < -0.5411359f){
            transform.position = new Vector3(transform.position.x, -0.5411359f, transform.position.z);
            Debug.Log("too low");
        }
    }

    private float easeIn(float t) {
        return Mathf.Pow(t, 2.0f);
    }
    private float easeOut(float t) {
        return 1.0f - Mathf.Pow(1.0f - t, 2.0f);
    }
    private float easeInEaseOut(float t){
        return Mathf.Lerp(easeIn(t), easeOut(t), t);
    }
    private IEnumerator turnLeft() {
        var t = 0.0f;
        firstTurnLeft = false;
        isTurning = true;
        while (t < 1.0f){
            if (newPosition.x > rightWall.transform.position.x - rightWallOffset){
                transform.position = new Vector3(rightWall.transform.position.x - rightWallOffset, transform.position.y, transform.position.z);
            }
            if (newPosition.x < leftWall.transform.position.x + leftWallOffset){
                transform.position = new Vector3(leftWall.transform.position.x + leftWallOffset, transform.position.y, transform.position.z);
            }
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, leftCam.transform.position, easeInEaseOut(t));
            mainCam2.transform.position = Vector3.Lerp(mainCam2.transform.position, leftCam.transform.position, easeInEaseOut(t));
            yield return new WaitForEndOfFrame();
            t+=Time.deltaTime/10;
        }
        isTurning = false;
        firstTurnLeft = true;
    }
    private IEnumerator turnRight() {
        var t = 0.0f;
        firstTurnRight = false;
        isTurning = true;
        while (t < 1.0f){
            if (newPosition.x > rightWall.transform.position.x - rightWallOffset){
                transform.position = new Vector3(rightWall.transform.position.x - rightWallOffset, transform.position.y, transform.position.z);
            }
            if (newPosition.x < leftWall.transform.position.x + leftWallOffset){
                transform.position = new Vector3(leftWall.transform.position.x + leftWallOffset, transform.position.y, transform.position.z);
            }
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, rightCam.transform.position, easeInEaseOut(t));
            mainCam2.transform.position = Vector3.Lerp(mainCam2.transform.position, rightCam.transform.position, easeInEaseOut(t));
            yield return new WaitForEndOfFrame();
            t+=Time.deltaTime/10;
        }
        isTurning = false;
        firstTurnRight = true;
    }
    private IEnumerator turnLeftLight() {
        var t = 0.0f;
        firstTurnLeft = false;
        isTurning = true;
        while (t < 1.0f){
            if (newPosition.x > rightWall.transform.position.x - rightWallOffset){
                transform.position = new Vector3(rightWall.transform.position.x - rightWallOffset, transform.position.y, transform.position.z);
            }
            if (newPosition.x < leftWall.transform.position.x + leftWallOffset){
                transform.position = new Vector3(leftWall.transform.position.x + leftWallOffset, transform.position.y, transform.position.z);
            }
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, leftCam.transform.position, easeInEaseOut(t));
            mainCam2.transform.position = Vector3.Lerp(mainCam2.transform.position, leftCam.transform.position, easeInEaseOut(t));
            yield return new WaitForEndOfFrame();
            t+=Time.deltaTime/10;
        }
        isTurning = false;
        firstTurnLeft = true;
    }
    private IEnumerator turnRightLight() {
        var t = 0.0f;
        firstTurnRight = false;
        isTurning = true;
        while (t < 1.0f){
            if (newPosition.x > rightWall.transform.position.x - rightWallOffset){
                transform.position = new Vector3(rightWall.transform.position.x - rightWallOffset, transform.position.y, transform.position.z);
            }
            if (newPosition.x < leftWall.transform.position.x + leftWallOffset){
                transform.position = new Vector3(leftWall.transform.position.x + leftWallOffset, transform.position.y, transform.position.z);
            }
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, rightCam.transform.position, easeInEaseOut(t));
            mainCam2.transform.position = Vector3.Lerp(mainCam2.transform.position, rightCam.transform.position, easeInEaseOut(t));
            yield return new WaitForEndOfFrame();
            t+=Time.deltaTime/10;
        }
        isTurning = false;
        firstTurnRight = true;
    }
}
