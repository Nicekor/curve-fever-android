using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections.Generic;
public class RoomManager : MonoBehaviourPunCallbacks
{
	private string roomName = "";
	public static string roomType = "PUBLIC";
	private string roomPassword = "";
	private byte maxPlayers = 4;

	[SerializeField] private Text roomNameText;
	[SerializeField] private Text roomPasswordText;
	[SerializeField] private Room Room;
	[SerializeField] private Transform RoomsListContent;

	private const string ROOM_TYPE_PROP_KEY = "rt";
	private const string ROOM_PW_PROP_KEY = "rp";

	public void CreateMatchRoom()
	{
		roomName = roomNameText.text;
		roomPassword = roomPasswordText.text;
		if (string.IsNullOrEmpty(roomName))
		{
			// display error to the user
			return;
		}
		if (roomType.Equals("PRIVATE") && string.IsNullOrEmpty(roomPassword))
		{
			// display error to the user
			return;
		}
		// todo: instantiate the logo probablyyy
		if (!PhotonNetwork.IsConnected)
		{
			return;
		}
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = maxPlayers;
		roomOptions.IsVisible = true;
		roomOptions.IsOpen = true;
		roomOptions.CustomRoomProperties = new Hashtable { { ROOM_TYPE_PROP_KEY, roomType }, { ROOM_PW_PROP_KEY, roomPassword } };
		PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
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

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
		Debug.Log("Leaving the room");
	}

	public override void OnLeftRoom()
	{
		Debug.Log("Left the room");
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		Debug.Log("onroomlistupdate from RoomManager");
		foreach (RoomInfo info in roomList)
		{
			Room room = Instantiate(Room, RoomsListContent);
			if (room != null)
			{
				room.SetRoomInfo(info);
			}
		}
	}
}
