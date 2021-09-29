using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 50f;

    private Transform targetTransform;
    private GameObject target;
    private NavMeshAgent agent;

    private bool alreadyHit;
 
    // Start is called before the first frame update
    void Start()
    {
        alreadyHit = false;
        target = PlayerManager.instance.player;
        targetTransform = target.transform;
        agent = GetComponent<NavMeshAgent>();  
        
        NavMeshHit closestHit;
 
        if (NavMesh.SamplePosition(agent.transform.position, out closestHit, 500f, NavMesh.AllAreas))
            agent.transform.position = closestHit.position;
        else
            Debug.LogError("Could not find position on NavMesh!"); 

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(targetTransform.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(targetTransform.position);
            if (distance <= 3f && !alreadyHit)
            {
                StartCoroutine(Hit(this.tag));
            }
        }

    }

    IEnumerator Hit(string tag)
    {
        alreadyHit = true;
        if (tag == "Donut")
            PlayerManager.instance.HitByDonut();
        else
            PlayerManager.instance.HitByEnemy();
        yield return new WaitForSeconds(2);
        alreadyHit = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
