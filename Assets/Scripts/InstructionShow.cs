using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionShow : MonoBehaviour
{
    public GameObject instruction;

    // Start is called before the first frame update
    void Start()
    {
        instruction.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            this.instruction.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.GetComponent<EnemyController>()) {
            Debug.Log("trigger enter");
            this.instruction.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.GetComponent<EnemyController>()) {
            // Debug.Log("trigger exit");
            this.instruction.SetActive(false);
        }
    }
}
