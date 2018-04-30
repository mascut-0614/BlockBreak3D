using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectController : MonoBehaviour {
	// Use this for initialization
	public static TcpClient tcpClient = new TcpClient();
	public static NetworkStream ns;
	public static bool cont=false;
	System.IO.MemoryStream ms;
	public InputField input;
	public Button button;
	public Text value;
	public void Connect(){
		try{
			tcpClient.Connect (input.text, 8080);
		}catch{
			input.text="Failed";
		}
		if (tcpClient.Connected) {
			ns = tcpClient.GetStream ();
			SceneManager.LoadScene ("Stage1");
		}
	}

	// Update is called once per frame
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
						Debug.Log (lineArray);
						//Action (lineArray);	
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
}