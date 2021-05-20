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
	[SerializeField] public ErrorPanel errorPanel;

	public void ShowJoinRoomMenu()
	{
		JoinRoomMenu.SetActive(true);
	}

	public void TriggerPrivateRoomPanel(bool value)
	{
		joinPrivateRoomPanel.SetActive(value);
	}
}
