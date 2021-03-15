using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CreateMatchBtnHandler : MonoBehaviourPunCallbacks
{
	public GameObject RoomSettings;
	public Text roomName;

	private const string ROOM_TYPE_PROP_KEY = "rt";
	private const string ROOM_PW_PROP_KEY = "rp";

	public void TriggerRoomSettings(bool value)
	{
		RoomSettings.SetActive(value);
	}

	public void CreateRoom()
	{
		// todo: instantiate the logo probablyyy
		if (!PhotonNetwork.IsConnected)
		{
			return;
		}
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = RoomManager.Instance.MaxPlayers;
		roomOptions.CustomRoomProperties = new Hashtable { { ROOM_TYPE_PROP_KEY, RoomManager.Instance.RoomType }, { ROOM_PW_PROP_KEY, RoomManager.Instance.RoomPassword } };
		PhotonNetwork.CreateRoom(RoomManager.Instance.RoomName, roomOptions, TypedLobby.Default);
	}

	public override void OnCreatedRoom()
	{
		Debug.Log("Created room successfuly");
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		Debug.LogWarningFormat("Room creation failed {0}", message);
	}
}
