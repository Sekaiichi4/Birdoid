using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraOrbit : MonoBehaviour
{
    public float speed, distance;
    public Transform target;
    Vector2 input;
    public Joystick joystick;
    public Slider slider;

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

        if(Input.GetKey(KeyCode.W))
        {
            transform.position -= transform.position.normalized;
            distance = Vector3.Distance(transform.position, Vector3.zero);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.position += transform.position.normalized;
            distance = Vector3.Distance(transform.position, Vector3.zero);
        }
    }

    public void Zoom()
    {
        transform.position = transform.position.normalized * slider.value;
        distance = Vector3.Distance(transform.position, Vector3.zero);
    }
}
