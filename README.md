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
