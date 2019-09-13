using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GenerateNumbers : MonoBehaviour
{
    [SerializeField] Text number1;
    [SerializeField] Text number2;
    [SerializeField] Text sign;
    [SerializeField] float waitTime = 2;
    [SerializeField] Text correctScoreText;
    [SerializeField] Text wrongScoreText;
    [SerializeField] int maxNumber = 10;
    [SerializeField] Image thumbUp;
    [SerializeField] Image thumbDown;

    InputField input;
    string answer;
    int wrongScore;
    int correctScore;

    private void Awake()
    {
        input = GameObject.Find("InputField").GetComponent<InputField>();
        input.ActivateInputField();
    }

    void Start()
    {
        correctScoreText.text = "0";
        wrongScoreText.text = "0";
        thumbUp.enabled = false;
        thumbDown.enabled = false;
        GenerateNewNumbers();
        sign.text = "+";
    }

    public void GenerateNewNumbers()
    {
        input.text = "";
        input.image.color = Color.white;
        number1.text = UnityEngine.Random.Range(1, maxNumber).ToString();
        number2.text = UnityEngine.Random.Range(1, maxNumber).ToString();
    }

    public void GetInput(string answer)
    {
        input.ActivateInputField();
        int number;
        bool answerIsInt = int.TryParse(answer, out number);
        if (!answerIsInt)
        {
            input.image.color = Color.red;
        }
        else
        {
            CheckAnswer(number);
        }
        StartCoroutine("WaitRoutine");
    }

    private void CheckAnswer(int number)
    {
        int num1 = Convert.ToInt32(number1.text);
        int num2 = Convert.ToInt32(number2.text);

        if (num1 + num2 == number)
        {
            input.image.color = Color.green;
            UpdateScore(0, 1);
        }
        else
        {
            input.image.color = Color.red;
            UpdateScore(1, 0);
        }
    }

    private void UpdateScore(int wrong, int correct)
    {
        correctScore += correct;
        wrongScore += wrong;
        correctScoreText.text = correctScore.ToString("0");
        wrongScoreText.text = wrongScore.ToString("0");
        UpdateThumb();
    }

    private void UpdateThumb()
    {
        if (correctScore > wrongScore)
        {
            thumbUp.enabled = true;
            thumbDown.enabled = false;
        }
        else
        {
            thumbDown.enabled = true;
            thumbUp.enabled = false;
        }
    }

    private IEnumerator WaitRoutine()
    {
        yield return new WaitForSeconds(waitTime);
        GenerateNewNumbers();
    }
}


