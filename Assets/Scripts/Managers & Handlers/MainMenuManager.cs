using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject canvasPanel;

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Main Level");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnCreditsButtonClicked()
    {
        canvasPanel.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        canvasPanel.SetActive(false);
    }
}
