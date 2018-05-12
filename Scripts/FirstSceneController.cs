using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour {

	private static FirstSceneController _instance;
	private GameObject map;
	public GameObject player;
	private PatrolFactory patrolFactory;
	public int gameOver;

	public static FirstSceneController getInstance() {
		return _instance;
	}
		
	void Awake(){
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		gameOver = 0;
		patrolFactory = PatrolFactory.getInstance();
		LoadResources ();
		this.transform.parent = player.transform;

		Patrol.hitPlayerEvent += Over ;
	}
	
	// Update is called once per frame
	void Update () {
		if (patrolFactory.check ())
			gameOver = 1;
	}

	public void LoadResources() {
		map = Instantiate(Resources.Load("Prefabs/Map"), Vector3.zero, Quaternion.identity) as GameObject;
		player = Instantiate(Resources.Load("Prefabs/Player"),new Vector3(0, 1, -3), Quaternion.identity) as GameObject;
		player.AddComponent<UserInterface>();
		patrolFactory.ProducePatrol ();
	}

	public void Over(){
		gameOver = -1;
	}

	void OnGUI(){
		if (gameOver == -1) {
			GUIStyle style = new GUIStyle();
			style.normal.background = null;
			style.normal.textColor = new Color(0, 0, 0);
			style.fontSize = 40;
			GUI.TextField (new Rect (300, 160, 350, 50), "You Lose!", style);
		}
		if (gameOver == 1) {
			GUIStyle style = new GUIStyle();
			style.normal.background = null;
			style.normal.textColor = new Color(0, 0, 0);
			style.fontSize = 40;
			GUI.TextField (new Rect (300, 160, 350, 50), "You Win!", style);
		}
	}
}
