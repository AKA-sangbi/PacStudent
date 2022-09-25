using UnityEngine;

public class DotObject : MonoBehaviour
{
    public bool isBig = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pacman")
        {
            if (isBig)
            {
                GameManager.Instance.EatDot(gameObject);
                GameManager.Instance.OnEatSuperPacdot();
                Destroy(gameObject);
            }
            else
            {
                GameManager.Instance.EatDot(gameObject);
                GameManager.Instance.dotNumber += 1;
                Destroy(gameObject);
            }

        }
    }
}
