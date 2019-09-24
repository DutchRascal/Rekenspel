using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{

    public string maxNumber1;
    public string maxNumber2;
    public string tafel;

    public PlayerData(Player player)
    {
        maxNumber1 = player.maxNumber1;
        maxNumber2 = player.maxNumber2;
        tafel = player.tafel;
    }
}
