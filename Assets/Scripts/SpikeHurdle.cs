using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHurdle : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * -100, Space.World);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().LevelEnd();
        }
    }

}
