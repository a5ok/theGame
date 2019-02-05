using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._3D4amb_LIB
{
    /// <summary>
    /// This policies must be configured by the Developer in Unity inspector
    /// </summary>
    public class GamePolicy
    {
        /// <summary>
        /// This policy defines if the lower-upper bounds of the increasing penalty
        /// are loaded from the Settings file or set by the Player himself
        /// </summary>
        /// <remarks>If it's set on FROM_FILE the library
        /// will use the default GameDifficulty settings, you can edit them on the file</remarks>
        public enum DifficultyBounds
        {
            FROM_FILE, FROM_PLAYER
        }

        /// <summary>
        /// This policy defines what to do with penalty when the player "loses"
        /// (whatever this means in the specific game)
        /// </summary>
        /// <example>LAST: go back to the last penalty setting, RESTART: from the start bounds</example>
        [Obsolete]
        public enum Continue
        {
            LAST, RESTART
        }

        /// <summary>
        /// This policy defines when the session is saved, at the end of each level or at the end of the game
        /// (for shorter games)
        /// </summary>
        [Obsolete]
        public enum SaveSession
        {
            EACH, ENDGAME
        }

        /// <summary>
        /// This policy defines if the game will stay always at the same Difficulty
        /// which the Scene started or it gan go beyond to the Hard difficulty
        /// </summary>
        /// <remarks>This is supposed to take action only with Time-Event increased penalty</remarks>
        public enum DifficultyProgression
        {
            FIXED, INFINITE
        }

        /// <summary>
        /// This policy defines if the Penalty is increased by a % based on a number
        /// of steps it's supposed to do before max or it's manually increased
        /// </summary>
        /// <remarks>It's strongly suggested to use the easiest (BY_STEPS) way</remarks>
        public enum IncreasePenalty
        {
            BY_STEPS, MANUAL
        }

        /// <summary>
        /// This policy defines if the Penalty is STATIC (always the same during the
        /// single Scene) or it's supposed to increase in some manner (DYNAMIC)
        /// </summary>
        public enum IncreaseType
        {
            STATIC, DYNAMIC
        }

        /// <summary>
        /// This policy defines what to know when the player loses
        /// </summary>
        public enum IncreaseResetOnDeath
        {
            LAST, TO_ZERO, NO_RESET
        }
    }
}
