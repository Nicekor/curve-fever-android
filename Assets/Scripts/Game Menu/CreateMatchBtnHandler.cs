using UnityEngine;

public class CreateMatchBtnHandler : MonoBehaviour
{
	[SerializeField] private GameObject RoomSettings;

	public void TriggerRoomSettings(bool value)
	{
		RoomSettings.SetActive(value);
	}
}
