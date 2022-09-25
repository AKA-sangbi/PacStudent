using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cherry : MonoBehaviour
{
    public float timer = 0f;

    public GameObject cherryObj;

    // Start is called before the first frame update
    void Start()
    {
        cherryObj.SetActive(false);
    }


    void Update()
    {
        if (GameManager.Instance.OnStartGame == false)
        {
            timer += Time.deltaTime;
            if (timer >= 10f)
            {
                cherryObj.SetActive(true);
                timer = -10f;
            }

            if (timer >= 0 && timer <= 1f)
            {
                cherryObj.SetActive(false);
            }
        }
    }
}
