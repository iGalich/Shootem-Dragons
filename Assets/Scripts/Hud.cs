using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Hud : MonoBehaviour
{
    //public static Hud Instance { get; private set; }

    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject newHighScoreText;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private RectTransform hpBarFront, hpBarBack;
    [SerializeField] private GameObject[] playerHearts;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject menuButtonCredits;
    [SerializeField] private GameObject newHighScoreCredits;

    public RectTransform HpBarBack { get => hpBarBack; set => hpBarBack = value; }
    public RectTransform HpBarFront { get => hpBarFront; set => hpBarFront = value; }
    public GameObject DeathMenu { get => deathMenu; set => deathMenu = value; }
    private void FixedUpgate()
    {
        if (hpBarBack.localScale.x > hpBarFront.localScale.x)
            SyncBar();
    }
    private void Start()
    {
        newHighScoreText.SetActive(false);
        bossHealthBar.SetActive(false);
        newHighScoreCredits.SetActive(false);
    }
    private void SyncBar()
    {
        hpBarBack.localScale = new Vector3(Mathf.Lerp(hpBarBack.localScale.x, hpBarFront.localScale.x, Time.unscaledDeltaTime), hpBarBack.localScale.y, hpBarBack.localScale.z);
    }
    public void RemoveHeart()
    {
        //playerHearts[Player.Instance.Health - 1].SetActive(false);
        playerHearts[GameManager.Instance.player.Health - 1].SetActive(false);
    }
    public void AddHeart()
    {
        //playerHearts[Player.Instance.Health - 1].SetActive(true);
        playerHearts[GameManager.Instance.player.Health - 1].SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting"); //simple check here since application doesn't quit in editor
        deathMenu.GetComponent<CanvasGroup>().interactable = false;
        deathMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        Application.Quit();
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMainMenu()
    {
        //deathMenu.GetComponent<CanvasGroup>().interactable = false;
        //deathMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //SceneManager.LoadScene(1);
        //SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //SceneManager.LoadScene("Test");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GameManager.Instance.FindObjects();
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
    public void EnableNewHighScoreText()
    {
        newHighScoreText.SetActive(true);
    }
    public void EnableNewHighScoreTextInCredits()
    {
        newHighScoreCredits.SetActive(true);
    }
    public void ShowBlackScreen()
    {
        StartCoroutine(ShowBlackScreenCo());
    }
    private IEnumerator ShowBlackScreenCo(bool end = false)
    {
        while (blackScreen.GetComponent<CanvasGroup>().alpha < 1)
        {
            blackScreen.GetComponent<CanvasGroup>().alpha += 0.01f;
            yield return null;
        }
        if (blackScreen.GetComponent<CanvasGroup>().alpha >= 1)
        {
            if (!end)
            { 
                bossHealthBar.SetActive(true);
                GameManager.Instance.ActivateBoss();
                StartCoroutine(DisableBlackScreenCo()); 
            }
            else
            {
                EnableCredits();
            }
        }
    }
    private IEnumerator DisableBlackScreenCo()
    {
        while (blackScreen.GetComponent<CanvasGroup>().alpha > 0)
        {
            blackScreen.GetComponent<CanvasGroup>().alpha -= 0.01f;
            yield return null;
        }
    }
    public void DisableBossHealthBar()
    {
        StartCoroutine(DisableBossHealthBarCo());
    }
    private IEnumerator DisableBossHealthBarCo()
    {
        while (bossHealthBar.GetComponent<CanvasGroup>().alpha > 0)
        {
            if (Random.value > 0.95f)
                AudioManager.Instance.Play("BossDragonDie");

            bossHealthBar.GetComponent<CanvasGroup>().alpha -= 0.01f;
            yield return null;
        }
        if (bossHealthBar.GetComponent<CanvasGroup>().alpha == 0)
        {
            StartCoroutine(ShowBlackScreenCo(true));
        }
    }
    private void EnableCredits()
    {
        StartCoroutine(ShowCreditsCo());
        creditsScreen.GetComponent<CanvasGroup>().interactable = true;
        creditsScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;
        ScoreManager.Instance.CheckScore();
        GameManager.Instance.player.IsAlive = false;
    }
    private IEnumerator ShowCreditsCo()
    {
        while (creditsScreen.GetComponent<CanvasGroup>().alpha < 1)
        {
            creditsScreen.GetComponent<CanvasGroup>().alpha += 0.01f;
            yield return null;
        }
    }
}
