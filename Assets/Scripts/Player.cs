using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public string maxNumber1 = "10";
    public string maxNumber2 = "20";
    public string tafel = " 1, 2, 3, 10 ";

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            maxNumber1 = data.maxNumber1;
            maxNumber2 = data.maxNumber2;
            tafel = data.tafel;
        }
    }
}
