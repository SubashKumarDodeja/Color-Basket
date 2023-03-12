using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public BasketSpawner basketSpawner;
    [SerializeField]
    public Player player;
    [SerializeField]
    public GameObject spikeHurdle;
    public Color objectColor;
    [SerializeField]
    public int spikeHurdleDistance;
    public static int score = 0;
    public GameObject hurdle;
    public Coroutine watchAd;
    public UiController uiController;
    int time;
    private void Start()
    {

        
        time = 5;
        uiController= FindObjectOfType<UiController>();
        Setup();
        StartCoroutine(ShowSmartBanner());
    }
    IEnumerator ShowSmartBanner()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.ShowSmartBanner();
        }
    }
    IEnumerator HideSmartBanner()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.HideSmartBanner();
        }
    }
    public Color RandomColor()
    {
        int x = UnityEngine.Random.Range(1, 8);
        Color tempColor;
        switch (x)
        {
            case 1:
                tempColor = Color.red;
                break;
            case 2:
                tempColor = Color.green;
                break;
            case 3:
                tempColor = Color.black;
                break;
            case 4:
                tempColor = Color.blue;
                break;
            case 5:
                tempColor = Color.cyan;
                break;
            case 6:
                tempColor = Color.yellow;
                break;
            case 7:
                tempColor = Color.magenta;
                break;
            default:
                tempColor = Color.red;
                break;
        }

        return tempColor;
    }


    public void Setup()
    {

        if(hurdle!=null)
            Destroy(hurdle);
        objectColor = RandomColor();
        player.SetColor(objectColor);
        basketSpawner.CorrectInstantiateBasket();
        basketSpawner.currentBasket.SetColor(objectColor);
        GenerateHurdle();
       
    }
    public void GenerateHurdle()
    {
        int basketProbablility = UnityEngine.Random.Range(0, 100);
        int hurdleProbablility = UnityEngine.Random.Range(0, 100);
        if (score > 50)
        {
            basketSpawner.InstantiateEnemyBasket();
            if (hurdleProbablility <= 90)
            {
                GenerateSpikeHurdle();
            }
        }
        else if (score > 40)
        {
            if (basketProbablility <= 80)
            {
                basketSpawner.InstantiateEnemyBasket();
            }
            if (hurdleProbablility <= 80)
            {
                GenerateSpikeHurdle();
            }
        }
        else if (score > 30)
        {
            if (basketProbablility <= 60)
            {
                basketSpawner.InstantiateEnemyBasket();
                
            }
            if (hurdleProbablility <= 70)
            {
                GenerateSpikeHurdle();
            }

        }
        else if (score > 20)
        {
            if (basketProbablility <= 40)
            {
                basketSpawner.InstantiateEnemyBasket();
              
            }
            if (hurdleProbablility <= 60)
            {
                GenerateSpikeHurdle();
            }
        }
        else if (score >= 10)
        {
         
            if (basketProbablility <= 20)
            {

              
                basketSpawner.InstantiateEnemyBasket();
            }
            else
            {

               
                GenerateSpikeHurdle();
            }
        }
    }
    public void LevelEnd()
    {
        Time.timeScale = 0;
        AudioManager.Instance.PlayFailSound();
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.transform.GetChild(0).gameObject.SetActive(true);
        uiController.RewardPanel.SetActive(true);
        watchAd = StartCoroutine(rewardedAd());
        uiController.UpdateAdText(time);

    }
    IEnumerator rewardedAd()
    {
        yield return new WaitForSecondsRealtime(1f);
        time--;
        uiController.UpdateAdText(time);
        if (time>=0)
            StartCoroutine(rewardedAd());
        else
        {

            StartCoroutine(HideSmartBanner());
            yield return new WaitForSecondsRealtime(0.1f);
            if (AdsManager.Instance != null)
            {
                AdsManager.Instance.ShowRewardAd();
            }
            StopCoroutine(watchAd);
        }

    }
    public void RestartButton()
    {
        //AudioManager.Instance.PlayTapSound();
        StopCoroutine(watchAd);
        StartCoroutine(HideSmartBanner());
        Restart();
    }
    void Restart()
    {
        
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void GenerateSpikeHurdle()
    {
        int temp = 0;
       
        hurdle = Instantiate(spikeHurdle);
        hurdle.SetActive(false);
        Vector2 position;
        do
        {
            temp++;
            position = randomVector2();
        }
        while ( (Vector2.Distance(basketSpawner.currentBasket.transform.position, position) <= spikeHurdleDistance 
        ||
        Vector2.Distance(player.transform.position, position) <= spikeHurdleDistance)
        &&
        temp<=10000);
        if(temp< 10000)
        {

            hurdle.gameObject.transform.position = position;
            hurdle.SetActive(true);
        }
    }
    public Vector2 randomVector2()
    {
        float x = UnityEngine.Random.Range(0.15f, 0.85f);
        float y = UnityEngine.Random.Range(0.15f, 0.7f);
        Vector3 pos = new Vector3(x, y, 10.0f);
        pos = Camera.main.ViewportToWorldPoint(pos);

        return pos;
    }

    internal void Reward()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
