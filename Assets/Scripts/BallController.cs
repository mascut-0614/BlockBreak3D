using UnityEngine;

public class BallController : MonoBehaviour {

	public static int attack=0;
	public AudioSource sound;
	void Start(){
		sound = GetComponent<AudioSource> ();
	}
	public void Accel(){
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (5f, 0, 5f);
	}

	void OnCollisionEnter(Collision other){
		sound.PlayOneShot (sound.clip);
		if(other.gameObject.CompareTag("Wall")){
			attack++;
			if (attack >= 7) {//詰み防止
				Accel ();
			}
		}
	}
}
