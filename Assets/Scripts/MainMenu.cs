using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }

    [SerializeField] private GameObject gameTitle;
    [SerializeField] private GameObject aboutScreen;
    [SerializeField] private GameObject aboutText;

    [SerializeField] private Toggle toggleAutofire;

    [SerializeField] private AudioSource buttonClick;

    public Toggle ToggleAutoFire => toggleAutofire;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        gameTitle.transform.position = new Vector3(gameTitle.transform.position.x, gameTitle.transform.position.y + Mathf.Sin(Time.time) * 0.06f, 0);
        aboutText.transform.position = new Vector3(aboutText.transform.position.x, aboutText.transform.position.y + Mathf.Sin(Time.time) * 0.03f, 0);
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
    public void ClickButton()
    {
        buttonClick.pitch = 1f;
        if (Random.value > 0.5)
        {
            if (Random.value > 0.5)
                buttonClick.pitch += 0.15f;
            else
                buttonClick.pitch -= 0.15f;
        }
        buttonClick.Play();
    }
}
