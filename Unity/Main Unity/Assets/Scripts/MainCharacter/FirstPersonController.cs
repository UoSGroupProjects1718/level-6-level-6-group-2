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

    public bool lockCursor = true;

    public bool onPuzzle;



    private bool m_cursorIsLocked = true;

    // Use this for initialization
    void Start () {

		Screen.lockCursor = true;
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {


		if(firstPersonCamera.enabled == true)
	{

            //if the player is not on the puzzle platform, rotate camera normally
            if (!onPuzzle)
            {

                // Rotation
                float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
                transform.Rotate(0, rotLeftRight, 0);


                verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
                verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
                Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

                // Movement

                float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
                float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

                verticalVelocity += Physics.gravity.y * Time.deltaTime;

                if (characterController.isGrounded && Input.GetButton("Jump"))
                {
                    verticalVelocity = jumpSpeed;
                }

                Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);

                speed = transform.rotation * speed;


                characterController.Move(speed * Time.deltaTime);
            }
            if (onPuzzle && Input.GetKey(KeyCode.Mouse2))
            {
                // Rotation
                float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
                transform.Rotate(0, rotLeftRight, 0);


                verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
                verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
                Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

                // Movement

                float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
                float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

                verticalVelocity += Physics.gravity.y * Time.deltaTime;

                if (characterController.isGrounded && Input.GetButton("Jump"))
                {
                    verticalVelocity = jumpSpeed;
                }

                Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);

                speed = transform.rotation * speed;


                characterController.Move(speed * Time.deltaTime);

            }



            

		}

	}
    public void SetCursorLock(bool value)
    {
        lockCursor = value;
      //  if (!lockCursor)
       // {//we force unlock the cursor if the user disable the cursor locking helper
      //      Cursor.lockState = CursorLockMode.Confined;
      //      Cursor.visible = true;
      //  }

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            mouseSensitivity = 5;
            Cursor.visible = false;
        }
        else if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            mouseSensitivity = 8;
            Cursor.visible = true;
        }


    }



    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }


}
