using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class Room : MonoBehaviour
{
	[SerializeField] private Text roomNameText;
	[SerializeField] private Text roomPlayersText;
	[SerializeField] GameObject privateRoomIcon;
	[SerializeField] Image joinBtnImg;
	[SerializeField] Text joinBtnText;

	private Image roomImage;

	public RoomInfo RoomInfo { get; private set; }

	public void SetRoomInfo(RoomInfo roomInfo)
	{
		RoomInfo = roomInfo;
		roomNameText.text = roomInfo.Name;
		roomPlayersText.text = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers;

		if (roomInfo.CustomProperties[RoomManager.ROOM_TYPE_PROP_KEY].Equals("PRIVATE"))
		{
			privateRoomIcon.SetActive(true);
			joinBtnImg.color = Theme.accentBtnBgColour;
			joinBtnText.color = Theme.accentBtnTextColour;
		} else
		{
			privateRoomIcon.SetActive(false);
			joinBtnImg.color = Theme.primaryBtnBgColour;
			joinBtnText.color = Theme.primaryBtnTextColour;
		}
	}

	public void SetAccentBackgroundColor()
	{
		roomImage = gameObject.GetComponent<Image>();
		roomImage.color = Theme.accentBgColour;
	}

	public void JoinRoom()
	{
		PhotonNetwork.JoinRoom(RoomInfo.Name);
	}
}
