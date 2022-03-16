using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMove : MonoBehaviour
{
    public GameObject strong;
    public Vector3 strongPos;
    public SpriteRenderer sprite;
    public Animator animator;
    public float offset;
    public float leftSpeed;
    public float rightSpeed;
    public bool isFollowing = false;
    public bool isRandom = false;
    public Rigidbody rb;
    public Transform throwPoint;
    public float throwRange = 0.5f;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        strong = GameObject.Find("Strong");
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        strongPos = strong.transform.position;
        if (this.transform.position.x > strong.transform.position.x + offset && !isFollowing){
            StartCoroutine("followLeft");
        } else if (this.transform.position.x < strong.transform.position.x - offset && !isFollowing){
            StartCoroutine("followRight");
        }
        float rng = Random.Range(0.0f, 10000.0f);
        // if (!isFollowing && !isRandom){
        //     if (rng < 100.0f){
        //         StartCoroutine("randomWalk");
        //     }
        // }
        if (rb.velocity.x == 0){
            if (this.transform.position.x > strong.transform.position.x && !isFollowing){
                sprite.flipX = true;
            } else if (this.transform.position.x < strong.transform.position.x && !isFollowing){
                sprite.flipX = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            Throw();
        }

        throwPoint.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    void Throw(){
        Collider[] hitEnemies = Physics.OverlapSphere(throwPoint.position, throwRange, enemyLayers);
    
        foreach(Collider enemy in hitEnemies) {
            Debug.Log("We hit");
            FindObjectOfType<AudioManager>().Play("Shadow_gets_hit_frustriate");
            enemy.GetComponent<EnemyController>().TakeDamage(1f);
        }
    }

    void OnDrawGizmosSelected(){
        if (throwPoint == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(throwPoint.position, throwRange);

    }

    private IEnumerator followLeft(){
        if(isRandom){
            StopCoroutine("randomWalk");
            isRandom = false;
        }
        float stopOffset = Random.Range(0f,0.5f);
        animator.SetFloat("Speed", leftSpeed);
        isFollowing = true;
        faceLeft();
        while(this.transform.position.x > strong.transform.position.x + stopOffset){
            float randomspeed = Random.Range(strong.GetComponent<PlayerMovement>().leftSpeed - 0.5f,strong.GetComponent<PlayerMovement>().leftSpeed + 0.5f);
            rb.velocity = new Vector3(-randomspeed, rb.velocity.y, rb.velocity.z);
            yield return new WaitForEndOfFrame();
        }
        rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);
        isFollowing = false;
        animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(0f);
    }
    private IEnumerator followRight(){
        if(isRandom){
            StopCoroutine("randomWalk");
            isRandom = false;
        }
        float stopOffset = Random.Range(0f,0.5f);
        animator.SetFloat("Speed", rightSpeed);
        isFollowing = true;
        faceRight();
        while(this.transform.position.x < strong.transform.position.x - stopOffset){
            float randomspeed = Random.Range(strong.GetComponent<PlayerMovement>().rightSpeed - 0.5f,strong.GetComponent<PlayerMovement>().rightSpeed + 0.5f);
            rb.velocity = new Vector3(randomspeed, rb.velocity.y, rb.velocity.z);
            yield return new WaitForEndOfFrame();
        }
        //rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);
        isFollowing = false;
        animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(0f);
    }

    // dont need this anymore
    private IEnumerator randomWalk(){
        isRandom = true;
        float rngwalk = Random.Range(0.3f, 0.7f);
        float destPos = this.transform.position.x + rngwalk;
        faceRight();
        while(destPos > this.transform.position.x){
            animator.SetFloat("Speed", rightSpeed);
            rb.velocity = new Vector3(1f, rb.velocity.y, rb.velocity.z);
            yield return new WaitForEndOfFrame();
            Debug.Log("random walking ");
        }
        rb.velocity = new Vector3(0, 0, 0);
        animator.SetFloat("Speed", 0);
        yield return new WaitForSecondsRealtime(2f);
        faceLeft();
        while(destPos - rngwalk < this.transform.position.x){
            animator.SetFloat("Speed", rightSpeed);
            rb.velocity = new Vector3(-1f, rb.velocity.y, rb.velocity.z);
            yield return new WaitForEndOfFrame();
            Debug.Log("random walking back ");
        }
        rb.velocity = new Vector3(0, 0, 0);
        animator.SetFloat("Speed", 0);
        isRandom = false;
    }

    private void faceLeft(){
        sprite.flipX = true;
        animator.Play("Small_Idle");
    }
    private void faceRight(){
        sprite.flipX = false;
        animator.Play("Small_Idle");
    }
}
