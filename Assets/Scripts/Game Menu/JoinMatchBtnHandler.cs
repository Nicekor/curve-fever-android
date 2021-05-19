using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class JoinMatchBtnHandler : MonoBehaviour
{
	[SerializeField] private GameObject JoinRoomMenu;
	[SerializeField] public GameObject joinPrivateRoomPanel;
	[SerializeField] public Button joinPrivateRoomBtn;
	[SerializeField] public InputField passwordInput;

	public void ShowJoinRoomMenu()
	{
		JoinRoomMenu.SetActive(true);
	}

	public void TriggerPrivateRoomPanel(bool value)
	{
		joinPrivateRoomPanel.SetActive(value);
	}

	public void JoinPrivateRoom(RoomInfo roomInfo)
	{
		if (roomInfo.CustomProperties[RoomManager.ROOM_PW_PROP_KEY].Equals(passwordInput.text))
		{
			print("password correct");
			print(roomInfo.Name);
			PhotonNetwork.JoinRoom(roomInfo.Name);
		}
		else
		{
			print("password incorrect");
		}
	}
}
