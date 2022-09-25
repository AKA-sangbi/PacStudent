using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunOrShow : MonoBehaviour
{
    public string WeaponName;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (WeaponName == "Gun")
            {
                PacStudent.Instance.gunBuffer = true;
                gameObject.SetActive(false);
            }

            if (WeaponName == "Show")
            {
                PacStudent.Instance.showBuffer = true;
                gameObject.SetActive(false);
            }
        }
    }
}
