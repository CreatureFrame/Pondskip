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

	public GameObject particledash;

	public GameObject lastsave;
	Transform respawnmark;

	float xbordermin = -30;
	float xbordermax = 30;
	float zbordermin = -30;
	float zbordermax = 30;


	void Start(){
		currentDashTime = maxDashTime;
	}

	void Update() {
		if (!Sunk){
			Movement();
			LevelEdges();
			Dash();
			CheckIfDashing();
			if (goingtosink <= 0){
				if (!RespawnLoop){
					Debug.Log("start respawn");
					Respawn();
					RespawnLoop = true;
				}
			}
		}
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


	}

	void CheckIfDashing(){

		if (currentDashTime < maxDashTime)
		{
			Vector3 dashing = new Vector3(0.0f, 0.0f, 1.0f);
			transform.Translate (dashing.normalized * dashspeed * Time.deltaTime, Space.Self);
			currentDashTime += dashStoppingSpeed;
		}
	}

	void LevelEdges(){

		float newX = Mathf.Clamp(transform.position.x, xbordermin, xbordermax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		float newZ = Mathf.Clamp(transform.position.z, zbordermin, zbordermax);
		transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
	}


	void Dash(){
		if (Input.GetKeyDown(KeyCode.Space)){
			DashParts();
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

		respawnmark = lastsave.gameObject.transform.GetChild(0);
		transform.position = respawnmark.transform.position;

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


	void DashParts(){
		GameObject instanParts;
		instanParts = (GameObject) Instantiate(particledash, transform.position, transform.rotation);
		Destroy(instanParts, 1f);
	}
			



}