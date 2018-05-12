using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : MonoBehaviour {
	private static SSDirector _instance;
	private FirstSceneController firstSceneController;

	public static SSDirector getInstance() {
		return _instance;
	}

	void Awake(){
		_instance = this;
	}

	void Start () {
		firstSceneController = FirstSceneController.getInstance ();
	}
}
