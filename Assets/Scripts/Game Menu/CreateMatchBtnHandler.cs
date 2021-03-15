using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CreateMatchBtnHandler : MonoBehaviourPunCallbacks
{
	public GameObject RoomSettings;

	private const string ROOM_TYPE_PROP_KEY = "rt";
	private const string ROOM_PW_PROP_KEY = "rp";

	public void TriggerRoomSettings(bool value)
	{
		RoomSettings.SetActive(value);
	}

	public void CreateMatchRoom()
	{
		// todo: instantiate the logo probablyyy
		if (!PhotonNetwork.IsConnected)
		{
			return;
		}
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = RoomManager.Instance.MaxPlayers;
		roomOptions.IsVisible = true;
		roomOptions.IsOpen = true;
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

	public override void OnJoinedRoom()
	{
		Debug.Log("Joined room successfuly");
	}

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		Debug.LogWarningFormat("Join room failed {0}", message);
	}

}
