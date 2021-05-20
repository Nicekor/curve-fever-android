using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Dictionary<string, Texture> avatarCache = new Dictionary<string, Texture>();
    public Color[] avatarColours = { Color.red, Color.blue, Color.green, Color.yellow };

    public void CacheTexture(string uri, Texture texture)
    {
        if (avatarCache.ContainsKey(uri)) return;
        avatarCache.Add(uri, texture);
    }

    public Texture LookupTexture(string uri)
    {
        if (!avatarCache.ContainsKey(uri)) return null;
        return avatarCache[uri];
    }

    public int GetRandomColourIndex()
    {
        return Random.Range(0, avatarColours.Length);
    }

    public Color HexToColour(string colourHex)
    {
        Color colour;
        ColorUtility.TryParseHtmlString("#" + colourHex, out colour);
        return colour;
    }
}
