using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class OpenningCutscene : MonoBehaviour
{
    public string sceneToLoad;
    public float loadTime = 1f;
    public Animator transition;

    public GameObject strong;

    public GameObject enemy;

    public GameObject lantern;

    void Start()
    {
        strong.GetComponent<PlayerMovement>().enabled = false;
        enemy.GetComponent<NavMeshAgent>().enabled = false;
        lantern.GetComponent<GameOver>().enabled= false;
    }

    void Update()
    {
        StartCoroutine(StunTime());
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy") && !other.isTrigger) {
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator StunTime() {
        yield return new WaitForSeconds(5f);
        enemy.GetComponent<NavMeshAgent>().enabled = true;
    }
    private IEnumerator LoadNextScene() {
        // Play Animation
        transition.SetTrigger("Start");
        //GetComponent<PlayerMovement>().stopPlayerMove();
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
