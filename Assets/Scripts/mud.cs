using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mud : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        Debug.Log("mud mud mud");
        if (other.tag == "Player"){
            FindObjectOfType<PlayerMovement>().inMud();
        }
    }
    void OnTriggerExit(Collider other){
        if (other.tag == "Player"){
            FindObjectOfType<PlayerMovement>().outMud();
        }
    }
}
