using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _difficultyMenu;
    [SerializeField] private GameObject _backgroundMusic;


    private void Awake()
    {
        DontDestroyOnLoad(_backgroundMusic);
    }
    public void LevelTransition(string levelString)
    {
        SceneManager.LoadScene(levelString);
    }

    public void ToggleOptions()
    {
        _optionsMenu.SetActive(!_optionsMenu.activeInHierarchy);
    }

    public void ToggleDifficulty()
    {
        _difficultyMenu.SetActive(!_difficultyMenu.activeInHierarchy);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
