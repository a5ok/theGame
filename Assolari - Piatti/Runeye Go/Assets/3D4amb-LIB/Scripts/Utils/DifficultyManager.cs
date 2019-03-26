using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// This static class gives utils method to manage difficulty
/// </summary>
namespace Assets._3D4amb_LIB
{
    public static class DifficultyManager
    {
        /// <summary>
        /// This method let you get the next difficulty in order (EASY -> MEDIUM -> HARD -> (always HARD))
        /// </summary>
        /// <param name="now">The current difficulty</param>
        /// <returns>Next difficulty in logical approach</returns>
        /// <remarks>It can't go beyond Hard</remarks>
        /// <example>It simply does:
        /// EASY    -> MEDIUM
        /// MEDIUM  -> HARD
        /// HARD    -> HARD
        /// </example>
        public static GameDifficulty GetNext(GameDifficulty now)
        {
            switch(now)
            {
                case GameDifficulty.EASY:  return GameDifficulty.MEDIUM;
                case GameDifficulty.MEDIUM: return GameDifficulty.HARD;
                default: return GameDifficulty.HARD;
            }
        }
    }
}
