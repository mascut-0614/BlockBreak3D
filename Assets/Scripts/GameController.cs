using UnityEngine;
using System.Net.Sockets;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	TcpClient tcpClient = ConnectController.tcpClient;
	NetworkStream ns=ConnectController.ns;
	System.IO.MemoryStream ms;
	public static bool start=false;
	public Text text;
	public AudioSource bgm;

	void Start(){
		bgm = GetComponent<AudioSource> ();
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
							FindObjectOfType<PlayerControl>().Action(lineArray);
						} else {
							if(lineArray=="Red"){
								start = true;
								text.gameObject.SetActive (false);
								FindObjectOfType<BallController> ().Accel ();
							}
						}
					}
				}
			}
		}
	}
}
