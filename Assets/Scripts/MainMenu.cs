using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;


    private void Awake()
    {
        optionsMenu.SetActive(false);
    }

    // Methods for the Buttons
    public void StartWasClicked()
    {
        SceneManager.LoadScene("1");
    }

    public void QuitWasClicked()
    {
        Application.Quit();
    }
}
