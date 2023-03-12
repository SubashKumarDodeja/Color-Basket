using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketSpawner : MonoBehaviour
{
    public Basket basket;
    public Basket Enemy;
    public Basket currentBasket;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    float distance;
    void Start()
    {
        if(gameManager==null)
            gameManager = FindObjectOfType<GameManager>();
    }
    public void CorrectInstantiateBasket()
    {
        currentBasket =  Instantiate(basket);
        Vector2 position = randomVector2();
   
        currentBasket.gameObject.transform.position = position;
        
    }

    public void InstantiateEnemyBasket()
    {
        int temp = 0;
        Enemy = Instantiate(basket);
        Enemy.gameObject.SetActive(false);
        Vector2 position;
        Color _color;
        do {
            temp++;
            position = randomVector2();
        }
        while ((Vector2.Distance(currentBasket.transform.position, position) <= distance 
        ||
        Vector2.Distance(gameManager.player.transform.position, position) <= distance)
        && 
        temp <= 10000);
        Enemy.gameObject.transform.position = position;
        do
            _color = gameManager.RandomColor();
        while (_color == currentBasket.color);
        Enemy.SetColor(_color);
        if (temp < 10000)
        {
            Enemy.gameObject.SetActive(true);
        }
        


    }
    public Vector2 randomVector2()
    {
        float x = Random.Range(0.15f, 0.85f);
        float y = Random.Range(0.15f, 0.7f);
        Vector3 pos = new Vector3(x, y, 10.0f);
        pos = Camera.main.ViewportToWorldPoint(pos);

        return pos;
    }


    
}
