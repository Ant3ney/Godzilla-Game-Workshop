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

    public float maxHealthBarWidth = 211.5548f;
    public float maxHealth = 100f;
    public float health = 100f;

    public bool isReactingToHit = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GlueScript.onDamageMechaGodzilla += takeDamage;
        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("Godzilla").transform;
        }
        if (playerTransform != null)
        {
            Debug.Log("Moving MechaGodzilla to Godzilla. Starting position: " + transform.position);
        }
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null && !isReactingToHit)
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
        else if (isReactingToHit)
        {
            agent.isStopped = true;
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

    public void takeDamage(float damage)
    {

        health -= damage;
        updateHealthBar(health);
        if (health <= 0)
        {
            //    Navigator.navigateToStatic("Victory");
        }
        if (isReactingToHit)
        {
            return;
        }
        animator.SetTrigger("hit");
        isReactingToHit = true;
    }

    public void updateHealthBar(float health)
    {
        float healthBarRatio = health / maxHealth;
        if (healthBarRatio <= 0)
        {
            healthBarRatio = 0;
        }
        float newHealthBarWidth = maxHealthBarWidth * healthBarRatio;
        GlueScript.updateMechaGodzillaUIEvent(healthBarRatio);
        //RectTransform healthLeft = enemy_health_left_bar.GetComponent<RectTransform>();
        //healthLeft.localScale = new Vector3(healthBarRatio, healthLeft.localScale.y, healthLeft.localScale.z);
    }


    public void doneReactingToHit()
    {
        isReactingToHit = false;
    }
}
