using UnityEngine;
using System.Collections;

public class AudioPlaying : MonoBehaviour {

	private static AudioPlaying instance = null;

	public static AudioPlaying Instance {
		get { return instance; }
	}

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}


	// any other methods you need

}
