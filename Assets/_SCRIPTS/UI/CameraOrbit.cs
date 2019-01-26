using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public float speed, distance;
    public Transform target;
    Vector2 input;
    public Joystick joystick;

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            input += new Vector2(joystick.Horizontal * -speed, joystick.Vertical * speed);

            Quaternion mRot = Quaternion.Euler(input.y, input.x, 0);

            Vector3 mPos = target.position - (mRot * Vector3.forward * distance);

            transform.localRotation = mRot;
            transform.localPosition = mPos; 
        }
    }
}
