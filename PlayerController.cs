using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float nowspeed;
	public float dashspeed;
	public float maxDashTime;
	public float dashStoppingSpeed = 0.1f;
	private float currentDashTime;

	public float goingtosink = 1f;

	public bool OnWater;
	public bool IsDashing;
	public bool Sunk;
	public bool RespawnLoop;


	void Start(){
		currentDashTime = maxDashTime;
	}

	void Update() {
		if (!Sunk){
			Movement();
			Dash();
			if (goingtosink <= 0){
				if (!RespawnLoop){
					Debug.Log("start respawn");
					Respawn();
					RespawnLoop = true;
				}
			}
		}


		//CODE VARIATIONS
		#region CodeVars

		#region Controller Ver2 Xtras
//		http://answers.unity3d.com/questions/803365/make-the-player-face-his-movement-direction.html
//		The way it was done was by replacing transform.rotation = Quaternion.LookRotation(movement); with transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);

//		if(movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
		#endregion


		#region Controller Ver1

		//this one didnt face direction

		//	private Vector3 moveDirection = Vector3.zero;
		//	public CharacterController controller;
//		public float gravity = 20.0F;
//		public float speed = 6.0F;

		//under start
		// Store reference to attached component
		//		controller = GetComponent<CharacterController>();


		//under update
//		// Character is on ground (built-in functionality of Character Controller)
//		if (controller.isGrounded) 
//		{
//			// Use input up and down for direction, multiplied by speed
//			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//			moveDirection = transform.TransformDirection(moveDirection);
//			moveDirection *= speed;
//		}
//		// Apply gravity manually.
//		moveDirection.y -= gravity * Time.deltaTime;
//		// Move Character Controller
//		controller.Move(moveDirection * Time.deltaTime);

		#endregion

		#endregion

	}


	void Movement(){

		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		if(moveHorizontal != 0 || moveVertical != 0){
			transform.rotation = Quaternion.LookRotation(movement);
		}

		if (!OnWater){
			transform.Translate (movement.normalized * nowspeed * Time.deltaTime, Space.World);
		}
		else if (OnWater){
			goingtosink -= 1f * Time.deltaTime;
		}

		if (currentDashTime < maxDashTime)
		{
			Vector3 dashing = new Vector3(0.0f, 0.0f, 1.0f);
			transform.Translate (dashing.normalized * dashspeed * Time.deltaTime, Space.Self);
			currentDashTime += dashStoppingSpeed;
		}

	}


	void Dash(){
		if (Input.GetKeyDown(KeyCode.Space)){
			currentDashTime = 0f;
			goingtosink = 1f;
			Debug.Log("restart sink timer");
		}
	}

	void Respawn(){
		Sunk = true;
		StartCoroutine (SunkRespawn());
	}
		

	public IEnumerator SunkRespawn(){
//		while(!IsDashing){
			Debug.Log("sink start");
			yield return new WaitForSeconds(5.0f);
			Debug.Log("sink end");

			//animation
			//move camera / flash out
			//move player
			//flash in
			//player on of on off

			goingtosink = 1f;
			Sunk = false;
			RespawnLoop = false;
//		}
	}
			



}