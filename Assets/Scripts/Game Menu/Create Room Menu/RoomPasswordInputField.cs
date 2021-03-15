using UnityEngine;

public class RoomPasswordInputField : MonoBehaviour
{
	public void OnEndEdit(string roomPassword)
	{
		RoomManager.Instance.RoomPassword = roomPassword;
	}
}
