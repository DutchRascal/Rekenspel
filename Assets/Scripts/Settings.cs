using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    PlayerData data;
    Player player;
    InputField inputNumber1;
    InputField inputNumber2;
    InputField inputTafels;
    public Text textNumber1;
    public Text textNumber2;
    public Text textTafels;

    private void Awake()
    {
        inputNumber1 = GameObject.Find("Input Getal 1").GetComponent<InputField>();
        inputNumber2 = GameObject.Find("Input Getal 2").GetComponent<InputField>();
        inputTafels = GameObject.Find("Input Tafels").GetComponent<InputField>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.LoadPlayer();
        UpdateFields(player);
    }


    public void SaveChanges()
    {
        int correct = CheckInputVaues(textNumber1, textNumber2, textTafels);
        if (correct == 0)
        {
            player.maxNumber1 = textNumber1.text;
            player.maxNumber2 = textNumber2.text;
            player.tafel = textTafels.text;
            player.SavePlayer();
            StartCoroutine("WaitForSave");
            // SceneManager.LoadScene("Menu");
        }
    }

    private int CheckInputVaues(Text textNumber1, Text textNumber2, Text textTafels)
    {
        int number1;
        int number2;
        int intTafel;
        string[] tafels;
        int correct = 0;
        string stringTafels = textTafels.text;
        if (!int.TryParse(textNumber1.text, out number1)) { correct += 1; }
        if (!int.TryParse(textNumber2.text, out number2)) { correct += 10; }
        tafels = stringTafels.Split(',');
        if (tafels.Length > 0)
        {
            for (int i = 0; i < tafels.Length; i++)
            {
                if (!int.TryParse(tafels[i], out intTafel)) { correct += 100; break; }
            }
        }
        return correct;
    }

    private void UpdateFields(Player player)
    {
        inputNumber1.text = player.maxNumber1;
        inputNumber2.text = player.maxNumber2;
        inputTafels.text = player.tafel;
    }

    IEnumerator WaitForSave()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");
    }

}
