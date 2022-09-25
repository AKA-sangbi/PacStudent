using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    private float timer = 0f;

    public bool IsIn = false;

    public GameObject[] Weapons = new GameObject[14];
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in Weapons)
        {
            item.SetActive(false);
            timer = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>=10f&&IsIn==false)
        {
            Weapons[Random.Range(0, 13)].SetActive(true);
            IsIn = true;
        }

        if(timer>=15f&&IsIn)
        {
           foreach(GameObject item in Weapons)
           {
                item.SetActive(false);
                timer = 0f;
                IsIn = false;
           }
        }
    }
}
