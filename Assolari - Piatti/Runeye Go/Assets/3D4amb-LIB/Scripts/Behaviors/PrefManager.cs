using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets._3D4amb_LIB.Scripts.Utils;

/// <summary>
/// This class manages the preferences (data) of the game and sessions
/// All the data are stored in Unity PlayerPrefs
/// </summary>
public class PrefManager : MonoBehaviour
{
    /// <summary>
    /// A reference to the NewPlayerPanel in the MainMenu Scene
    /// </summary>
    public GameObject NewPlayerPanel;
    /// <summary>
    /// A reference to the MainManupanel in the MainMenu Scene
    /// </summary>
    public GameObject MainMenuPanel;
    /// <summary>
    /// Reference to player that is playing right now
    /// </summary>
    public PlayerID actualPlayer;
    /// <summary>
    /// Reference to the settings for the actualPlayer
    /// (loaded prof PlayerPrefs
    /// </summary>
    public PlayerSettings actualPlayerSettings;
    /// <summary>
    /// Sprites used to represent all player avatars
    /// </summary>
    /// <remarks>These must be set also in the AvatarSelectButton script</remarks>
    public Sprite[] allAvatars;
    /// <summary>
    /// This policy must stay here because it involved the behaviour
    /// of the MainMenu panels.
    /// </summary>
    /// <remarks>The corresponding one in PenaltyManager is set like this</remarks>
    public GamePolicy.DifficultyBounds PolicyDifficultyBounds;

    private string key_actualPlayer = "Key_ActualPlayer";
    private string key_playerSetting = "Key_AllUserSettings";
    private string key_sessResults = "Key_SessionResults";
    private string key_allPlayers = "Key_AllPlayers";
    private string key_PenaltyInfoStart = "Key_PIstart";
    private string key_PenaltyInfoEnd = "Key_PIend";
    //private string key_helthyEye = "Key_eye";

    void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);
        actualPlayer = LoadActualPlayer();
        if (actualPlayer != null)
        {
            actualPlayerSettings = LoadPlayerSettings();
        }
        else
        {
            //Go to New Player Panel
            MainMenuPanel.SetActive(false);
            NewPlayerPanel.SetActive(true);
        }
    }

    /// <summary>
    /// This method load the (last) actualPlayer
    /// saved in the persistent data
    /// </summary>
    /// <returns>Last actualPlayer</returns>
    public PlayerID LoadActualPlayer()
    {
        string p = PlayerPrefs.GetString(key_actualPlayer);
        if (p != null && p.Length != 0)
        {
            //Debug.Log("Actual Player loaded: " + p);
            return JsonUtility.FromJson<PlayerID>(p);
        }
        else
        {
            //Debug.Log("No Player found in PlayerPrefs - creating NewPlayer");
            return null;
        }
    }

    /// <summary>
    /// Save the parameter PlayerID as the actualPlayer
    /// in the persistent data
    /// </summary>
    /// <param name="player">An actualPlayer</param>
    private void SaveAsActualPlayer(PlayerID player)
    {
        PlayerPrefs.SetString(key_actualPlayer, JsonUtility.ToJson(player));
    }

    /// <summary>
    /// Load all Players saved in the persistent data
    /// </summary>
    /// <returns>An array of PlayerID[] with all players loaded</returns>
    public PlayerID[] LoadAllPlayers()
    {
        string p = PlayerPrefs.GetString(key_allPlayers);
        //Debug.Log("AllPlayers loaded: " + p);
        if (p.Length>0)
        {
            PlayerID[] allPlayers = JsonHelper.FromJson<PlayerID>(p);
            //Debug.Log("Length of AllPlayers loaded: " + allPlayers.Length);
            return allPlayers;
        }
        else
        {
            //Debug.Log("No Players to load");
            return null;
        }
    }

    /// <summary>
    /// Add a new Player to all the players saved
    /// </summary>
    /// <param name="newPlayer">A new player to save</param>
    public void AddNewPlayer(PlayerID newPlayer)
    {
        PlayerID[] from = LoadAllPlayers();
        PlayerID[] to;
        if(from!=null)
        {
            to = new PlayerID[from.Length + 1];
            from.CopyTo(to, 0);
            to[to.Length - 1] = newPlayer;
        }
        else
        {
            to = new PlayerID[1];
            to[0] = newPlayer;
        }
        PlayerPrefs.SetString(key_allPlayers, JsonHelper.ToJson(to));
        PlayerPrefs.SetString(key_actualPlayer, JsonUtility.ToJson(newPlayer));
        actualPlayer = LoadActualPlayer();
        actualPlayerSettings = LoadPlayerSettings();
    }

    /// <summary>
    /// Load all the Session results available in the
    /// persistent data for the actualPlayer
    /// </summary>
    /// <returns>An array of SessionResult with all the results
    /// for the ActualPlayer</returns>
    public SessionResult[] LoadSResForActualPlayer()    
    {
        string key_sesres_playername = key_sessResults + "_" + actualPlayer.PlayerName;
        string sres = PlayerPrefs.GetString(key_sesres_playername);
        //Debug.Log("Loaded session results for " + key_sesres_playername + "\n" + sres);
        if (sres.Length > 0)
        {
            return JsonHelper.FromJson<SessionResult>(sres);
        }
        else
        {
            //Debug.Log("no results for the actual player");
            return null;
        }
    }

    /// <summary>
    /// Add a SessionResult (for the actualPlayer) to
    /// the persistent data
    /// </summary>
    /// <param name="sr">A SessionResult for the actualPlayer</param>
    public void AddSResForActualPlayer(SessionResult sr)
    {
        SessionResult[] from = LoadSResForActualPlayer();
        SessionResult[] to;
        string key_sesres_playername = key_sessResults + "_" + actualPlayer.PlayerName;
        if (from != null)
        {
            to = new SessionResult[from.Length + 1];
            from.CopyTo(to, 0);
            to[to.Length - 1] = sr;
        }
        else
        {
            to = new SessionResult[1];
            to[0] = sr;
        }
        string sres = JsonHelper.ToJson(to);
        //Debug.Log("Saving in " + key_sesres_playername + ": " + sres);
        PlayerPrefs.SetString(key_sesres_playername, sres);
    }
    
    /// <summary>
    /// Load the PlayerSettings for the actualPLayer that
    /// is found in the persistent data
    /// </summary>
    /// <returns>A PlayerSettings object with all the settings
    /// for the actualPlayer</returns>
    public PlayerSettings LoadPlayerSettings()
    {
        string key_withName = key_playerSetting + "_" + actualPlayer.PlayerName;
        string loadedPlayerSettings = PlayerPrefs.GetString(key_withName);
        if (loadedPlayerSettings.Length > 0)
        {
            //Debug.Log("Loaded settings from " + key_withName + ": " +  loadedPlayerSettings);
            return JsonUtility.FromJson<PlayerSettings>(loadedPlayerSettings);
        }
        else
        {
            //Debug.Log("No UserSettings for this Player. Default settings created.");
            PlayerSettings defaultPlayerSettings = new PlayerSettings(actualPlayer);
            //Debug.Log("Saving in " + key_withName + ": " + JsonUtility.ToJson(defaultPlayerSettings));
            PlayerPrefs.SetString(key_withName, JsonUtility.ToJson(defaultPlayerSettings));
            return defaultPlayerSettings;
        }
    }
    
    /// <summary>
    /// Save the PlayerSettings as given parameters, for the actualPlayer
    /// </summary>
    /// <param name="setDiff">The (starting) difficulty set</param>
    /// <param name="setEye">The healthy eye set</param>
    public void SavePlayerSettings(GameDifficulty setDiff, Eye setEye)
    {
        string key_withName = key_playerSetting + "_" + actualPlayer.PlayerName;
        PlayerSettings playerSettings = new PlayerSettings(actualPlayer, setEye, setDiff);
        //Debug.Log("Saving in " + key_withName + ": " + JsonUtility.ToJson(playerSettings));
        PlayerPrefs.SetString(key_withName, JsonUtility.ToJson(playerSettings));
        actualPlayerSettings = playerSettings;
    }


    /// <summary>
    /// This method loads the PenaltyInfoEnd of the actual Player
    /// </summary>
    /// <returns>The PenaltyInfoEnd for the actual Player</returns>
    public PenaltyInfo LoadPlayerPIstart()
    {
        string key_withName = key_PenaltyInfoStart + "_" + actualPlayer.PlayerName;
        string loaded = PlayerPrefs.GetString(key_withName);
        if(loaded.Length>0)
        {
            //Debug.Log("Loaded PenaltyInfostart for " + actualPlayer.PlayerName + ": " + loaded);
            return JsonUtility.FromJson<PenaltyInfo>(loaded);
        }
        else
        {
            //Debug.Log("No PenaltyInfostart found for " + actualPlayer.PlayerName +": using default one");
            return new PenaltyInfo();
        }
    }

    /// <summary>
    /// This method loads the PenaltyInfoEnd of the actual Player
    /// </summary>
    /// <returns>The PenaltyInfoEnd for the actual Player</returns>
    public PenaltyInfo LoadPlayerPIend()
    {
        string key_withName = key_PenaltyInfoEnd + "_" + actualPlayer.PlayerName;
        string loaded = PlayerPrefs.GetString(key_withName);
        if (loaded.Length > 0)
        {
            //Debug.Log("Loaded PenaltyInfoEnd for " + actualPlayer.PlayerName + ": " + loaded);
            return JsonUtility.FromJson<PenaltyInfo>(loaded);
        }
        else
        {
            //Debug.Log("No PenaltyInfoEnd found for " + actualPlayer.PlayerName + ": using default one");
            return new PenaltyInfo();
        }
    }

    /// <summary>
    /// This method save the parameter PenaltyInfo as PenaltyInfoStart
    /// for the actual player
    /// </summary>
    /// <param name="penaltyInfo">PenaltyInfoStart to save for the actual player</param>
    public void SavePlayerPIstart(PenaltyInfo penaltyInfo)
    {
        string key_withName = key_PenaltyInfoStart + "_" + actualPlayer.PlayerName;
        PlayerPrefs.SetString(key_withName, JsonUtility.ToJson(penaltyInfo));
        //Debug.Log("Saving in " + key_withName + ": " + JsonUtility.ToJson(penaltyInfo));
    }

    /// <summary>
    /// This method save the parameter PenaltyInfo as PenaltyInfoEnd
    /// for the actual player
    /// </summary>
    /// <param name="penaltyInfo">PenaltyInfoEnd to save for the actual player</param>
    public void SavePlayerPIend(PenaltyInfo penaltyInfo)
    {
        string key_withName = key_PenaltyInfoEnd + "_" + actualPlayer.PlayerName;
        PlayerPrefs.SetString(key_withName, JsonUtility.ToJson(penaltyInfo));
        //Debug.Log("Saving in " + key_withName + ": " + JsonUtility.ToJson(penaltyInfo));
    }
}
