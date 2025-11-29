using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NetworkClientInfo
{
    public static string Username
    {
        get => PlayerPrefs.GetString("PlayerPrefsKeys._PlayerUsername", null);
        set => PlayerPrefs.SetString("PlayerPrefsKeys._PlayerUsername", value);
    }
}
