using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using UnityEngine.Networking;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyPlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private RawImage avatar;
    [SerializeField] private Text text;
    public RawImage checkIcon;

    public Player Player { get; private set; }
    public bool Ready = false;

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        text.text = player.NickName;
        if (player.CustomProperties.ContainsKey("colour"))
        {
            StartCoroutine(DownloadAvatar(getPlayerAvatarUri(player.NickName, (string)player.CustomProperties["colour"])));
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer != null && targetPlayer == Player)
        {
            if (changedProps.ContainsKey("colour"))
            {
                StartCoroutine(DownloadAvatar(getPlayerAvatarUri(targetPlayer.NickName, (string)targetPlayer.CustomProperties["colour"])));
            }
        }
    }

    private string getPlayerAvatarUri(string nickname, string colour)
    {
        return "https://eu.ui-avatars.com/api/?name=" + nickname + "&length=1&background=" + colour;
    }

    IEnumerator DownloadAvatar(string uri)
    {
        Texture cachedTexture = PlayerManager.Instance.LookupTexture(uri);
        if (cachedTexture != null)
        {
            renderAvatar(cachedTexture);
            yield return null;
        }
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(uri);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Error While Sending: " + request.error);
        }
        else
        {
            Texture texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            PlayerManager.Instance.CacheTexture(uri, texture);
            renderAvatar(texture);
        }
    }

    private void renderAvatar(Texture texture)
    {
        avatar.color = Color.white;
        avatar.texture = texture;
    }
}
