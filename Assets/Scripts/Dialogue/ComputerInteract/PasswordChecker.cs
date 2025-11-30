using UnityEngine;
using TMPro; 

public class PasswordChecker : MonoBehaviour
{
    public TMP_InputField input;
    public string correctPassword = "7482";
    public GameObject successScreen;

    public void Check()
    {
        if (input.text == correctPassword)
        {
            successScreen.SetActive(true);
            Debug.Log("Пароль верный");
        }
        else
        {
            Debug.Log("Неверно");
        }
    }

}
