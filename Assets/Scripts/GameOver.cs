using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string sceneToLoad;
    public float loadTime = 1f;
    public Animator transition;
    public GameObject strong;
    public Transform GameOverPoint;
    // public Sprite image;
    public Transform lantern;
    float inputHorizontal;
    // bool facingRight = true;

    void Start()
    {
        strong = GameObject.Find("Strong");
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        GameOverPoint.transform.position = lantern.transform.position;

        if (inputHorizontal > 0) {
            lantern.transform.position = new Vector3(strong.transform.position.x + 0.5f, strong.transform.position.y + 0.1f, strong.transform.position.z);
        }

        if (inputHorizontal < 0) {
            lantern.transform.position = new Vector3(strong.transform.position.x - 0.5f, strong.transform.position.y + 0.1f, strong.transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy") && !other.isTrigger) {
            StartCoroutine(LoadNextScene());
        }
    }

    // void Flip() {
    //     Vector3 currentScale = gameObject.transform.localScale;
    //     currentScale.x *= -1;
    //     gameObject.transform.localScale = currentScale;

    //     facingRight = !facingRight;
    // }
    private IEnumerator LoadNextScene() {
        // Play Animation
        transition.SetTrigger("Start");
        //GetComponent<PlayerMovement>().stopPlayerMove();
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
