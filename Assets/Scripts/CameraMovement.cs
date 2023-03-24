using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public FixedTouchField TouchField;
    public float turnSpeed;
    public Transform reticle;

    void Update()
    {
        MoveReticle();

    }

    private void MoveReticle()
    {
        Vector2 delta = TouchField.TouchDist;
         float x = delta.x * turnSpeed * Time.deltaTime;
         float y = delta.y * turnSpeed * Time.deltaTime;
         reticle.Translate(x, y, 0);
        if(delta.x !=0 && delta.y !=0)
            reticle.position = new Vector3(Mathf.Clamp(reticle.position.x, -1.5f, 1.5f), Mathf.Clamp(reticle.position.y, 0.13f, 1.67f), -1.94f);
       
    }
}
