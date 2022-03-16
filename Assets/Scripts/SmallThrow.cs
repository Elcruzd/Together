// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SmallThrow : MonoBehaviour
// {
//     public Transform throwPoint;
//     public float throwRange = 0.5f;
//     public LayerMask enemyLayers;

//     void Start()
//     {

//     }
    
//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.T)) {
//             Throw();
//         }
//         throwPoint.position = new Vector3(small.transform.position.x, small.transform.position.y, small.transform.position.z);
//     }

//     void Throw()
//     {
//         Collider[] hitEnemies = Physics.OverlapSphere(throwPoint.position, throwRange, enemyLayers);
        
//         foreach(Collider enemy in hitEnemies) {
//             Debug.Log("We hit");
//         }
//     }

//     void OnDrawGizmosSelected()
//     {
//         if (throwPoint == null)
//             return;

//         Gizmos.color = Color.green;
//         Gizmos.DrawWireSphere(throwPoint.position, throwRange);

//     }
// }
