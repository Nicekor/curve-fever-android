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
    private List<Room> roomsListing = new List<Room>();

    [SerializeField] private Text roomNameText;
    [SerializeField] private Text roomPasswordText;
    [SerializeField] private Room roomPrefab;
    [SerializeField] private Transform roomsListContent;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private Transform roomOptionsPanel;
    [SerializeField] private ErrorPanel errorPanelPrefab;

    public const string ROOM_TYPE_PROP_KEY = "rt";
    public const string ROOM_PW_PROP_KEY = "rp";

    private void Start()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
    }

    public void CreateMatchRoom()
    {
        roomName = roomNameText.text.Trim();
        roomPassword = roomPasswordText.text;
        bool roomExists = roomsListing.Exists(room => room.RoomInfo.Name == roomName);

        if (roomExists)
        {
            Instantiate(errorPanelPrefab, roomOptionsPanel).Alert("This room already exists.");
            return;
        }
        if (string.IsNullOrEmpty(roomName))
        {
            Instantiate(errorPanelPrefab, roomOptionsPanel).Alert("The Room Name is missing!");
            return;
        }
        if (roomType.Equals("PRIVATE"))
        {
            if (string.IsNullOrEmpty(roomPassword))
            {
                Instantiate(errorPanelPrefab, roomOptionsPanel).Alert("The Room Name is missing!");
                return;
            }

        }
        // todo: instantiate the logo maybee
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        roomOptions.EmptyRoomTtl = 0;
        roomOptions.PlayerTtl = 0;
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
        lobbyPanel.SetActive(true);
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
        lobbyPanel.SetActive(false);
        Debug.Log("Left the room");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        LeaveRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("onroomlistupdate from RoomManager - RoomInfo");
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = roomsListing.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(roomsListing[index].gameObject);
                    roomsListing.RemoveAt(index);
                }
            }
            else
            {
                Room room = Instantiate(roomPrefab, roomsListContent);
                if (room != null)
                {
                    room.SetRoomInfo(info);
                    roomsListing.Add(room);
                    // todo: SetAccentBgColor for odd rows
                }

            }
        }
    }
}
