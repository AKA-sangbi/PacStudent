using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    public static Ghost _instance;

    public float speed = 0.2f;

    private void Awake()
    {
        _instance = this;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet")
        {
            GameManager.Instance.FreezeEnemy(this.gameObject);
            GameManager.Instance.OnKillEnemy(this.gameObject);
            GameManager.Instance.currentScore += 300;
        }

        if (collision.gameObject.name == "Pacman")
        {
            GameObject.FindGameObjectWithTag("HealthValue").GetComponent<Text>().text = GameManager.Instance.HealthValue.ToString();
            if (GameManager.Instance.isSuperPacman == false && GameManager.Instance.HealthValue != 0)//����Ҳ��ǳ�����Ҳ���Ѫ����Ϊ0ʱ
            {
                GameManager.Instance.HealthValue -= 1;
                GameObject.Find("Dead").GetComponent<AudioSource>().Play();
                GameObject.Find("HealthValue").GetComponent<Text>().text = GameManager.Instance.HealthValue.ToString();
                Vector3 P = GameManager.Instance.ReBoomPosition.transform.position;
                GameManager.Instance.Player.transform.position = P;
                PacStudent.Instance.playerCurrentInput = null;
                PacStudent.Instance.playerLastInput = null;
                PacStudent.Instance._currentInput = null;
            }
            if (GameManager.Instance.isSuperPacman)//����ҳ�Ϊ�������֮��
            {
                GameManager.Instance.FreezeEnemy(this.gameObject);
                GameManager.Instance.OnKillEnemy(this.gameObject);
                GameManager.Instance.currentScore += 300;
                GameObject.Find("Eat").GetComponent<AudioSource>().Play();
            }
            if (GameManager.Instance.isSuperPacman == false && GameManager.Instance.HealthValue == 0)//����Ҳ��ǳ�����Ҳ���Ѫ��Ϊ0ʱ
            {
                GameManager.Instance.HealthValue -= 1;
                Vector3 P = GameManager.Instance.ReBoomPosition.transform.position;
                GameManager.Instance.Player.transform.position = P;
                collision.gameObject.SetActive(false);
                GameManager.Instance.gamePanel.SetActive(false);
                Instantiate(GameManager.Instance.gameoverPrefab);
                Invoke("Restart", 3f);
            }
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
