using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float jumpForceY;
    [SerializeField]
    float jumpForceX;
    [SerializeField]
    Rigidbody2D rigidbody;

    public Color color;

    bool firstTouch = false;


    SpriteRenderer renderers;

    bool isWrappingX = false;
    bool isWrappingY = false;
    bool tapSoundOnce = true;
    private void Start()
    {
        tapSoundOnce = true;
        Time.timeScale = 0;
        firstTouch = false;
        rigidbody = GetComponent<Rigidbody2D>();
        
        renderers = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        //if (Input.touchCount > 0)
        //{
        //    var touch = Input.GetTouch(0);
        //    if (touch.position.x < Screen.width / 2)
        //    {
        //        rigidbody.velocity = new Vector2(jumpForceX, jumpForceY);
        //    }
        //    else if (touch.position.x > Screen.width / 2)
        //    {
        //        rigidbody.velocity = new Vector2(-jumpForceX, jumpForceY);
        //    }
        //}

        
        if (Input.GetMouseButton(0))
        { 


            if(!firstTouch)
            {
                firstTouch = true;
                FindObjectOfType<UiController>().DisablePlayButton();
                Time.timeScale = 1;
                return;

            }
            else
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (tapSoundOnce)
                {
                    tapSoundOnce = false;
                    AudioManager.Instance.PlayTapSound();
                    Invoke("tapSoundTrue", 0.1f);
                }
                    
                if (worldPosition.x >= 0)
                {
                    rigidbody.velocity = new Vector2(jumpForceX, jumpForceY);
                }
                else if (worldPosition.x < 0)
                {
                    rigidbody.velocity = new Vector2(-jumpForceX, jumpForceY);
                }
            }
            
        }

        ScreenWrap();
    }


    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * -50, Space.World);

    }
    void tapSoundTrue()
    {
        tapSoundOnce = true;
    }

    bool CheckRenderers()
    {
        
            // If at least one render is visible, return true
            if (renderers.isVisible)
            {
                return true;
            }
        

        // Otherwise, the object is invisible
        return false;
    }

    void ScreenWrap()
    {
        var isVisible = CheckRenderers();

        if (isVisible)
        {
            isWrappingX = false;
            //isWrappingY = false;
            return;
        }

        //if (isWrappingX && isWrappingY)
        //{
        //    return;
        //}
        if (isWrappingX )
        {
            return;
        }

        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;

            isWrappingX = true;
        }

        //if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        //{
        //    newPosition.y = -newPosition.y;

        //    isWrappingY = true;
        //}

        transform.position = newPosition;
    }
    public void SetColor(Color c)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = c;
        color = c;
    }


   

}
