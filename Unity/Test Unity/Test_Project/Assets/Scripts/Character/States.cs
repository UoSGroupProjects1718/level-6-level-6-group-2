using UnityEngine;
using System.Collections;

public class States : MonoBehaviour {

	public enum State
	{
		Walk,
		WalkBackwards,
		StrafeLeft,
		StrafeRight,
		Jump,
		Die,
		Idle,
	}

	public State state;

	IEnumerator IdleState()
	{
		Debug.Log ("Idle:Enter");
		while (state == State.Idle) {
			yield return 0;
		}
		Debug.Log ("Idle:Exit");
		NextState ();
	}

	IEnumerator WalkState()
	{						
		GetComponent<Animation>().CrossFade ("walk_forward");
		
		Debug.Log ("Walk:Enter");
		while (state == State.Walk) {
			yield return 0;
		}
		Debug.Log ("Walk:Exit");
		NextState ();
	}

	IEnumerator WalkBackwardsState()
	{
		GetComponent<Animation>().CrossFade ("walk_backward");
		Debug.Log ("WalkBackwards:Enter");
		while (state == State.WalkBackwards) {
			yield return 0;
		}
		Debug.Log ("WalkBackwards:Exit");
		NextState ();
	}

	IEnumerator StrafeLeftState()
	{
		GetComponent<Animation>().CrossFade ("walk_left");
		Debug.Log ("LStrafe:Enter");
		while (state == State.StrafeLeft) {
			yield return 0;
		}
		Debug.Log ("LStrafe:Exit");
		NextState ();
	}

	IEnumerator StrafeRightState()
	{
		GetComponent<Animation>().CrossFade ("walk_right");
		Debug.Log ("RStrafe:Enter");
		while (state == State.StrafeRight) {
			yield return 0;
		}
		Debug.Log ("RStrafe:Exit");
		NextState ();
	}

	IEnumerator JumpState()
	{
		GetComponent<Animation>().CrossFade ("jump");
		Debug.Log ("Jump:Enter");
		while (state == State.Jump) {
			yield return 0;
		}
		Debug.Log ("Jump:Exit");
		NextState ();
	}

	IEnumerator DieState()
	{
		Debug.Log ("Die:Enter");
		while (state == State.Die) {
			yield return 0;
		}
		Debug.Log ("Die:Exit");
		NextState ();
	}


	// Use this for initialization
	void Start () {
		NextState ();
	
	}

	void NextState (){
		string methodName = state.ToString () + "State";
		System.Reflection.MethodInfo info =
			GetType ().GetMethod (methodName,
			                    System.Reflection.BindingFlags.NonPublic |
			System.Reflection.BindingFlags.Instance);

		StartCoroutine((IEnumerator)info.Invoke(this, null));
	}
	
	// Update is called once per frame
	void Update () {

		
		
		if (Input.GetKey (KeyCode.S)) {
			WalkBackwardsState ();
		}
		if (Input.GetKey (KeyCode.W)) 
		{
			WalkState();
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			StrafeLeftState();
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			StrafeRightState();
		}
		
		if (Input.GetKey (KeyCode.Space)) 
		{
			JumpState ();
		}
		
		else
		{
			IdleState();
			GetComponent<Animation>().CrossFade("examine");
		}
	
	}
}
