using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void LoadScene(int scene)
    {
        switch (scene)
        {
            case 1:
                SceneManager.LoadScene("Add");
                break;
            case 2:
                SceneManager.LoadScene("Substract");
                break;
            case 3:
                SceneManager.LoadScene("Divide");
                break;
        }
    }
}
