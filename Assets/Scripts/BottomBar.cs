using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BottomBar : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().LevelEnd();
           
        }     
    }
}
