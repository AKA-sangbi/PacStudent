using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public string dirString;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag=="Player")
        {
           if(dirString=="zuo")
            {
                GameManager.Instance.Directionzuo = true;
            }

            if (dirString == "you")
            {
                GameManager.Instance.Directionyou = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (dirString == "zuo")
            {
                GameManager.Instance.Directionzuo = false;
            }

            if (dirString == "you")
            {
                GameManager.Instance.Directionyou = false;
            }
        }
    }
}
