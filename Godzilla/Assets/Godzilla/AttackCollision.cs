using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public CapsuleCollider attackCollider;
    public GameObject destructionSpoke;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        //speed = speed * -1;
    }
}
