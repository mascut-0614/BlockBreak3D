using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	// Use this for initialization
	public int attack=0;
	public GameObject explode;
	public GameObject player;
	public Text text;
	void OnCollisionEnter(Collision other){
		Rigidbody rigidbody=gameObject.GetComponent<Rigidbody>();
		if(other.gameObject.CompareTag("Block")){
			attack = 0;
			StartCoroutine ("Explosion");
		}
		if(other.gameObject.CompareTag("Enemy")){
			if(HP.clear==0){
				PlayerControl.start = false;
				HP.clear += 1;
				SceneManager.LoadScene("Stage2");
			}else if(HP.clear==1){
				PlayerControl.start = false;
				HP.clear += 1;
				SceneManager.LoadScene ("GameOver");
			}
		}
		if(other.gameObject.CompareTag("Red")){
			attack = 0;
			if(gameObject.CompareTag("RedBall")){
				Destroy (other.gameObject);	
			}else if(gameObject.CompareTag("BlueBall")){
				rigidbody.velocity =new Vector3(rigidbody.velocity.x*1.1f,0,rigidbody.velocity.z*1.1f);
			}
		}else if(other.gameObject.CompareTag("Blue")){
			attack = 0;
			if(gameObject.CompareTag("BlueBall")){
				Destroy (other.gameObject);	
			}else if(gameObject.CompareTag("RedBall")){
				rigidbody.velocity =new Vector3(rigidbody.velocity.x*1.1f,0,rigidbody.velocity.z*1.1f);
			}
		}
		if(other.gameObject.CompareTag("Wall")){
			attack++;
			if (attack >= 10) {
				rigidbody.velocity = new Vector3 (5f,0,5f);
			}
		}
	}
	IEnumerator Explosion(){
		gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		Instantiate (explode, this.gameObject.transform.position, Quaternion.identity);
		PlayerControl.start = false;
		yield return new WaitForSeconds(2f);
		gameObject.transform.position = new Vector3 (-3f, 0, 0);
		text.gameObject.SetActive (true);
		player.gameObject.transform.position = new Vector3 (-5f,0,0);
		HP.hp--;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
