using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
	public GameObject RoomSettingsPanel;
	public GameObject LoadingPanel;
	public Animator logoAnimator;
	public Text infoText;
	public GameObject RetryButton;

	private byte maxPlayersPerRoom = 4;
	string gameVersion = "1";
	bool isConnecting;

	private void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public void TriggerRoomSettings(bool value)
	{
		RoomSettingsPanel.SetActive(value);
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
			logoAnimator.enabled = true;
			RetryButton.SetActive(false);
			isConnecting = PhotonNetwork.ConnectUsingSettings();
			PhotonNetwork.GameVersion = gameVersion;
		}
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connecting Using Settings worked fine and I am now connected");
		if (isConnecting)
		{
			isConnecting = false;
			// create room
		}

	}

	// Called after disconnecting from the Photon server. It could be a failure or an explicit disconnect call 
	public override void OnDisconnected(DisconnectCause cause)
	{
		RetryButton.SetActive(true);
		logoAnimator.enabled = false;
		infoText.text = "Network connection failed";
		infoText.color = Color.red;
		isConnecting = false;
		Debug.LogWarningFormat("Cause of disconnection: {0}", cause);
	}
}
