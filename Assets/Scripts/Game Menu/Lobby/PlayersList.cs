using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using ExitGames.Client.Photon;

public class PlayersList : MonoBehaviourPunCallbacks
{
	[SerializeField] private Transform content;
	[SerializeField] private LobbyPlayer lobbyPlayerPrefab;
	[SerializeField] private Button playBtn;
	[SerializeField] private Text playBtnText;
	[SerializeField] private Image playBtnImg;
	[SerializeField] private Transform lobbyContent;
	[SerializeField] private ErrorPanel errorPanelPrefab;

	private List<LobbyPlayer> lobbyPlayers = new List<LobbyPlayer>();
	private List<int> existingColourIndices = new List<int>();
	private bool ready = false;

	public override void OnEnable()
	{
		base.OnEnable();
		SetReady(false);
		if (!PhotonNetwork.IsConnected) return;
		if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null) return;
		UpdateLocalPlayerProps();
		foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
		{
			AddLobbyPlayer(player.Value);
		}
	}

	private void SetReady(bool state)
	{
		ready = state;

		if (!PhotonNetwork.IsMasterClient)
		{
			playBtnText.text = "Ready";
			int index = lobbyPlayers.FindIndex(x => x.Player == PhotonNetwork.LocalPlayer);
			if (index != -1)
			{
				lobbyPlayers[index].checkIcon.enabled = ready;
			}
			if (ready)
			{
				playBtnImg.color = playBtn.colors.pressedColor;
			}
			else
			{
				playBtnImg.color = Theme.primaryBtnBgColour;
			}
		}
	}

	private void UpdateLocalPlayerProps()
	{
		Player player = PhotonNetwork.LocalPlayer;
		int randomIndex = PlayerManager.Instance.GetRandomColourIndex();
		// todo: remove chosen ones instead of looping until I find a new one
		int[] existingColourIndicesArray = (int[])PhotonNetwork.CurrentRoom.CustomProperties["existingColourIndices"];
		while (existingColourIndicesArray.Contains(randomIndex))
		{
			randomIndex = PlayerManager.Instance.GetRandomColourIndex();
		}
		existingColourIndices.Add(randomIndex);
		string colourHex = ColorUtility.ToHtmlStringRGB(PlayerManager.Instance.avatarColours[randomIndex]);
		player.SetCustomProperties(new Hashtable { { "colour", colourHex }, { "dead", false } });
		PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable { { "existingColourIndices", existingColourIndices.ToArray() } });
	}

	public override void OnDisable()
	{
		base.OnDisable();
		for (int i = 0; i < lobbyPlayers.Count; i++)
		{
			Destroy(lobbyPlayers[i].gameObject);
		}
		lobbyPlayers.Clear();
	}

	private void AddLobbyPlayer(Player player)
	{
		LobbyPlayer lobbyPlayer = Instantiate(lobbyPlayerPrefab, content);
		if (lobbyPlayer != null)
		{
			lobbyPlayer.SetPlayerInfo(player);
			lobbyPlayers.Add(lobbyPlayer);
		}
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		AddLobbyPlayer(newPlayer);
	}

	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		int index = lobbyPlayers.FindIndex(x => x.Player == otherPlayer);
		if (index != -1)
		{
			Destroy(lobbyPlayers[index].gameObject);
			lobbyPlayers.RemoveAt(index);
		}
	}

	public void OnClick_StartGame()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			foreach (LobbyPlayer lobbyPlayer in lobbyPlayers)
			{
				if (lobbyPlayer.Player != PhotonNetwork.LocalPlayer)
				{
					if (!lobbyPlayer.Ready)
					{
						Instantiate(errorPanelPrefab, lobbyContent).Alert("All Players must be ready");
						return;
					}
				}
			}

			PhotonNetwork.CurrentRoom.IsOpen = false;
			PhotonNetwork.CurrentRoom.IsVisible = false;
			PhotonNetwork.LoadLevel(2);
		}
		else
		{
			SetReady(!ready);
			photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, ready);
		}

	}

	[PunRPC]
	private void RPC_ChangeReadyState(Player player, bool ready)
	{
		int index = lobbyPlayers.FindIndex(x => x.Player == player);
		if (index != -1)
		{
			lobbyPlayers[index].Ready = ready;
			lobbyPlayers[index].checkIcon.enabled = ready;
		}
	}
}
