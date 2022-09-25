using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CherryController : MonoBehaviour
{
    public float timer = 0f;

    public GameObject Charry;

    // Start is called before the first frame update
    void Start()
    {
        Charry.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.OnStartGame==false)
        {
            timer += Time.deltaTime;
            if (timer >= 10f)
            {
                Charry.SetActive(true);
                timer = -10f;
            }

            if(timer>=0&&timer<=1f)
            {
                Charry.SetActive(false);
            }
        }
    }
}
