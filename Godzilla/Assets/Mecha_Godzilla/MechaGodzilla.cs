using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MechaGodzilla : MonoBehaviour
{
    public Transform playerTransform; // Assign the player's transform in the inspector
    private NavMeshAgent agent;
    public float shootDistance = 10f;

    public Transform missilePoint;

    Animator animator;

    public GameObject missilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            if (distance <= shootDistance)
            {
                agent.isStopped = true;
                lookAtPlayer();
                //animator.SetTrigger("idle");
                animator.SetTrigger("shoot");
                // Shoot
                // Instantiate(attackCollision, transform.position, Quaternion.identity);
            }
            else
            {
                agent.isStopped = false;
                animator.SetTrigger("run");
            }
            agent.SetDestination(playerTransform.position);
        }
    }

    void lookAtPlayer()
    {
        // Calculate direction from AI to player
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0; // Ignore the Y-axis difference

        // Check if direction is not zero
        if (direction != Vector3.zero)
        {
            // Create a rotation looking in the direction of the player
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Set the AI's rotation to this new rotation only on the Y-axis
            transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
        }

    }

    public void shoot()
    {
        GameObject missile = Instantiate(missilePrefab, missilePoint.position, Quaternion.identity);
        missile.GetComponent<Missle>().playerTransform = playerTransform;
    }
}
