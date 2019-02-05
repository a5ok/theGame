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
        /// Date when the session started
        /// </summary>
        public string Date;
        /// <summary>
        /// This is the DateTime format used
        /// to save the session
        /// </summary>
        public string DateFormat = "yy/MM/dd h:mm:ss tt";
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
            this.Date = DateTime.Now.ToString(DateFormat);
            this.DifficultyStart = start;
            this.DifficultyEnd = end;
            this.Score = score;
        }

        /// <summary>
        /// Get the DateTime object parsing the string saved
        /// </summary>
        /// <returns>The DateTime object of the Date string saved in the session</returns>
        public DateTime GetDate()
        {
            return DateTime.ParseExact(Date, DateFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Ctor for the sessionresult at the beggining
        /// </summary>
        /// <param name="player"><see cref="Player"/></param>
        /// <param name="start"><see cref="DifficultyStart"/></param>
        public SessionResult(PlayerID player, GameDifficulty start)
        {
            this.Player = player;
            this.Date = DateTime.Now.ToString(DateFormat);
            this.DifficultyStart = start;
            this.Score = 0;
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
