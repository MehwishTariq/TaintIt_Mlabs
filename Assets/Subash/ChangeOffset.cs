using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOffset : MonoBehaviour
{
    Material m;
    [SerializeField]
    float moveSpeed;
    private void OnEnable()
    {
        m = GetComponent<SpriteRenderer>().material;
       
    }

    private void Update()
    {
        m.SetTextureOffset("_MainTex", new Vector2(0, m.GetTextureOffset("_MainTex").y + moveSpeed * Time.deltaTime));
    }
}
