using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class LeaderboardPlayer : MonoBehaviour
{
	[SerializeField] private Text placeText;
	[SerializeField] private Text nicknameText;

	public void SetLeaderboardPlayerInfo(int playerPosition, Player player)
	{
		placeText.text = playerPosition.ToString() + ".";
		nicknameText.text = player.NickName;
	}
}
