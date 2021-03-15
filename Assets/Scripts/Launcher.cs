using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class Launcher : MonoBehaviourPunCallbacks
{
	public GameObject LoadingPanel;
	public Animator logoAnimator;
	public Text infoText;
	public GameObject RetryButton;

	string gameVersion = "1";
	bool isConnecting;

	private void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	private void Start()
	{
		Connect();
	}

	public void Connect()
	{
		if (PhotonNetwork.IsConnected)
		{
			// join room
		} 
		else
		{
			LoadingPanel.SetActive(true);
			infoText.color = Color.white;
			infoText.text = "Connecting...";
			logoAnimator.enabled = true;
			RetryButton.SetActive(false);
			isConnecting = PhotonNetwork.ConnectUsingSettings();
			PhotonNetwork.GameVersion = gameVersion;
		}
	}

	public override void OnConnectedToMaster()
	{
		LoadingPanel.SetActive(false);
		PhotonNetwork.JoinLobby();
		Debug.Log("Connecting Using Settings worked fine and I am now connected");
		if (isConnecting)
		{
			isConnecting = false;
		}
	}

	// Called after disconnecting from the Photon server. It could be a failure or an explicit disconnect call 
	public override void OnDisconnected(DisconnectCause cause)
	{
		RetryButton.SetActive(true);
		logoAnimator.enabled = false;
		infoText.color = Color.red;
		infoText.text = "Network connection failed";
		isConnecting = false;
		Debug.LogWarningFormat("Cause of disconnection: {0}", cause);
	}

	public override void OnJoinedLobby()
	{
		Debug.Log("Joined lobby");
	}
}
