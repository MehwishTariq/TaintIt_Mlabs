using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public static Color forReticle;
    public Material mat;
    void Start()
    {
        forReticle = new Color(Random.value, Random.value, Random.value, 1.0f);
        if(GetComponent<SpriteRenderer>() != null)
            GetComponent<SpriteRenderer>().color = forReticle;
    }
    private void OnEnable()
    {
        if (GetComponent<ParticleSystem>() != null)
        {
            mat.color = forReticle;
        }
    }
}
