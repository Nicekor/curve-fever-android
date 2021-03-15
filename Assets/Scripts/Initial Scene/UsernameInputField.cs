using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(InputField))]
public class UsernameInputField : MonoBehaviour
{
	private void Start()
	{
		string defaultName = string.Empty;
		InputField _inputField = GetComponent<InputField>();

		if (_inputField != null)
		{
			if (PlayerPrefs.HasKey(PlayerPrefsConstants.USERNAME))
			{
				defaultName = PlayerPrefs.GetString(PlayerPrefsConstants.USERNAME);
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

		PlayerPrefs.SetString(PlayerPrefsConstants.USERNAME, username);
	}
}
