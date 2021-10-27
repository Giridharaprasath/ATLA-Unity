using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Single Player Game Data
[System.Serializable]
public class SPGD
{
    public float[] position = new float[3];
    public float[] rotation = new float[3];

    public SPGD(SPLSS player)
    {
        position[0] = player.playerPosition.x;
        position[1] = player.playerPosition.y;
        position[2] = player.playerPosition.z;
        
        rotation[0] = player.playerRotation.x;
        rotation[1] = player.playerRotation.y;
        rotation[2] = player.playerRotation.z;
    }
}
