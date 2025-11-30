using UnityEngine;
using TMPro;

public class Computer : MonoBehaviour
{
    [Header("UI")]
    public GameObject computerUI;          
    public TMP_InputField passwordInput;   
    public GameObject successScreen;       

    [Header("Password")]
    public string correctPassword = "7482";

    private bool inside = false;
    [HideInInspector] public bool computerOpen = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) inside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) inside = false;
    }

    private void Update()
    {
        if (inside && Input.GetKeyDown(KeyCode.E))
        {
            OpenComputer();
        }
    }

    public void OpenComputer()
    {
        computerUI.SetActive(true);
        computerOpen = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        passwordInput.text = ""; 
    }

    public void ExitComputer()
    {
        computerUI.SetActive(false);
        computerOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CheckPassword()
    {
        if (passwordInput.text == correctPassword)
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
