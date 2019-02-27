using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Assets._3D4amb_LIB
{
    /// <summary>
    /// This object represent the result of a game session
    /// </summary>
    [Serializable]
    public class SessionResult : IComparable<SessionResult>
    {
        /// <summary>
        /// The player that played this session
        /// </summary>
        public PlayerID Player;
        /// <summary>
        /// Difficulty at the start of the session
        /// </summary>
        public GameDifficulty DifficultyStart;
        /// <summary>
        /// Difficulty at the end of the session
        /// </summary>
        public GameDifficulty DifficultyEnd;
        /// <summary>
        /// Score of the session
        /// </summary>
        public /*S*/ int Score;

        // Level number
        public int Level;

        /// <summary>
        /// Ctor of the session results with all the parameters
        /// </summary>
        /// <param name="player"><see cref="Player"/></param>
        /// <param name="start"><see cref="DifficultyStart"/></param>
        /// <param name="end"><see cref="DifficultyEnd"/></param>
        /// <param name="score"><see cref="Score"/></param>
        public SessionResult(PlayerID player, GameDifficulty start, GameDifficulty end, int score)
        {
            this.Player = player;
            this.DifficultyStart = start;
            this.DifficultyEnd = end;
            this.Score = score;
        }

        /// <summary>
        /// Ctor for the sessionresult at the beggining
        /// </summary>
        /// <param name="player"><see cref="Player"/></param>
        /// <param name="start"><see cref="DifficultyStart"/></param>
        public SessionResult(PlayerID player, GameDifficulty start, int level)
        {
            this.Player = player;
            this.DifficultyStart = start;
            this.Score = 0;
            this.Level = level;
        }

        /// <summary>
        /// Compare this session with another one
        /// </summary>
        /// <remarks>This compares the scores of the 2 sessions</remarks>
        /// <param name="o">Another SessionResult</param>
        /// <returns>The comparison between the 2 scores</returns>
        public int CompareTo(SessionResult o)
        {
            return this.Score.CompareTo(o.Score);
        }
    }
}
