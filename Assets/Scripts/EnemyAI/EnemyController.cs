using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public float distance;
    public float lookRadius = 5f;
    public float smallRadius = 5f;
    private float speed;
    private float currentSpeed;
    public float stunTime = 1f;

    public bool isAngered;
    public bool isAngeredBlue;
    public Animator animator;
    public Animator handAnim;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentSpeed = agent.speed;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Player.transform.position, this.transform.position);

        if (distance <= lookRadius) {
            isAngered = true;
        }
        if (distance > lookRadius) {
            isAngered = false;
        }
        if (distance <= smallRadius) {
            isAngeredBlue = true;
        }
        if (distance > smallRadius) {
            isAngeredBlue = false;
        }

        if (isAngered) {
            // agent.isStopped = false;
            agent.SetDestination(Player.transform.position);
            agent.speed = 4f;
            animator.SetBool("EnemyMove", true);
        }
        if (isAngeredBlue) {
            // Debug.Log("in area");
            lookRadius = 0f;
            agent.SetDestination(Player.transform.position);
            agent.speed = 2f;
        }
        if (!isAngeredBlue) {
            lookRadius = 35f;
            agent.speed = 4f;
        }
        if (agent.isStopped) {
            animator.SetBool("EnemyMove", false);
        }
    }

    public void TakeDamage(float slowSpeed)
    {
        currentSpeed -= slowSpeed;

        if (currentSpeed <= 1) {
            agent.isStopped = true;
        }
        if (agent.isStopped) {
            StartCoroutine(StunTime());
        }
    }
    private IEnumerator StunTime() {
        yield return new WaitForSeconds(stunTime);
        agent.ResetPath();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, smallRadius);
    }
}
