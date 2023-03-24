using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnim : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !other.GetComponent<EnemyMovement>().dead)
        {
            other.GetComponent<Animator>().Play("Wipe");
        }

    }
}
