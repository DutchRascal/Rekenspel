using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{

    public string maxNumber1 = "10";
    public string maxNumber2 = "20";
    public string tafel = " 1, 2, 3, 10 ";
    public int tafelMax = 1;

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
            tafelMax = GetMax(data.tafel);
        }
    }

    public int GetMax(string tafel)
    {
        int max = 1;
        var textSplit = tafel.Split(","[0]);
        foreach (string tmpString in textSplit)
        {
            int tmp;
            var tmpInt = int.TryParse(tmpString, out tmp);
            if (tmp > max)
            {
                max = tmp;
            }
        }
        return max;
    }
}
