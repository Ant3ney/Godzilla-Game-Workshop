using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Missle : MonoBehaviour
{
    public Transform playerTransform; // Assign the player's transform in the inspector
    private bool fire = false;
    public GameObject explosion;
    public CapsuleCollider attackCollider;
    public GameObject destructionSpoke;

    // Start is called before the first frame update
    void Start()
    {

        attackCollider = GetComponent<CapsuleCollider>();


        // look at player

        if (playerTransform != null)
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

            fire = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fire)
        {
            transform.position += transform.forward * Time.deltaTime * 10f;
        }




    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 center = other.bounds.center;
        // Check if collided with MechaGodzilla
        if (other.gameObject.TryGetComponent(out MechaGodzilla mechaGodzilla))
        {
            return;
        }
        if (other.GetType() == typeof(MeshCollider))
        {
            // Destroy(other.GameObject);
            GameObject collidedObject = other.gameObject;
            BuildingHealth buildingHealth = collidedObject.GetComponent<BuildingHealth>();
            if (buildingHealth != null)
            {
                buildingHealth.health -= 100f;
                Instantiate(destructionSpoke, center, Quaternion.identity);
            }


            if (buildingHealth.health <= 0)
            {
                Destroy(collidedObject);
            }
        }
        else if (other.gameObject.TryGetComponent(out FirstPersonController player))
        {
            // Damage health here
            player.takeDamage(40f);
        }
        // instantiate explosion prefab at hitPosition

        Instantiate(explosion, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
        //speed = speed * -1;
    }

    public void Destroy()
    {
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
