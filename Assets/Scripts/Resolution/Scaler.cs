using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField]
    public float scaleFactor=14f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        
        float width = ScreenSize.GetScreenToWorldWidth;
        transform.localScale = Vector3.one * width * scaleFactor;
    }
}
