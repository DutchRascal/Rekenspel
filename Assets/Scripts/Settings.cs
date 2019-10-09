using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    EventSystem system;


    void Start()
    {
        system = EventSystem.current;
        inputNumber1 = GameObject.Find("Input Getal 1").GetComponent<InputField>();
        inputNumber2 = GameObject.Find("Input Getal 2").GetComponent<InputField>();
        inputTafels = GameObject.Find("Input Tafels").GetComponent<InputField>();
        player = FindObjectOfType<Player>();
        player.LoadPlayer();
        inputNumber1.ActivateInputField();
        UpdateFields(player);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(system));
                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }
    }

    public void SaveChanges()
    {
        char[] correct = CheckInputVaues(textNumber1, textNumber2, textTafels);
        int pos = Array.IndexOf(correct, '1');
        if (pos < 0)
        {
            player.maxNumber1 = textNumber1.text;
            player.maxNumber2 = textNumber2.text;
            player.tafel = textTafels.text;
            player.SavePlayer();
            StartCoroutine("WaitForSave");
            // SceneManager.LoadScene("Menu");
        }
        else
        {
            {
                HighLightWrongFields(correct);
            }
        }
    }

    private void HighLightWrongFields(char[] correct)
    {
        if (correct[2] == '1')
        {
            inputTafels.image.color = Color.red;
            inputTafels.ActivateInputField();
        }
        else
        {
            inputTafels.image.color = Color.white;
        }
        if (correct[1] == '1')
        {
            inputNumber2.image.color = Color.red;
            inputNumber2.ActivateInputField();
        }
        else
        {
            inputNumber2.image.color = Color.white;
        }
        if (correct[0] == '1')
        {
            inputNumber1.image.color = Color.red;
            inputNumber1.ActivateInputField();
        }
        else
        {
            inputNumber1.image.color = Color.white;
        }
    }

    private char[] CheckInputVaues(Text textNumber1, Text textNumber2, Text textTafels)
    {
        int number1;
        int number2;
        int intTafel;
        string[] tafels;
        char[] correct = new char[3];
        string stringTafels = textTafels.text;
        if (!int.TryParse(textNumber1.text, out number1)) { correct[0] = '1'; }
        if (!int.TryParse(textNumber2.text, out number2)) { correct[1] = '1'; }
        tafels = stringTafels.Split(',');
        if (tafels.Length > 0)
        {
            for (int i = 0; i < tafels.Length; i++)
            {
                bool isInt = int.TryParse(tafels[i], out intTafel);
                if (!isInt) { correct[2] = '1'; break; }
                if (intTafel == 0) { correct[2] = '1'; break; }
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
