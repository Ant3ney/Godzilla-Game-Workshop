using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueScript : MonoBehaviour
{

    public delegate void damageMechaGodzilla(float damage);
    public static event damageMechaGodzilla onDamageMechaGodzilla;

    public delegate void updateMechaGodzillaUI(float ratio);
    public static event updateMechaGodzillaUI onUpdateMechaGodzillaUI;

    public delegate void updateGodzillaUI(float ratio);
    public static event updateGodzillaUI onUpdateGodzillaUI;

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

    public static void updateMechaGodzillaUIEvent(float ratio)
    {
        if (onUpdateMechaGodzillaUI != null)
        {
            onUpdateMechaGodzillaUI(ratio);
        }
    }

    public static void updateGodzillaUIEvent(float ratio)
    {
        if (onUpdateGodzillaUI != null)
        {
            onUpdateGodzillaUI(ratio);
        }
    }

    public static void cleanUp()
    {
        onDamageMechaGodzilla = null;
    }
}
