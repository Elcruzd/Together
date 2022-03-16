using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch2D : MonoBehaviour
{
    public string sceneToLoad;
    public float loadTime = 1f;
    public Animator transition;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene() {
        // Play Animation
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
