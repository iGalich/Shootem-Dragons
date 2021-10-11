using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
{
    public static Hud Instance { get; private set; }

    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject[] playerHearts;

    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void RemoveHeart()
    {
        playerHearts[Player.Instance.Health - 1].SetActive(false);
    }
    public void AddHeart()
    {
        playerHearts[Player.Instance.Health - 1].SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting"); //simple check here since application doesn't quit in editor
        deathMenu.GetComponent<CanvasGroup>().interactable = false;
        deathMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        Application.Quit();
    }
    public void BackToMainMenu()
    {
        deathMenu.GetComponent<CanvasGroup>().interactable = false;
        deathMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        SceneManager.LoadScene(0);
    }
    public void ShowDeathMenu()
    {
        StartCoroutine(ShowDeathMenuCo());
    }
    private IEnumerator ShowDeathMenuCo()
    {
        while (deathMenu.GetComponent<CanvasGroup>().alpha < 1)
        {
            deathMenu.GetComponent<CanvasGroup>().alpha += 0.01f;
            yield return null;
        }

        deathMenu.GetComponent<CanvasGroup>().interactable = true;
        deathMenu.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
