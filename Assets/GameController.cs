using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool IsGameOver;
    public bool IsGameBegins;
    public int Score {get; private set;}
    public int HiScore {get; private set;}
    [SerializeField] Text _scoreText;
    [SerializeField] Text _hiScoreText;
    [SerializeField] Text _spaceToStartText;
    [SerializeField] GameObject _gameOverTexts;
    private void Update()
    {
        if (IsGameOver && Input.GetKeyDown(KeyCode.Return) && !IsGameBegins)
        {
            ResetGame();
        }
    }
    void ResetGame()
    {
        PipesSpawnerController pipesSpawner = GameObject.FindGameObjectWithTag("PipesSpawner")
            .GetComponent<PipesSpawnerController>();
        pipesSpawner.PipesCount = 0;

        GameObject[] pipes = GameObject.FindGameObjectsWithTag("PipeWrapper");
        for (int i =0 ; i < pipes.Length; i++)
        {
            if (pipes[i] != null)
            {
                Destroy(pipes[i].gameObject);
            }
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform playerTransf = player.transform;
        playerTransf.position = new Vector3(-3.5f, 0f, 0f);
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.bodyType = RigidbodyType2D.Kinematic;
        if (playerRb != null)
            playerRb.velocity = Vector2.zero;
        IsGameOver = false;

        Score = 0;

        UpdateScoreText();
        _spaceToStartText.gameObject.SetActive(true);
        _gameOverTexts.gameObject.SetActive(false);
    }
    public void UpdateHiScore()
    {
        HiScore = Math.Max(Score, HiScore);
    }
    public void IncreaseScore()
    {
        Score++;
        UpdateHiScore();
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        _scoreText.text = Score.ToString();
        _hiScoreText.text = HiScore.ToString();
    }
    public void BeginGame()
    {
        _spaceToStartText.gameObject.SetActive(false);
    }
    public void SetGameOver()
    {
        _gameOverTexts.gameObject.SetActive(true);
    }
}
