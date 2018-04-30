using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour {
	public GameObject Life1;
	public GameObject Life2;
	public GameObject Life3;
	public GameObject Life4;
	public static int hp=4;
	public static int clear = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(hp){
		case 4:
			Life1.gameObject.SetActive (true);
			Life2.gameObject.SetActive (true);
			Life3.gameObject.SetActive (true);
			Life4.gameObject.SetActive (true);
			break;
		case 3:
			Life1.gameObject.SetActive (true);
			Life2.gameObject.SetActive (true);
			Life3.gameObject.SetActive (true);
			Life4.gameObject.SetActive (false);
			break;
		case 2:
			Life1.gameObject.SetActive (true);
			Life2.gameObject.SetActive (true);
			Life3.gameObject.SetActive (false);
			Life4.gameObject.SetActive (false);
			break;
		case 1:
			Life1.gameObject.SetActive (true);
			Life2.gameObject.SetActive (false);
			Life3.gameObject.SetActive (false);
			Life4.gameObject.SetActive (false);
			break;
		case 0:
			SceneManager.LoadScene ("GameOver");
			break;
		default:
			break;
		}
	}
}
