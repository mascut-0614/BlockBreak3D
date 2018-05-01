using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerControl : MonoBehaviour {

	public GameObject ball;
	public Material redmat;
	public Material bluemat;
	public GameObject back;

	public void Action(string key){
		switch(key){
		case "Red":
			ball.gameObject.tag = "Red";
			ball.gameObject.GetComponent<Renderer> ().material = redmat;
			break;
		case "Blue":
			ball.gameObject.tag = "Blue";
			ball.gameObject.GetComponent<Renderer> ().material = bluemat;
			break;
		case "Up":
			if(transform.position.z<3.5f){
				transform.position= transform.position + new Vector3 (0, 0, 0.2f);
			}
			break;
		case "Down":
			if(transform.position.z>-3.5f){
				transform.position= transform.position - new Vector3 (0, 0, 0.2f);
			}
			break;
		case "Right":
			if(transform.position.x<0){
				transform.position= transform.position + new Vector3 (0.2f, 0, 0);
			}
			break;
		case "Left":
			if(transform.position.x>-7.5){
				transform.position= transform.position - new Vector3 (0.2f, 0, 0);
			}
			break;
		default:
			float num = (int.Parse (key) - 50) * 0.07f;
			back.gameObject.transform.position = new Vector3 (7.5f, 0, num);
			break;
		}
	}
}
