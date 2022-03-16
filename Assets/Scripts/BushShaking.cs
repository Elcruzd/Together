using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushShaking : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        // GetComponent<Collider>().isTrigger = true;
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
            animator.SetBool("Shake", true);
            FindObjectOfType<AudioManager>().Play("Owl");
            // GetComponent<Collider>().isTrigger = false;
        }
    }
}
