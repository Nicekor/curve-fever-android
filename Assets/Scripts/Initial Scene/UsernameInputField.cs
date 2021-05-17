using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(InputField))]
public class UsernameInputField : MonoBehaviour
{
	[SerializeField] private GameObject emptyUsernameText;
	[SerializeField] private Button customGamesBtn;

	private void OnEnable()
	{
		customGamesBtn.onClick.AddListener(OnClick_CustomGamesBtn);
	}

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

	private void OnClick_CustomGamesBtn()
	{
		if (!IsUsernameValid(PlayerPrefs.GetString(PlayerPrefsConstants.USERNAME))) return;

		SceneLoader.Instance.LoadScene(1);
	}

	private bool IsUsernameValid(string username)
	{
		if (string.IsNullOrEmpty(username.Trim()))
		{
			emptyUsernameText.SetActive(true);
			return false;
		}

		emptyUsernameText.SetActive(false);
		return true;
	}

	public void SetUsername(string username)
	{
		PlayerPrefs.SetString(PlayerPrefsConstants.USERNAME, username);

		if (!IsUsernameValid(username)) return;

		PhotonNetwork.NickName = username;
	}

}
