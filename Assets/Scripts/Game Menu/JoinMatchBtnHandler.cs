using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinMatchBtnHandler : MonoBehaviour
{
	[SerializeField] private GameObject JoinRoomMenu;

	public void ShowJoinRoomMenu()
	{
		JoinRoomMenu.SetActive(true);
	}
}
