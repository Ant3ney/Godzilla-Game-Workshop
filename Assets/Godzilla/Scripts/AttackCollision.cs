using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public CapsuleCollider attackCollider;
    public GameObject destructionSpoke;

    public float maxTime = 0.125f;
    public float time = 0f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= maxTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(MeshCollider))
        {
            // Destroy(other.GameObject);
            GameObject collidedObject = other.gameObject;
            BuildingHealth buildingHealth = collidedObject.GetComponent<BuildingHealth>();
            if (buildingHealth == null)
            {
                return;
            }
            buildingHealth.health -= 100f;
            Vector3 center = other.bounds.center;
            Instantiate(destructionSpoke, center, Quaternion.identity);
            if (buildingHealth.health <= 0)
            {
                Destroy(collidedObject);
            }
        }
        else if (other.GetType() == typeof(CapsuleCollider))
        {
            //Event_System.takeDamage(1, "player");
            GlueScript.damageMechaGodzillaEvent(1f);
        }
        //speed = speed * -1;
    }
}
