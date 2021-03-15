using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class Room : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void SetRoomInfo(RoomInfo roomInfo)
	{
		_text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
	}
}
