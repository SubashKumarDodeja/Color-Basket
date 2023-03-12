using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{

    public Color color;
    public void SetColor(Color c)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = c;
        color = c;
    }
    
}
