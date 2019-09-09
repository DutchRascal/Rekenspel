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

    InputField input;
    string answer;
    int wrongScore;
    int correctScore;

    private void Awake()
    {
        input = GameObject.Find("InputField").GetComponent<InputField>();
        correctScoreText.text = "0";
        wrongScoreText.text = "0";
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateNewNumbers(); sign.text = "+";
    }

    public void GenerateNewNumbers()
    {
        input.text = "";
        input.image.color = Color.white;
        number1.text = UnityEngine.Random.Range(1, 20).ToString();
        number2.text = UnityEngine.Random.Range(1, 20).ToString();
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
        correctScoreText.text = correctScore.ToString();
        wrongScoreText.text = wrongScore.ToString();
    }

    private IEnumerator WaitRoutine()
    {
        yield return new WaitForSeconds(waitTime);
        GenerateNewNumbers();
    }
}
