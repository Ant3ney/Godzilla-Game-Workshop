using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHealthBars : MonoBehaviour
{
    public bool isGodzilla = false;
    // Start is called before the first frame update
    void Start()
    {
        if (isGodzilla)
        {
            GlueScript.onUpdateGodzillaUI += updateHealthBar;
        }
        else
        {
            GlueScript.onUpdateMechaGodzillaUI += updateHealthBar;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateHealthBar(float ratio)
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.localScale = new Vector3(ratio, rt.localScale.y, rt.localScale.z);
    }
}
