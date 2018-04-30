using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour {

	TcpClient tcpClient = ConnectController.tcpClient;
	NetworkStream ns=ConnectController.ns;
	System.IO.MemoryStream ms;
	public GameObject ball;
	public static bool start=false;
	public Material redmat;
	public Material bluemat;
	public static float speed=5f;
	public Text text;
	public GameObject back;
	public GameObject explode;
	void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag ("Block")) {
			ball.gameObject.SetActive (false);
			StartCoroutine ("Explosion");

		}
	}
	IEnumerator Explosion(){
		start = false;
		Instantiate (explode, this.gameObject.transform.position, Quaternion.identity);
		yield return new WaitForSeconds(2f);
		HP.hp-=1;
		ball.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		ball.gameObject.transform.position = new Vector3 (-3f, 0, 0);
		ball.gameObject.SetActive (true);
		text.gameObject.SetActive (true);
		gameObject.transform.position = new Vector3 (-5f,0,0);

	}
	void Update() {
		if (tcpClient.Connected)
		{
			if (tcpClient.Available > 0)
			{
				ms = new System.IO.MemoryStream();
				byte[] resBytes = new byte[256];
				int resSize = 0;
				resSize = ns.Read(resBytes, 0, resBytes.Length);
				if (resSize == 0)
				{
					Debug.Log("サーバーが切断されました。");
					ms.Close();
					ns.Close();
					tcpClient.Close();
				}
				ms.Write(resBytes, 0, resSize);
				System.Text.Encoding enc = System.Text.Encoding.UTF8;
				string resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);
				ms.Close();
				string[] lineArrays = resMsg.Split('|');
				foreach (string lineArray in lineArrays){
					if(lineArray!=""){
						if (start) {
							Action (lineArray);	
						} else {
							if(lineArray=="Red"){
								start = true;
								text.gameObject.SetActive (false);
								ball.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (speed, 0, speed);
							}
						}
					}
				}
			}
		}
	}
	public void SendMes(string mes) {
		if (tcpClient.Connected)
		{
			System.Text.Encoding enc = System.Text.Encoding.UTF8;
			byte[] sendBytes = enc.GetBytes(mes + '\n');
			ns.Write(sendBytes, 0, sendBytes.Length);
		}else {
			//Debug.Log("サーバーに接続されていません。");
		}
	}
	public void Action(string key){
		switch(key){
		case "Red":
			ball.gameObject.tag = ("RedBall");
			ball.gameObject.GetComponent<Renderer> ().material = redmat;
			break;
		case "Blue":
			ball.gameObject.tag = ("BlueBall");
			ball.gameObject.GetComponent<Renderer> ().material = bluemat;
			break;
		case "Up":
			if(gameObject.transform.position.z<3.5f){
				gameObject.transform.position =new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z+0.2f);
			}
			break;
		case "Down":
			if(gameObject.transform.position.z>-3.5){
				gameObject.transform.position=new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z-0.2f);
			}
			break;
		case "Right":
			gameObject.transform.position =new Vector3(gameObject.transform.position.x+0.2f,gameObject.transform.position.y,gameObject.transform.position.z);
			break;
		case "Left":
			gameObject.transform.position=new Vector3(gameObject.transform.position.x-0.2f,gameObject.transform.position.y,gameObject.transform.position.z);
			break;
		default:
			float num = (int.Parse (key) - 50) * 0.07f;
			back.gameObject.transform.position = new Vector3 (7.5f, 0, num);
			break;
		}
	}
}
