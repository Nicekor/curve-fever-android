using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
	public GameObject roomSettingsPanel;

	private byte maxPlayersPerRoom = 4;
	string gameVersion = "1";
	bool isConnecting;

	private void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public void TriggerRoomSettings(bool value)
	{
		roomSettingsPanel.SetActive(value);
	}

	public void OnCreateMatch()
	{

	}

	public void Connect()
	{
		
	}
}
