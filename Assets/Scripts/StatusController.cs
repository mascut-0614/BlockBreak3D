using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusController : MonoBehaviour {
	public GameObject Life1;
	public GameObject Life2;
	public GameObject Life3;
	public static int hp=3;
	public static int clear = 0;

	void OnCollisionEnter(Collision other){
		BallController.attack = 0;
		if(clear==0){
			GameController.start = false;
			clear += 1;
			SceneManager.LoadScene("Stage2");
		}else if(clear==1){
			GameController.start = false;
			clear += 1;
			SceneManager.LoadScene ("GameOver");
		}
	}
	void Update () {
		switch(hp){
		case 3:
			Life1.gameObject.SetActive (true);
			Life2.gameObject.SetActive (true);
			Life3.gameObject.SetActive (true);
			break;
		case 2:
			Life1.gameObject.SetActive (true);
			Life2.gameObject.SetActive (true);
			Life3.gameObject.SetActive (false);
			break;
		case 1:
			Life1.gameObject.SetActive (true);
			Life2.gameObject.SetActive (false);
			Life3.gameObject.SetActive (false);
			break;
		case 0:
			SceneManager.LoadScene ("GameOver");
			break;
		default:
			break;
		}
	}
}
