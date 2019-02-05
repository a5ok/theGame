using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represent a point of the Graph,
/// with simple (X, Y) coords and a gameDifficulty to set the color
/// </summary>
/// <remarks>You can use whatever dimension of Y, the Graph scales it 
/// so that the maximum height is shown and scale everything accordingly</remarks>
/// <example>A DataPoint of (2, 3, EASY) is a bar put at position X=2, 
/// it has a height of Y=3 and it's colored with the EASY color</example>
public class DataPoint
{
    /// <summary>
    /// X coord, used to define position in space
    /// </summary>
    public float x;
    /// <summary>
    /// Y coord, used to define height of the bar
    /// </summary>
    public float y;
    /// <summary>
    /// Difficulty of the SessionResult (starting difficulty)
    /// </summary>
    /// <remarks>This is used to define the color of the bar</remarks>
    public GameDifficulty gameDifficulty;

    /// <summary>
    /// Ctor of the datapoint
    /// </summary>
    /// <param name="x">X coord</param>
    /// <param name="y">Y coord</param>
    /// <param name="difficultyStart">Difficulty (used to define the color)</param>
    public DataPoint(float x, float y, GameDifficulty difficultyStart)
    {
        this.x = x;
        this.y = y;
        this.gameDifficulty = difficultyStart;
    }
}

