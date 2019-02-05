using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._3D4amb_LIB
{
    /// <summary>
    /// This class represent a Player that can play to the game
    /// </summary>
    [Serializable]
    public class PlayerID
    {
        /// <summary>
        /// Name of the player, used as unique key
        /// </summary>
        /// <remarks>This must be unique</remarks>
        public string PlayerName;
        /// <summary>
        /// Index (in the "allAvatars" array)
        /// of the avatar for this player
        /// </summary>
        public int IdAvatar;

        /// <summary>
        /// Ctor that builds a player with a name
        /// (default avatar)
        /// </summary>
        /// <param name="name">Name of the player</param>
        public PlayerID(string name/*, Gender g*/)
        {
            PlayerName = name;
        }

        /// <summary>
        /// Ctor that builds a player with a name and his avatar
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <param name="idAvatar">Index of the avatar for this player</param>
        public PlayerID(string name, int idAvatar)
        {
            PlayerName = name;
            this.IdAvatar = idAvatar;
        }

        /// <summary>
        /// Check the player equivalence (confronting the names)
        /// </summary>
        /// <param name="o">Anothe player</param>
        /// <returns>True if they have both the same name</returns>
        public bool Equals(PlayerID o)
        {
            return this.PlayerName == o.PlayerName;
        }
    }
}

