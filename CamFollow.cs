using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	public GameObject player;
	private float smooth = 5f;
	private Vector3 offset;

	void Start () 
	{
		offset = transform.position - player.transform.position;
	}


	void LateUpdate () 
	{
//		transform.position = player.transform.position + offset;
		transform.position = Vector3.Lerp (
			transform.position, player.transform.position + offset,
			Time.deltaTime * smooth);
	}

}
