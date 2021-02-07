using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(InputField))]
public class UsernameInputField : MonoBehaviour
{
	const string usernamePrefKey = "Username";

	private void Start()
	{
		string defaultName = string.Empty;
		InputField _inputField = GetComponent<InputField>();

		if (_inputField != null)
		{
			if (PlayerPrefs.HasKey(usernamePrefKey))
			{
				defaultName = PlayerPrefs.GetString(usernamePrefKey);
				_inputField.text = defaultName;
			}
		}

		PhotonNetwork.NickName = defaultName;
	}

	public void SetUsername(string username)
	{
		if (string.IsNullOrEmpty(username))
		{
			// todo: Add this to a gui label
			Debug.LogError("Player Name is null or empty");
			return;
		}
		PhotonNetwork.NickName = username;

		PlayerPrefs.SetString(usernamePrefKey, username);
	}
}
