using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameTimeText : MonoBehaviour
{
    private int hour;
    private int minute;
    private int second;
    private int millisecond;
    private int a = 0;

    public Text text_timeSpend;
    public Text test_chenggong;


    private float gameTime = 0.0f;

    // Update is called once per frame                                                                                              
    void Update()
    {
        if (a == 0)
        {
            gameTime += Time.deltaTime;
            int intTime = (int)gameTime;
            hour = intTime / 3600;
            minute = intTime % 3600 / 60;
            second = intTime % 3600 % 60;
            text_timeSpend.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second, millisecond);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        a = 1;
        test_chenggong.enabled = true;
    }
}
