using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject menu;

    private void Start()
    {
        menu.SetActive(true);
        game.SetActive(false);
    }
    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Debug.Log("start");
        menu.SetActive(false);
        game.SetActive(true);        
    }

    public void ExitGame()
    {
        //Debug.Log("Zamknij");
        Application.Quit();
    }
}
