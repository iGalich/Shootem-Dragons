using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameTitle;
    [SerializeField] private GameObject aboutScreen;

    private void FixedUpdate()
    {
        gameTitle.transform.position = new Vector3(gameTitle.transform.position.x, gameTitle.transform.position.y + Mathf.Sin(Time.time) * 0.06f, 0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OpenAbout()
    {
        aboutScreen.SetActive(true);
    }
    public void CloseAbout()
    {
        aboutScreen.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting"); //simple check here since application doesn't quit in editor
        Application.Quit();
    }
}
