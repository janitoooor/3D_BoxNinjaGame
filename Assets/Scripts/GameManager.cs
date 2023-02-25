using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _targets;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Image pausePanel;

    [SerializeField] private Button _restartButton;

    public GameObject voiceScreen;
    public GameObject titleScreen;

    public Slider voiceSlider;
    public Slider pauseVoiceSlider;
    public Slider effectsSlider;
    public Slider pauseEffectsSlider;
    public AudioSource gameOverVoice;
    private int score;
    private int lives = 3;

    private float spawnRate = 1f;
    private int livesToEnd = 1;

    public bool isGameActive;
    public bool isPauseActive;

    IEnumerator SpawnTarget()
    {
        while (isGameActive)// öèêë, ðàáîòàåò ïîñòîÿííî ïðè ëîãè÷åñêîì óñëîâèè true/åñëè false îí âûêëþ÷àåòñÿ.
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(_targets[index]);
        }
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score:" + score;
    }

    public void UpdateLives(int livesToAdd)
    {
        lives += livesToAdd;
        livesText.text = "Lives:" + lives;
        if(lives < livesToEnd)
            GameOver();
    }

    public void GameOver()
    {
        _restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        gameOverVoice.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;

        spawnRate /= difficulty;

        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);

        titleScreen.gameObject.SetActive(false);
        voiceScreen.gameObject.SetActive(false);
    }

    void SetPause()
    {
        if (Input.GetKeyDown(KeyCode.F9) && !isPauseActive && isGameActive)
        {
            Time.timeScale = 0;
            isPauseActive = true;
            pausePanel.gameObject.SetActive(true);
            pauseVoiceSlider.value = voiceSlider.value;
            pauseEffectsSlider.value = effectsSlider.value;
        }
        else if (Input.GetKeyDown(KeyCode.F9) && isPauseActive && isGameActive)
        {
            Time.timeScale = 1;
            isPauseActive = false;
            pausePanel.gameObject.SetActive(false);
            voiceSlider.value = pauseVoiceSlider.value;
            effectsSlider.value = pauseEffectsSlider.value;
        }
    }
}
