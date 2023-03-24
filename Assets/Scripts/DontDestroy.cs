using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy bgInstance;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (bgInstance == null)
        {
            bgInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
}
