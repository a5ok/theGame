using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._3D4amb_LIB.Scripts.Utils
{
    /// <summary>
    /// Settings for a player
    /// </summary>
    [Serializable]
    public class PlayerSettings
    {
        /// <summary>
        /// The owner of the settings
        /// </summary>
        public PlayerID playerID;
        /// <summary>
        /// Reference for the healthy eye (the one to penalize)
        /// for the owner
        /// </summary>
        public Eye healthyEye;
        /// <summary>
        /// Starting difficulty for the owner
        /// </summary>
        public GameDifficulty difficultyStart;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="actualPlayer"><see cref="playerID"/></param>
        public PlayerSettings(PlayerID actualPlayer)
        {
            playerID = actualPlayer;
            healthyEye = Eye.LEFT;
            difficultyStart = GameDifficulty.MEDIUM;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="playerID"><see cref="playerID"/></param>
        /// <param name="healthyEye"><see cref="healthyEye"/></param>
        /// <param name="defaultDifficulty"><see cref="difficultyStart"/></param>
        public PlayerSettings(PlayerID playerID, Eye healthyEye, GameDifficulty defaultDifficulty)
        {
            this.playerID = playerID;
            this.healthyEye = healthyEye;
            this.difficultyStart = defaultDifficulty;
        }
    }
}
