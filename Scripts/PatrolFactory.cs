using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory : MonoBehaviour{

	private static PatrolFactory _instance;
	public List<GameObject> patrols = new List<GameObject> ();
	private GameObject newPatrol;

	public static PatrolFactory getInstance() {
		if (_instance == null) {
			_instance = new PatrolFactory();
		} 
		return _instance;
	}

	public void ProducePatrol(){
		newPatrol = Instantiate(Resources.Load("Prefabs/Patrol"),new Vector3(-12, 1, 11), Quaternion.identity) as GameObject;
		newPatrol.AddComponent<Patrol>();
		patrols.Add (newPatrol);
		newPatrol = Instantiate(Resources.Load("Prefabs/Patrol"),new Vector3(2, 1, 9), Quaternion.identity) as GameObject;
		newPatrol.AddComponent<Patrol>();
		patrols.Add (newPatrol);
		newPatrol = Instantiate(Resources.Load("Prefabs/Patrol"),new Vector3(12, 1, -5), Quaternion.identity) as GameObject;
		newPatrol.AddComponent<Patrol>();
		patrols.Add (newPatrol);
		newPatrol = Instantiate(Resources.Load("Prefabs/Patrol"),new Vector3(-12, 1, -13), Quaternion.identity) as GameObject;
		newPatrol.AddComponent<Patrol>();
		patrols.Add (newPatrol);
	}

	public bool check(){
		foreach (GameObject i in patrols){
			float posX = i.transform.position.x;
			float posZ = i.transform.position.z;
			if (posX < -4f || posX > 5f || posZ < -7f || posZ > 2f) {
				return false;
			} 
		}
		return true;
	}

}
