using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public Text text;

	void Start () {
		if (StatusController.hp > 0) {
			text.text = "GameClear!";
		}else{
			text.text="GameOver!";
		}
	}
	public void Retry(){
		StatusController.hp = 3;
		StatusController.clear = 0;
		SceneManager.LoadScene ("Stage1");
	}

}
