using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public class Snake : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    private Player player;

    [SerializeField]
    private Dictionary<Color, string> headSpritesPaths = new Dictionary<Color, string>()
        {
            { Color.red, "Sprites/red_head" },
            { Color.blue, "Sprites/blue_head" },
            { Color.green, "Sprites/green_head" },
            { Color.yellow, "Sprites/yellow_head" },
        };

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        player = info.Sender;
    }

    public bool isSnakeDead()
	{
        return (bool)player.CustomProperties["dead"];
	}

    public Color GetPlayerColour()
    {
        if (player == null) return Color.white;
        string colourHex = (string)player.CustomProperties["colour"];
        return PlayerManager.Instance.HexToColour(colourHex);
    }

    public string GetHeadSpritePath(Color colour)
    {
        //if (player == null || colour == null) return headSpritesPaths[Color.red];
        print(colour.ToString());
        return headSpritesPaths[GetPlayerColour()];
    }

    public void SetDeadPlayer()
	{
        player.SetCustomProperties(new Hashtable { { "dead", true } });
	}
}
