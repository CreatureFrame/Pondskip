using UnityEngine;
using System.Collections;

public class OnLand : MonoBehaviour {

	public PlayerController playercont;

	// Use this for initialization
	void Start () {
	
	}

	public void OnTriggerEnter (Collider col) {

		if (col.gameObject.tag == "Player"){

			playercont.OnWater = false;
			playercont.lastsave = this.gameObject;

		}
	}


	public void OnTriggerExit  (Collider col) {

		if (col.gameObject.tag == "Player"){

			playercont.OnWater = true;

		}
	}
}
