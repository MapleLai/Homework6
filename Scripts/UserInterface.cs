using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {
		if (FirstSceneController.getInstance().gameOver != 0){
			return;
		}

		float translationX = Input.GetAxis ("Horizontal") * 6;
		float translationZ = Input.GetAxis ("Vertical") * 6;
		translationX *= Time.deltaTime;
		translationZ *= Time.deltaTime;
		this.transform.Translate (translationX, 0, 0);
		this.transform.Translate (0, 0, translationZ);
	}
}
