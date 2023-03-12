using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Sprite[] logoList =  new Sprite[4];
    public Image logo;

    // Start is called before the first frame update
    void Start()
    {
        logo.sprite = logoList[Random.Range(0, 4)];
    }

    
    public void PlayGame()
    {
        StartCoroutine(Play());
        AudioManager.Instance.PlayTapSound();
    }
    IEnumerator Play()
    {
        yield return new WaitForSeconds(0.001f);
        if(AdsManager.Instance != null)
            {
            AdsManager.Instance.ShowInterstitialAd();
        }



        SceneManager.LoadScene(1);
    }
    public void ShareButton()
    {
        StartCoroutine(Share());
    }
    IEnumerator Share()
    {
        print("Application.identifier:" + Application.identifier);
        yield return new WaitForSeconds(0.001f);
        AudioManager.Instance.PlayTapSound();
        new NativeShare().SetSubject("Hello!😊 Can your score 120+? Download Now Color Basket 2020! Download for free now! ")
            .SetText("https://play.google.com/store/apps/details?id=" +
            Application.identifier).Share();
    }
}
