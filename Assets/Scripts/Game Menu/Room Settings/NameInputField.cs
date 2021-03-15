using UnityEngine;
using UnityEngine.UI;

public class NameInputField : MonoBehaviour
{
    private InputField roomNameInputField;

    private void Start()
    {
        roomNameInputField = GetComponent<InputField>();
		roomNameInputField.text = PlayerPrefs.GetString(PlayerPrefsConstants.USERNAME) + "'s match";
        RoomManager.Instance.RoomName = roomNameInputField.text;
    }

    public void OnEndEdit(string roomName)
	{
        RoomManager.Instance.RoomName = roomName;
	}
}
