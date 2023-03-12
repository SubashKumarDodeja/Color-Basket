using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckCollision : MonoBehaviour
{
   
    void OnTriggerEnter2D(Collider2D other)
    {
        print("trigger");
        if (other.gameObject.CompareTag("Player"))
        {
            print("triggerplayer");
            if (other.gameObject.GetComponent<Player>())
            {
                if (other.gameObject.GetComponent<Player>().color.Equals(gameObject.GetComponent<Basket>().color))
                {
                    AudioManager.Instance.PlayBasketSound();
                    GameManager gm = FindObjectOfType<GameManager>();
                    
                    if(FindObjectOfType<BasketSpawner>().Enemy!=null)
                        Destroy(FindObjectOfType<BasketSpawner>().Enemy.gameObject);
                    Destroy(this.gameObject.transform.gameObject);

                    GameManager.score++;
                    
                    gm.Setup();
                    //Destroy(gm.hurdle);
                    print("success");
                    
                    gm.uiController.UpdateScore();
                }
                else
                {

                   
                    FindObjectOfType<GameManager>().LevelEnd();
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            if (other.gameObject.GetComponent<Player>().color == this.gameObject.GetComponent<Basket>().color)
            {

            }
            else
            {

                FindObjectOfType<GameManager>().LevelEnd();
            }
        }
    }
}
