using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cheery")
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.score += 100;
        }
    }
}
