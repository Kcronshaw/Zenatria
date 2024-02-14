using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPEEEEDBOOST : MonoBehaviour
{

    private float timescale = 1.0f;

    public void ChangeTimescale()
    {
        if (timescale == 1.0f)
        {
            timescale = 2.0f;
            Debug.Log("running1");
            
        }
        else if (timescale == 2.0f)
        {
            timescale = 1.0f;
            Debug.Log("running2");
        }


        Time.timeScale = timescale;
        Debug.Log(Time.timeScale);

    }

}
