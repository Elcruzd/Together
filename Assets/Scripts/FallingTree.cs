using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTree : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
            animator.SetBool("Fall", true);
            FindObjectOfType<AudioManager>().Play("tree_fall");
            //GetComponent<Collider>().isTrigger = false;
        }
    }
}
