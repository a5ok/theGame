using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This object handles the session. Create a new session when the game starts,
/// save a completed sessions, add/sub score to the session and so on.
/// </summary>
public class SessionManager : MonoBehaviour
{
    private SessionResult sessionResult;
    private PrefManager prefManager;
    private PlayerID actualPlayer;
    private PenaltyManager PM;
    private string levelName;
    private int levelNumber;

	void Start ()
    {
        prefManager = GameObject.Find("PrefManager").GetComponent<PrefManager>();
        PM          = GameObject.Find("PenaltyManager").GetComponent<PenaltyManager>();
        actualPlayer = prefManager.actualPlayer;
        levelName = SceneManager.GetActiveScene().name;
        switch (levelName)
        {

            case "Level 1":
                {
                    levelNumber = 1;
                    break;
                }
            case "Level 2":
                {
                    levelNumber = 2;
                    break;
                }
            case "Level 3":
                {
                    levelNumber = 3;
                    break;
                }
            case "Level 4":
                {
                    levelNumber = 4;
                    break;
                }
        }

        sessionResult = new SessionResult(actualPlayer, PM.Difficulty, levelNumber);
	}

    /// <summary>
    /// Get the integer score of this session
    /// </summary>
    /// <returns>The score value for the actual session loaded into
    /// the SessionManager
    /// </returns>
    public int GetScore()
    {
        return sessionResult.Score;
    }

    /// <summary>
    /// Add a fixed value to the score of the current session
    /// </summary>
    /// <param name="score">A value to add to the score</param>
    public void AddScore(int score)
    {
        sessionResult.Score += score;
    }

    /// <summary>
    /// Sub a fixed value to the score of the current session
    /// </summary>
    /// <param name="score">A value to sub from the score</param>
    public void SubScore(int score)
    {
        sessionResult.Score -= score;
    }

    /// <summary>
    /// Set a fixed value to the score of the current session
    /// </summary>
    /// <param name="score">A value to set to the score</param>
    /// <remarks>This will ignore previous score. Try to use 
    /// <see cref="AddScore(int)"/> or <see cref="SubScore(int)"/> for 
    /// more clear score-behaviours</remarks>
    public void SetScore(int score)
    {
        sessionResult.Score = score;
    }

    /// <summary>
    /// Save the current session for the actualPlayer
    /// </summary>
    public void SaveSession()
    {
        sessionResult.DifficultyEnd = PM.Difficulty;
        prefManager.AddSResForActualPlayer(sessionResult);
    }
}
