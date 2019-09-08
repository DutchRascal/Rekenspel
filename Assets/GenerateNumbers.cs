using UnityEngine;
using UnityEngine.UI;

public class GenerateNumbers : MonoBehaviour
{
    [SerializeField] Text number1;
    [SerializeField] Text number2;
    [SerializeField] Text sign;
    [SerializeField] Text answer;

    // Start is called before the first frame update
    void Start()
    {
        number1.text = Random.Range(0, 20).ToString();
        number2.text = Random.Range(0, 20).ToString();
        sign.text = "+";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetInput()
    {
        if (answer.text != "")
        {
            print(answer.text);
        }
    }
}
