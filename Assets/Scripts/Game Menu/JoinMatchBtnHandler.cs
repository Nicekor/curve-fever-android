using UnityEngine;

public class JoinMatchBtnHandler : MonoBehaviour
{
	[SerializeField] private GameObject JoinRoomMenu;

	public void ShowJoinRoomMenu()
	{
		JoinRoomMenu.SetActive(true);
	}
}
