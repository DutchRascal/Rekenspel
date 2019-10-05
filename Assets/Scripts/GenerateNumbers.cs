using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GenerateNumbers : MonoBehaviour
{
    [SerializeField] Text number1;
    [SerializeField] Text number2;
    [SerializeField] Text sign;
    [SerializeField] float waitTime = 2;
    [SerializeField] Text correctScoreText;
    [SerializeField] Text wrongScoreText;
    [SerializeField] int maxNumber1 = 20;
    [SerializeField] int maxNumber2 = 10;
    [SerializeField] Image thumbUp;
    [SerializeField] Image thumbDown;

    InputField input;
    string answer;
    int wrongScore, correctScore;
    float desiredNumber, initialNumber, currentNumber;
    float animationTime = 1.5f;
    Player player;
    string currentScene;

    private void Awake()
    {
        input = GameObject.Find("InputField").GetComponent<InputField>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        player.LoadPlayer();
        correctScoreText.text = "0";
        wrongScoreText.text = "0";
        thumbUp.enabled = false;
        thumbDown.enabled = false;
        GenerateNewNumbers();
        input.ActivateInputField();
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Add")
        {
            sign.text = "+";
        }
        else if (currentScene == "Substract")
        {
            sign.text = "-";
        }
    }

    private void Update()
    {
        if (currentNumber != desiredNumber)
        {
            if (initialNumber < desiredNumber)
            {
                currentNumber += (animationTime * Time.deltaTime) * (desiredNumber - initialNumber);
                if (currentNumber >= desiredNumber)
                    currentNumber = desiredNumber;
            }
            else
            {
                currentNumber -= (animationTime * Time.deltaTime) * (initialNumber - desiredNumber);
                if (currentNumber <= desiredNumber)
                    currentNumber = desiredNumber;
            }
        }
    }

    public void GenerateNewNumbers()
    {
        input.text = "";
        input.image.color = Color.white;
        int num1Int = Mathf.FloorToInt(UnityEngine.Random.Range(1, int.Parse(player.maxNumber1)));
        int num2Int = Mathf.FloorToInt(UnityEngine.Random.Range(1, int.Parse(player.maxNumber2)));
        print(num1Int + " / " + num2Int);
        if (currentScene == "Substract")
        {
            print(num1Int + " / " + num2Int);
            if (num1Int < num2Int)
            {
                print("Swapped");
                int tmpInt = num1Int;
                num1Int = num2Int;
                num2Int = tmpInt;
            }
        }
        number1.text = num1Int.ToString();
        number2.text = num2Int.ToString();

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
        if (currentScene == "Add")
        {
            int answer = num1 + num2;
            if (answer == number)
            {
                input.image.color = Color.green;
                UpdateScore(0, 1);
            }
        }
        else if (currentScene == "Substract")
        {
            int answer = num1 - num2;
            if (answer == number)
            {
                input.image.color = Color.green;
                UpdateScore(0, 1);
            }
        }

        else
        {
            input.image.color = Color.red;
            input.text = answer.ToString();
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


