using UnityEngine;
using System.Collections;

public class PlayerSettingsOnline : MonoBehaviour
{
    public string difficulty;
    public string eye;

    public string SaveToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
