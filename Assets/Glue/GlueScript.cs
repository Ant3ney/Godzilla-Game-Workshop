using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueScript : MonoBehaviour
{

    public delegate void damageMechaGodzilla(float damage);
    public static event damageMechaGodzilla onDamageMechaGodzilla;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void damageMechaGodzillaEvent(float damage)
    {
        if (onDamageMechaGodzilla != null)
        {
            onDamageMechaGodzilla(damage);
        }
    }

    public static void cleanUp()
    {
        onDamageMechaGodzilla = null;
    }
}
