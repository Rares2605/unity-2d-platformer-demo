using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Moveee : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float movementSpeed;
    private bool facingRight = true;
    private bool isGrounded;
    public float coinCounter;
    public GameObject coin;
    public float health;
    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;
    private bool gameOver = false;
    private bool takeDamageAllowed = true;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI coinFinish;
    public GameObject GameOver_Panel;
    public GameObject GameWin_Panel;

    public AudioClip coinPickUpSound;
    public AudioClip takeDamageSound;
    AudioSource soundController;
    Rigidbody2D rb;
    SpriteRenderer mySprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 3;
        gameOver = false;
        GameOver_Panel.SetActive(false);
        coinText.text = "Coins: 0";
        healthText.text = "HP: 3";

        rb = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        soundController = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector2(horizontalInput * movementSpeed, rb.linearVelocity.y);
        if (Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, movementSpeed);

        }
        FlipEyesLeft();
        FlipEyesRight();

        if (health == 2)
        { Destroy(hp1);
        }
        if (health == 1)
            Destroy(hp2);
        if (health == 0)
        {
            Destroy(hp3);
            print("game over");
            GameOver_Panel.SetActive(true);
            gameOver = true;


        }


    }
    void Update()
        
    {
        if (gameOver == true)
        { Time.timeScale = 0; }
        if (Input.GetKeyDown(KeyCode.R) && gameOver == true)
        {
            Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
  

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
      
        if (other.gameObject.CompareTag("Enemy") && takeDamageAllowed == true)
        {
            health--;
            takeDamageAllowed = false;
            StartCoroutine(DamageCooldown());
            soundController.clip = takeDamageSound;
            soundController.Play();
            healthText.text = "HP: " + health.ToString();
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coinCounter++;
            soundController.clip = coinPickUpSound;
            soundController.Play();
            coinText.text = "Coins: " + coinCounter.ToString();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Finish2"))
        {
            coinFinish.text = coinFinish.text + coinText.text + "/10";
            GameWin_Panel.SetActive(true);
            gameOver = true;
        }
    }

    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(1);
        takeDamageAllowed = true;
    }

    void FlipEyesLeft()
    {
        if (facingRight == true && horizontalInput < 0f)
        {
            facingRight = false;
            mySprite.flipX = true;
        }
    }
    void FlipEyesRight()
    {
        if (facingRight == false && horizontalInput > 0f)
        {
            facingRight = true;
            mySprite.flipX = false;
        }
    }
}

