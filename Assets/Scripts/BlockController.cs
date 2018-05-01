using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlockController : MonoBehaviour {

	public GameObject explode;
	public GameObject ball;
	public Text text;
	public GameObject player;

	void OnTriggerEnter(Collider other){
		StartCoroutine ("Explosion");
		text.gameObject.SetActive (true);
		player.transform.position = new Vector3 (-5f,0,0);
		ball.SetActive (true);
		StatusController.hp--;
	}
	IEnumerator Explosion(){
		ball.SetActive (false);
		GameController.start = false;
		Instantiate (explode, ball.transform.position, Quaternion.identity);
		ball.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		ball.transform.position = new Vector3 (-3f, 0, 0);
		yield return new WaitForSeconds(5f);
	}
}
