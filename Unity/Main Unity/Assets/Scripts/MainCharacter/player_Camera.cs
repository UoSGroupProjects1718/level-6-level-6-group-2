using UnityEngine;
using System.Collections;

public class player_Camera : MonoBehaviour 
{

	public static player_Camera Instance;
	public Transform TargetLookAt;

	public float distance = 5f;
	public float distanceMin = 3f;
	public float distanceMax = 10f;
	public float distanceSmooth = 0.1f;
	public float distanceResetSmooth = 1f;


	//player input
	public float x_Mouse_Sensitivity = 5f;
	public float y_Mouse_Sensitivity = 5f;
	public float mouseWheelSensitivity = 5f;
	public float X_Smooth = 0.05f;
	public float Y_Smooth = 0.1f;

	public float y_MinLimit = 20f;
	public float x_MinLimit = 80f;
	public float y_MaxLimit = 20f;
	public float x_MaxLimit = 80f;
	public float occlusionDistStep = 0.5f;
	public int maxOcclusionCheck = 10; // number of times iterating through check


	private float mouseX = 0f;
	private float mouseY = 0f;
	private float velocX = 0f;
	private float velocY = 0f;
	private float velocZ = 0f;
	private float velDistance = 0f;
	private float startDistance = 0f;
	private Vector3 desiredPosition = Vector3.zero;
	private float desiredDistance = 0f;
//	private float distanceSmooth = 0f;
	private float preOccludedDistance = 0;

	private Vector3 camPosition = Vector3.zero;



	void Awake()
	{
		Instance = this;
	}

	void Start () 
	{
	//need to validate our distance
		distance = Mathf.Clamp(distance, distanceMin, distanceMax); // take our current distance, and make sure its set between min & max dist
		startDistance = distance;
		Reset ();

	}
	

	void LateUpdate () 
	{
		//validate we have a targetLookAt
		if (TargetLookAt == null)
		{
			return;
		}
		HandlePlayerInput ();

		var count = 0;
		do {
			CalculateDesiredPosition ();
			count++;
		} while(CheckIfOccluded (count));


		UpdatePosition ();
	}

	void HandlePlayerInput()
	{
		var deadZone = 0.1f;



		if (Input.GetMouseButton (1)) 
		{
			//the RMB is down, get mouse axis input
			mouseX += Input.GetAxis("Mouse X") * x_Mouse_Sensitivity;
			mouseY -= Input.GetAxis("Mouse Y") * y_Mouse_Sensitivity;
		}

		// clamp/limit mouse y rotation

		mouseY = helper.ClampAngle (mouseY, y_MinLimit, y_MaxLimit);

		if (Input.GetAxis ("Mouse ScrollWheel") < -deadZone || Input.GetAxis ("Mouse ScrollWheel") > -deadZone) 
		{
			desiredDistance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * mouseWheelSensitivity, 
			                              distanceMin, distanceMax);


		}
	}
	void CalculateDesiredPosition()
	{
		//evaluate distance based on smoothing calculation
		distance = Mathf.SmoothDamp (distance, desiredDistance, ref velDistance, distanceSmooth);

		//calculate desired position
		desiredPosition = CalculatePosition (mouseY, mouseX, distance);

	}

	Vector3 CalculatePosition(float rotationX, float rotationY, float distancePos)
	{
		Vector3 direction = new Vector3 (0, 0, -distancePos);
		Quaternion rotation = Quaternion.Euler (rotationX, rotationY, 0);
		return TargetLookAt.position + rotation * direction; // returns desired position offset using target 
	}

	bool CheckIfOccluded(int count)
	{
		var isOccluded = false;

		var nearestDist = CheckCameraPoints (TargetLookAt.position, desiredPosition);

		if(nearestDist != -1)
		{
			if (count < maxOcclusionCheck) {
				isOccluded = true;
				distance -= occlusionDistStep;

				if (distance < 0.25f)
					distance = 0.25f;
			} else
				distance = nearestDist - Camera.main.nearClipPlane;

			desiredDistance = distance;
		}
			

		return isOccluded;
	}

	float CheckCameraPoints(Vector3 from, Vector3 to)
	{
		var nearDist = -1f;

		RaycastHit hitInfo;
		helper.ClipPlanePoints clipPoints = helper.ClipPlaneAtNear (to);

		//draw lines in editor to visualise
		Debug.DrawLine(from, to + transform.forward * -GetComponent<Camera>().nearClipPlane, Color.red);
		Debug.DrawLine(from, clipPoints.UpLeft);
		Debug.DrawLine(from, clipPoints.UpRight);
		Debug.DrawLine(from, clipPoints.LowLeft);
		Debug.DrawLine(from, clipPoints.LowRight);
		
			Debug.DrawLine(clipPoints.UpLeft, clipPoints.UpRight);
			Debug.DrawLine(clipPoints.UpRight, clipPoints.LowRight);
			Debug.DrawLine(clipPoints.LowRight, clipPoints.LowLeft);
			Debug.DrawLine(clipPoints.LowLeft, clipPoints.UpLeft);

		if (Physics.Linecast (from, clipPoints.UpLeft, out hitInfo) && hitInfo.collider.tag != "Player")
			nearDist = hitInfo.distance;
		
		if (Physics.Linecast (from, clipPoints.LowLeft, out hitInfo) && hitInfo.collider.tag != "Player")
			if(hitInfo.distance < nearDist || nearDist == -1)
				nearDist = hitInfo.distance;

		if (Physics.Linecast (from, clipPoints.UpRight, out hitInfo) && hitInfo.collider.tag != "Player")
			if(hitInfo.distance < nearDist || nearDist == -1)
				nearDist = hitInfo.distance;

		if (Physics.Linecast (from, clipPoints.LowRight, out hitInfo) && hitInfo.collider.tag != "Player")
			if(hitInfo.distance < nearDist || nearDist == -1)
				nearDist = hitInfo.distance;

		if (Physics.Linecast (from, to + transform.forward * -GetComponent<Camera>().nearClipPlane, out hitInfo) && hitInfo.collider.tag != "Player")
			if(hitInfo.distance < nearDist || nearDist == -1)
				nearDist = hitInfo.distance;

		return nearDist;
	}

	void UpdatePosition()
	{
		var posX = Mathf.SmoothDamp (camPosition.x, desiredPosition.x, ref velocX, X_Smooth);
		var posY = Mathf.SmoothDamp (camPosition.y, desiredPosition.y, ref velocY, Y_Smooth);
		var posZ = Mathf.SmoothDamp (camPosition.z, desiredPosition.z, ref velocZ, X_Smooth);
		camPosition = new Vector3 (posX, posY, posZ);

		transform.position = camPosition;
		transform.LookAt (TargetLookAt);


	}

	void Reset()
	{
		mouseX = 0;
		mouseY = 10;
		distance = startDistance;
		desiredDistance = distance;
	}

	public static void UseExcistingOrCreateCamera()
	{
		GameObject tempCamera;
		GameObject targetLookAt;
		player_Camera myCamera;

		if (Camera.main != null) { // if not = to null
			tempCamera = Camera.main.gameObject; // the temp cam is the main cam
		} 
		else
		{
			tempCamera = new GameObject("Main Camera"); // make cam
			tempCamera.AddComponent<Camera>(); // get cam componennt
			tempCamera.tag = "Main Camera"; // add tag

		}
		tempCamera.AddComponent<player_Camera>(); // get script component
		myCamera = tempCamera.GetComponent("tp_Camera")as player_Camera; // cast as matching type

		targetLookAt = GameObject.Find ("targetLookAt") as GameObject;

		if (targetLookAt == null) 
		{
			targetLookAt = new GameObject("targetLookAt");
			targetLookAt.transform.position = Vector3.zero;
		}

		myCamera.TargetLookAt = targetLookAt.transform; 

	}


}
