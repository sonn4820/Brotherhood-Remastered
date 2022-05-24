using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public Transform movePoint;

    private float inputDelay = 0.3f;
    private float timer;
    private bool canPress = true;
    private bool isPressed;

    public LayerMask obstructionLayer;
    public LayerMask playerLayer;
    void Start()
    {
        movePoint.parent = null;
        timer = inputDelay;
    }

    // Update is called once per frame
    void Update()
    {
        #region Moving
        // Delay Input 
        if (timer <= 0)
        {
            isPressed = false;
            timer = inputDelay;
            canPress = true;
        }
        if (isPressed == true)
        {
            canPress = false;
            timer -= Time.deltaTime;
        }
        // Move the player to the movePoint
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        // Grid-based movement system
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (canPress == true)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, obstructionLayer))
                    {
                        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, playerLayer))
                        {
                            movePoint.position += Vector3.zero;
                        }
                        else
                        {
                            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        }
                        isPressed = true;
                    }

                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, obstructionLayer))
                    {
                        if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, playerLayer))
                        {
                            movePoint.position += Vector3.zero;
                        }
                        else
                        {
                            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        }
                        isPressed = true;
                    }

                }
            }

        }
        #endregion
    }
}
