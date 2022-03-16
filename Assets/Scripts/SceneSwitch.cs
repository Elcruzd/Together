using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public string sceneToLoad;
    public float loadTime = 1f;
    public Animator transition;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") || other.CompareTag("HoldInteract") && !other.isTrigger) {
            FindObjectOfType<AudioManager>().Play("gate_sound");
            StartCoroutine(LoadNextScene());
        }
    }
    private void OnCollisionStay(Collision other) {
        if(other.gameObject.tag == ("Player") && Input.GetButtonDown("InteractiveLeft")) {
            FindObjectOfType<AudioManager>().Play("gate_sound");
            Debug.Log("nsjoadnasndsakldnjsaondskaljdnas");
            StartCoroutine(LoadNextScene());
        }
    }
    private IEnumerator LoadNextScene() {
        // Play Animation
        transition.SetTrigger("Start");
        //GetComponent<PlayerMovement>().stopPlayerMove();
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
