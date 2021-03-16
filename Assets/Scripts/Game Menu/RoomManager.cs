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
	private List<Room> listings = new List<Room>();

	[SerializeField] private Text roomNameText;
	[SerializeField] private Text roomPasswordText;
	[SerializeField] private Room roomPrefab;
	[SerializeField] private Transform roomsListContent;

	public const string ROOM_TYPE_PROP_KEY = "rt";
	public const string ROOM_PW_PROP_KEY = "rp";

	private void Start()
	{
		PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
		Debug.Log("Joined lobby");
	}

	public void CreateMatchRoom()
	{
		roomName = roomNameText.text;
		roomPassword = roomPasswordText.text;
		if (string.IsNullOrEmpty(roomName))
		{
			// display error to the user
			return;
		}
		if (roomType.Equals("PRIVATE"))
		{
			if (string.IsNullOrEmpty(roomPassword))
			{
				// display error to the user
				return;
			}

		}
		// todo: instantiate the logo probablyyy
		if (!PhotonNetwork.IsConnected)
		{
			return;
		}
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = maxPlayers;
		roomOptions.CustomRoomPropertiesForLobby = new string[] { ROOM_TYPE_PROP_KEY, ROOM_PW_PROP_KEY };
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
			if (info.RemovedFromList)
			{
				int index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
				if (index != -1)
				{
					Destroy(listings[index].gameObject);
					listings.RemoveAt(index);
				}
			}
			else
			{
				print(info.CustomProperties[ROOM_TYPE_PROP_KEY]);
				Room room = Instantiate(roomPrefab, roomsListContent);
				if (room != null)
				{
					room.SetRoomInfo(info);
					listings.Add(room);
					// todo: SetAccentBgColor for odd rows
					// add icon to private rooms
				}
				
			}
		}
	}
}
