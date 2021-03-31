using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using System.Collections;
using UnityEngine.Networking;

public class LobbyPlayer : MonoBehaviour
{
	[SerializeField] private RawImage avatar;
	[SerializeField] private Text text;

	public Player Player { get; private set; }

    // generate a color for each player to get the avatar and this color will persist for the snake
	public void SetPlayerInfo(Player player)
	{
		Player = player;
		text.text = player.NickName;
        StartCoroutine(DownloadImage("https://eu.ui-avatars.com/api/?name=" + player.NickName + "&length=1&background=random"));
    }

    IEnumerator DownloadImage(string uri)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(uri);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Error While Sending: " + request.error);
        }
        else
        {
            avatar.color = Color.white;
            avatar.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}
