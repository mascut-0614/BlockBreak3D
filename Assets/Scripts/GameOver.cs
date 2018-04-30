using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour {
	TcpClient tcpClient = ConnectController.tcpClient;
	NetworkStream ns=ConnectController.ns;
	System.IO.MemoryStream ms;
	public Text text;
	// Use this for initialization
	void Start () {
		if (HP.hp > 0) {
			text.text = "GameClear!";
		}else{
			text.text="GameOver!";
		}
	}
	void Update(){
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
						//Debug
					}
				}
			}
		}
	}
	// Update is called once per frame
	public void But(){
		HP.hp = 4;
		HP.clear = 0;
		SceneManager.LoadScene ("Stage1");
	}

}
