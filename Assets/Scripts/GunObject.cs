using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunObject : MonoBehaviour
{
    public string WeaponName;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (WeaponName == "Gun")
            {
                PacStudent.Instance.IsGun = true;
                gameObject.SetActive(false);
            }

            if (WeaponName == "Show")
            {
                PacStudent.Instance.IsShow = true;
                gameObject.SetActive(false);
            }
        }
    }
}
