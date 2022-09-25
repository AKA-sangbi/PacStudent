using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DontDestroy : MonoBehaviour
{
    public int MaxScore;

    private static DontDestroy Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            if (Instance != this)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    public static DontDestroy GetInstance()
    {
        return Instance;
    }

    private void Update()
    {
        if (GameObject.Find("Value") != null)
        {
            GameObject.Find("Value").GetComponent<Text>().text = MaxScore.ToString();
        }

    }
}
