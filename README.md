# 巡逻兵游戏
-----------------
## 游戏介绍
  我们的主人公将会在一个3X3的地图中进行战斗。地图上有4个巡逻兵正在巡逻，当主人公进入巡逻兵的追杀范围时，巡逻兵会试图追赶，主人公离开追杀范围时，巡逻兵恢复正常，继续巡逻。唯一的获胜方法就是把四个巡逻兵都集中在中间的方格上，让他们互相残杀！
  
## 游戏情景
+ [游戏演示视频](https://pan.baidu.com/s/1gQM8sSemtj487k6g22wPGA?qq-pf-to=pcqq.c2c)
+ 地图一览
![地图](https://raw.githubusercontent.com/MapleLai/Homework6/master/Screenshot/%E5%9C%B0%E5%9B%BE.png)
+ 游戏中
![游戏中](https://raw.githubusercontent.com/MapleLai/Homework6/master/Screenshot/%E6%B8%B8%E6%88%8F%E4%B8%AD.jpg)
+ 游戏失败
![游戏失败](https://raw.githubusercontent.com/MapleLai/Homework6/master/Screenshot/%E6%B8%B8%E6%88%8F%E5%A4%B1%E8%B4%A5.jpg)
+ 游戏获胜
![游戏获胜](https://raw.githubusercontent.com/MapleLai/Homework6/master/Screenshot/%E6%B8%B8%E6%88%8F%E8%8E%B7%E8%83%9C.jpg)

## 游戏代码
+ SSDirector  

游戏的导演类，由于此次游戏场景只有一个，所以导演类要实现的代码并不多。
<pre>
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

</pre>

+ FirstSceneController  

游戏的场景类，挂载在主摄影机上，在游戏启动时加载游戏地图跟游戏人物。此外，还负责记录当前游戏状态，在获胜或失败时弹出结束信息。使用订阅模式订阅巡逻兵与玩家的撞击事件。
<pre>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour {

	private static FirstSceneController _instance;
	private GameObject map;
	public GameObject player;
	private PatrolFactory patrolFactory;
	public int gameOver;//记录游戏状态，{失败，进行，获胜}={-1,0,1}

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

		Patrol.hitPlayerEvent += Over ;//订阅巡逻兵与玩家的撞击事件
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
</pre>

+ PatrolFactory  

巡逻兵工厂，负责生成巡逻兵并把巡逻兵放进数组里，当场景类请求时，会查看巡逻兵的位置信息判断是否获胜。
<pre>
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
</pre>

+ Patrol  

巡逻兵类控制巡逻兵的一些基本动作，如当前是在巡逻状态还是追赶状态，执行对应的动作。当玩家进入或离开追杀范围以及发生撞击时，执行对应的事件。
<pre>
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

	//与玩家撞击
	void OnCollisionStay(Collision other){
		if (other.gameObject.tag == "Player") {
			if (hitPlayerEvent != null) {
				hitPlayerEvent ();
			}
		}
	}

	//玩家进入追杀范围
	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Player") {
			patroling = false;
		}
	}

	//玩家离开追杀范围
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			patroling = true;
		}
	}

}
</pre>

+ UserInterface  

玩家类，负责玩家的移动，WASD与上下左右两套键位同时有效。
<pre>
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
</pre>
