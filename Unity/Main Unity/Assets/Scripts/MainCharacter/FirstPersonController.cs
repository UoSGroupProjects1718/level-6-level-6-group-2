using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour {
	
	public float movementSpeed = 5.0f;
	public float mouseSensitivity = 5.0f;
	public float jumpSpeed = 20.0f;
	
	float verticalRotation = 0;
	public float upDownRange = 60.0f;
	
	float verticalVelocity = 0;
	
	CharacterController characterController;

	public Camera firstPersonCamera;
	public Camera overHeadCamera;
	
	// Use this for initialization
	void Start () {
		firstPersonCamera.enabled = true;
		overHeadCamera.enabled = false;
		Screen.lockCursor = true;
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		// Rotation
		if(	firstPersonCamera.enabled == true)
		{
			float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
			transform.Rotate(0, rotLeftRight, 0);


			verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
			verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
			Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


			// Movement

			float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
			float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

			verticalVelocity += Physics.gravity.y * Time.deltaTime;

			if( characterController.isGrounded && Input.GetButton("Jump") ) {
				verticalVelocity = jumpSpeed;
			}

			Vector3 speed = new Vector3( sideSpeed, verticalVelocity, forwardSpeed );

			speed = transform.rotation * speed;


			characterController.Move( speed * Time.deltaTime );


		}

	}

	public void ShowOverHeadCamera() // need a transition for the camera switch
	{
		Screen.lockCursor = false;
		firstPersonCamera.enabled = false;
		overHeadCamera.enabled = true;
	}

	public void ShowFirstPersonCamera()
	{
		Screen.lockCursor = true;
		firstPersonCamera.enabled = true;
		overHeadCamera.enabled = false;
	}
}
