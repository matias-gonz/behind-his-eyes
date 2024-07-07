using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPatrollingScript : MonoBehaviour
{
    [SerializeField] PatrollingScript[] guards;
    [SerializeField] GameObject player;
    private Collider[] playerColliders;
    // Start is called before the first frame update
    void Start()
    {
        playerColliders = player.GetComponents<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerCollider(other))
        {
            foreach (PatrollingScript patrollingScript in guards) 
            {
                patrollingScript.isPatrolling = true;
            }
        }
    }

    private bool isPlayerCollider(Collider other)
    {
        foreach (Collider collider in playerColliders)
        {
            if (collider == other)
            {
                return true;
            }
        }
        return false;
    }
}
