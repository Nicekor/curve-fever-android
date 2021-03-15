using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomsList : MonoBehaviourPunCallbacks
{
	[SerializeField] private Transform _content;
	[SerializeField] private Room _room;

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		Debug.Log("onroomlistupdate from RoomsList");
		foreach (RoomInfo info in roomList)
		{
			Room room = Instantiate(_room, _content);
			if (room != null)
			{
				room.SetRoomInfo(info);
			}
		}
	}
}
