using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

	public bool patroling;
	public Vector3 nextPos;
	public GameObject player;
	public float minX;
	public float minZ;
	public float time;

	public delegate void HitPlayer();
	public static event HitPlayer hitPlayerEvent;

	void Start () {
		player = FirstSceneController.getInstance().player;
		minX = this.transform.position.x - 2.5f;
		minZ = this.transform.position.z - 2.5f;
		patroling = true;
		time = 3f;
	}

	void FixedUpdate () {
		if (FirstSceneController.getInstance().gameOver != 0){
			return;
		}

		if (patroling) {
			GoAhead ();
		} else {
			Follow ();
		}
	}

	private void GoAhead(){
		if (time > 2f) {
			float deltaX = Random.Range (0, 10f);
			float deltaZ = Random.Range (0, 10f);
			nextPos = new Vector3 (minX + deltaX, 0, minZ + deltaZ);
			time = 0;
		} else {
			this.transform.position = Vector3.MoveTowards (this.transform.position, nextPos, 2 * Time.deltaTime);
			time += Time.deltaTime;
		}
	}

	private void Follow(){
		if (player != null) {
			nextPos = player.transform.position;
			this.transform.position = Vector3.MoveTowards (this.transform.position, nextPos, 3 * Time.deltaTime);
		}
	}

	void OnCollisionStay(Collision other){
		if (other.gameObject.tag == "Player") {
			if (hitPlayerEvent != null) {
				hitPlayerEvent ();
			}
		}
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Player") {
			Debug.Log (1);
			patroling = false;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			Debug.Log (2);
			patroling = true;
		}
	}

}