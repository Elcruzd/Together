using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraController2D : MonoBehaviour
{
    public float movementSpeed;
    public float movementTime;
    public Vector3 newPosition;
    public Vector3 playerPosition;
    public GameObject player;
    public Rigidbody2D rb;
    public float cameraOffset;
    public float playerX;
    public GameObject camObj;
    public CinemachineVirtualCamera virtualCamera;
    
    public Vector3 oldPlayerPosition;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        player = GameObject.Find("Older Brother");
        rb = player.GetComponent<Rigidbody2D>();
        playerPosition = player.transform.position;
        playerX = playerPosition.x;
        virtualCamera = camObj.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        if (rb.velocity.x != 0){
            isMoving = true;
        }
        if (rb.velocity.x == 0){
            isMoving = false;
        }
        if (isMoving){
            //virtualCamera.Follow = player.transform;
            newPosition = new Vector3(playerPosition.x, transform.position.y, -10f);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime/10);
        }else{
            if (Input.GetKey(KeyCode.E)) {
                //virtualCamera.Follow = null;
                Debug.Log("im here");
                newPosition += (transform.right * movementSpeed);
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
                newPosition = transform.position;
            }

            if (Input.GetKey(KeyCode.Q)) {
                //virtualCamera.Follow = null;
                newPosition += (transform.right * -movementSpeed);
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
                newPosition = transform.position;
            }
        }
        oldPlayerPosition = playerPosition;
        playerX = playerPosition.x;


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
        if (newPosition.x > playerX + cameraOffset){
            transform.position = new Vector3(playerPosition.x + cameraOffset, transform.position.y, -10f);
        }  
        if (newPosition.x < playerX - cameraOffset){
            transform.position = new Vector3(playerPosition.x - cameraOffset, transform.position.y, -10f);
        }

    }

    // IEnumerator CameraBack(){
    //     newPosition = new Vector3(playerPosition.x, transform.position.y, -10f);
    //     transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime/10);
    //     yield return new WaitForSeconds(0.01f);
    // }
}
