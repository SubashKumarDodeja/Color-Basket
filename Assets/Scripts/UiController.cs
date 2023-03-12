using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public GameObject PlayButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    public TextMeshProUGUI adText;
    public GameObject RewardPanel;
    void Start()
    {
        UpdateScore();
        maxScoreText.text = PlayerPrefs.GetInt("MaxScore", 0).ToString();
    }
    public void DisablePlayButton()
    {
        PlayButton.SetActive(false);
    }
    public void UpdateScore()
    {
        scoreText.text = GameManager.score.ToString();
        if(GameManager.score > PlayerPrefs.GetInt("MaxScore", 0))
        {
            PlayerPrefs.SetInt("MaxScore", GameManager.score);
            maxScoreText.text = PlayerPrefs.GetInt("MaxScore", 0).ToString();

        }
    }
    public void UpdateAdText(int time)
    {
        time = time <= 0 ? 0 : time;  
        adText.text = "WANT TO MAKE HIGHSCORE ? AD IN " + time + " SECONDS ";
    }
}
