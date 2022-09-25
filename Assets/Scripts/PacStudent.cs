using UnityEngine;
using UnityEngine.UI;

public class PacStudent : MonoBehaviour
{
    public static PacStudent Instance;

    public float moveSpeed = 0.05f;
    public string playerCurrentInput = null;//玩家当前输入
    public string playerLastInput = null;//上次输入按键
    public string _currentInput = null;//当前输入按键
    public ParticleSystem effect;
    public bool IsGun = false;
    public bool IsBullet = false;
    public GameObject BulletPerfab;
    public GameObject CurrentBullet;

    public bool IsShow = false;

    public float Guntimer = 0f;

    public float ShowTmer = 0f;

    public Transform FirePosition;

    public Quaternion Rotation;

    public bool canMoveLeft = true;
    public bool canMoveRight = true;
    public bool canMoveBack = true;
    public bool canMoveFront = true;

    public bool isFaceLeft = false;
    public bool isFaceRight = false;
    public bool isFaceDown = false;
    public bool isFaceUp = false;


    private Animator animator;



    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (isFaceDown)
        {
            animator.SetBool("Down", true);
            animator.SetBool("Up", false);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
            isFaceDown = false;
        }

        if (isFaceUp)
        {
            animator.SetBool("Down", false);
            animator.SetBool("Up", true);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
            isFaceUp = false;
        }

        if (isFaceLeft)
        {
            animator.SetBool("Down", false);
            animator.SetBool("Up", false);
            animator.SetBool("Right", false);
            animator.SetBool("Left", true);
            isFaceLeft = false;
        }

        if (isFaceRight)
        {
            animator.SetBool("Down", false);
            animator.SetBool("Up", false);
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);
            isFaceRight = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (canMoveFront)
            {
                if (playerCurrentInput == null)
                {
                    playerCurrentInput = "D";
                    playerLastInput = playerCurrentInput;
                    _currentInput = playerLastInput;
                }

                if (playerCurrentInput != null)
                {
                    playerCurrentInput = "D";
                    if (playerCurrentInput != playerLastInput)
                    {
                        playerLastInput = playerCurrentInput;
                        _currentInput = playerLastInput;
                    }
                }
            }

            if (canMoveFront == false)
            {
                playerCurrentInput = "D";
                if (playerCurrentInput != playerLastInput)
                {
                    playerLastInput = playerCurrentInput;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (canMoveBack)
            {
                if (playerCurrentInput == null)
                {
                    playerCurrentInput = "A";
                    playerLastInput = playerCurrentInput;
                    _currentInput = playerLastInput;
                }

                if (playerCurrentInput != null)
                {
                    playerCurrentInput = "A";
                    if (playerCurrentInput != playerLastInput)
                    {
                        playerLastInput = playerCurrentInput;
                        _currentInput = playerLastInput;
                    }
                }
            }

            if (canMoveBack == false)
            {
                playerCurrentInput = "A";
                if (playerCurrentInput != playerLastInput)
                {
                    playerLastInput = playerCurrentInput;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (canMoveLeft)
            {
                if (playerCurrentInput == null)
                {
                    playerCurrentInput = "W";
                    playerLastInput = playerCurrentInput;
                    _currentInput = playerLastInput;
                }

                if (playerCurrentInput != null)
                {
                    playerCurrentInput = "W";
                    if (playerCurrentInput != playerLastInput)
                    {
                        playerLastInput = playerCurrentInput;
                        _currentInput = playerLastInput;
                    }
                }
            }

            if (canMoveLeft == false)
            {
                playerCurrentInput = "W";
                if (playerCurrentInput != playerLastInput)
                {
                    playerLastInput = playerCurrentInput;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (canMoveRight)
            {
                if (playerCurrentInput == null)
                {
                    playerCurrentInput = "S";
                    playerLastInput = playerCurrentInput;
                    _currentInput = playerLastInput;
                }

                if (playerCurrentInput != null)
                {
                    playerCurrentInput = "S";
                    if (playerCurrentInput != playerLastInput)
                    {
                        playerLastInput = playerCurrentInput;
                        _currentInput = playerLastInput;
                    }

                    if (playerCurrentInput == playerLastInput)
                    {
                        playerLastInput = playerCurrentInput;
                        _currentInput = playerLastInput;
                    }
                }
            }

            if (canMoveRight == false)
            {
                playerCurrentInput = "S";
                if (playerCurrentInput != playerLastInput)
                {
                    playerLastInput = playerCurrentInput;
                }
            }
        }




        if (playerLastInput == "S" && canMoveRight)
        {
            _currentInput = playerLastInput;
            isFaceDown = true;
        }

        if (playerLastInput == "W" && canMoveLeft)
        {
            _currentInput = playerLastInput;
            isFaceUp = true;
        }

        if (playerLastInput == "A" && canMoveBack)
        {
            _currentInput = playerLastInput;
            isFaceLeft = true;
        }

        if (playerLastInput == "D" && canMoveFront)
        {
            _currentInput = playerLastInput;
            isFaceRight = true;
        }

        if (_currentInput != null)
        {
            GetComponent<AudioSource>().Play();
        }

        if (_currentInput == "D" && canMoveFront)
        {
            isFaceRight = true;
            transform.Translate(new Vector3(moveSpeed, 0, 0));
        }

        if (_currentInput == "A" && canMoveBack)
        {
            isFaceLeft = true;
            transform.Translate(new Vector3(-moveSpeed, 0, 0));
        }

        if (_currentInput == "W" && canMoveLeft)
        {
            isFaceUp = true;
            transform.Translate(new Vector3(0, moveSpeed, 0));
        }

        if (_currentInput == "S" && canMoveRight)
        {
            isFaceDown = true;
            transform.Translate(new Vector3(0, -moveSpeed, 0));
        }

        if (isFaceUp || isFaceDown || isFaceLeft || isFaceRight)
        {
            effect.Play();
        }

        if (IsGun)
        {
            GameManager.Instance.HealthValue += 1;
            GameObject.Find("HealthValue").GetComponent<Text>().text = GameManager.Instance.HealthValue.ToString();
            IsGun = false;
        }


        if (IsShow)
        {
            moveSpeed = 0.1f;
            ShowTmer += Time.deltaTime;
            if (ShowTmer >= 5f)
            {
                moveSpeed = 0.05f;
                ShowTmer = 0f;
                IsShow = false;
            }
        }
    }
}
