using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject endCanvas;
    public GameObject winCanvas;

    private CanvasGroup startGroup;
    private CanvasGroup endGroup;
    private CanvasGroup winGroup;
    private bool gameStarted = false;

    void Start()
    {
        if (startCanvas != null)
            startGroup = startCanvas.GetComponent<CanvasGroup>();
        if (endCanvas != null)
            endGroup = endCanvas.GetComponent<CanvasGroup>();
        if (winCanvas != null)
            winGroup = winCanvas.GetComponent<CanvasGroup>();

        Time.timeScale = 0f;
        if (startCanvas != null)
            startCanvas.SetActive(true);
        if (endCanvas != null)
            endCanvas.SetActive(false);
        if (winCanvas != null)
            winCanvas.SetActive(false);

        // make start screen visible
        if (startGroup != null)
            startGroup.alpha = 1f;
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FadeOutStartScreen());
        }

        if ((endCanvas != null && endCanvas.activeSelf) || (winCanvas != null && winCanvas.activeSelf))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }
    }

    IEnumerator FadeOutStartScreen()
    {
        gameStarted = true;
        float duration = 1f; // fade time in seconds
        float startAlpha = startGroup.alpha;

        float time = 0;
        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            startGroup.alpha = Mathf.Lerp(startAlpha, 0, time / duration);
            yield return null;
        }

        startGroup.alpha = 0f;
        startCanvas.SetActive(false);
        Time.timeScale = 1f; // unpause
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        if (endCanvas != null)
        {
            endCanvas.SetActive(true);
            if (endGroup != null)
                StartCoroutine(FadeInCanvas(endGroup));
        }
    }

    public void WinGame()
    {
        Debug.Log("WIN: Showing WinCanvas, hiding StartCanvas");

        Time.timeScale = 0f;
        if (winCanvas != null)
        {
            winCanvas.SetActive(true);
            if (winGroup != null)
                StartCoroutine(FadeInCanvas(winGroup));
        }
        if (startCanvas != null)
            startCanvas.SetActive(false);
    }

    IEnumerator FadeInCanvas(CanvasGroup canvasGroup)
    {
        float duration = 1.5f;
        canvasGroup.alpha = 0f;
        float time = 0;
        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, time / duration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
