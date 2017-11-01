using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 2f;


    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }



    private void Update()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        Vector3 _velocity = (movHorizontal + movVertical).normalized * speed;

        motor.Move(_velocity);


        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

        //Camera Roatation
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;

        motor.RotateCamera(_cameraRotation);

    }

}
