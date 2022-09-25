using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColliderTriggerDetector : MonoBehaviour
{

    private PacStudent Pac;

    private void Awake()
    {
        Pac = GameObject.FindGameObjectWithTag("Player").GetComponent<PacStudent>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="left")
        {
            Pac.canMoveLeft = false;
        }

        if (collision.tag == "right")
        {
            Pac.canMoveRight = false;
        }

        if (collision.tag == "Back")
        {
            Pac.canMoveBack = false;
        }

        if (collision.tag == "Front")
        {
            Pac.canMoveFront = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "left")
        {
            Pac.canMoveLeft = true;
        }

        if (collision.tag == "right")
        {
            Pac.canMoveRight = true;
        }

        if (collision.tag == "Back")
        {
            Pac.canMoveBack = true;
        }

        if (collision.tag == "Front")
        {
            Pac.canMoveFront = true;
        }
    }
}
