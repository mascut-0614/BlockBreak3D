using UnityEngine;
using System.Net.Sockets;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectController : MonoBehaviour {
	
	public static TcpClient tcpClient = new TcpClient();
	public static NetworkStream ns;
	public InputField input;

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
}