using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject endCanvas;

    private CanvasGroup startGroup;
    private CanvasGroup endGroup;
    private bool gameStarted = false;

    void Start()
    {
        if (startCanvas != null)
            startGroup = startCanvas.GetComponent<CanvasGroup>();
        if (endCanvas != null)
            endGroup = endCanvas.GetComponent<CanvasGroup>();

        Time.timeScale = 0f;
        if (startCanvas != null)
            startCanvas.SetActive(true);
        if (endCanvas != null)
            endCanvas.SetActive(false);

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

        if (endCanvas != null && endCanvas.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
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
                StartCoroutine(FadeInEndScreen());
        }
    }

    IEnumerator FadeInEndScreen()
    {
        float duration = 1.5f;
        endGroup.alpha = 0f;

        float time = 0;
        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            endGroup.alpha = Mathf.Lerp(0, 1, time / duration);
            yield return null;
        }

        endGroup.alpha = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
