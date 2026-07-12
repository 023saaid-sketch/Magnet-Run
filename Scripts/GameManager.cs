using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager inst;

    [SerializeField] Text scoreText;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject successEffect;
    [SerializeField] GameObject letsGoText;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip coinSound;



    public int score = 0;

    public void AddScore(int amount)
    {

        score += amount;

        if (score < 0)
            score = 0;

        scoreText.text = "SCORE : " + score;
    }

    public bool UseScore(int amount)
    {
        if (score >= amount)
        {
            score -= amount;
            scoreText.text = "SCORE : " + score;
            return true;
        }

        return false;
    }
    public void IncrementScore()
    {

        AddScore(1);
        if (coinSound != null)
        {
            audioSource.PlayOneShot(coinSound);
        }

        playerMovement.speed += playerMovement.speedIncreasePerPoint;
    }

    private void Awake ()
    {
        inst = this;
    }

    private void Start () {

	}

	private void Update () {
	
	}

    public void StopPlayer()
    {
        playerMovement.canMove = false;
    }

    public void ContinuePlayer()
    {
        playerMovement.canMove = true;
    }

    public IEnumerator PassCheckpoint(GameObject gate, int requiredScore)
    {
        // توقف بازیکن
        StopPlayer();

        // کمی مکث جلوی گیت
        yield return new WaitForSeconds(0.2f);

        // اگر امتیاز کافی باشد
        if (UseScore(requiredScore))
        {
            // اجرای افکت موفقیت
            if (successEffect != null)
            {
                Instantiate(successEffect, gate.transform.position, Quaternion.identity);
            }

            // نمایش متن LET'S GO!
            if (letsGoText != null)
            {
                StartCoroutine(ShowLetsGo());
            }

            // کمی صبر برای دیده شدن افکت
            yield return new WaitForSeconds(1.5f);

            // حذف گیت
            Destroy(gate);

            // ادامه حرکت
            ContinuePlayer();
        }
        else
        {
            Debug.Log("Not enough score");

            playerMovement.Die();

            yield return new WaitForSeconds(1f);

            GameOver();
        }
    }

    private IEnumerator ShowLetsGo()
    {
        letsGoText.SetActive(true);

        yield return new WaitForSeconds(1.9f);

        letsGoText.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");

        StopPlayer();

        gameOverPanel.SetActive(true);


    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}