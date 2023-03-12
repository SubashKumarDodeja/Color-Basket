using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBg : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    
    Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        if(transform.position.y < -1.53f)
        {
            transform.position = startPosition;
        }
    }
}
