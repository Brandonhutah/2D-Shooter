using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // The script that manages all others
    public static GameManager instance = null;

    [Tooltip("The UI text that displays the players health")]
    public Text healthText;
    [Tooltip("The UI text that displays the players score")]
    public Text[] scoreText;

    [Tooltip("The player gameobject")]
    public GameObject player = null;

    public GameObject gameOverScreen = null;
    public GameObject gameUI = null;

    // The current player score in the game
    [Tooltip("The player's score")]
    [SerializeField] private int gameManagerScore = 0;

    // The current player health in the game
    [Tooltip("The player's score")]
    [SerializeField] private int gameManagerHealth = 10;

    private AudioSource audioSource;

    public AudioClip scoreIncrementedSound;
    public AudioClip healthDecrementedSound;

    // Static getter/setter for player score (for convenience)
    public static int score
    {
        get
        {
            return instance.gameManagerScore;
        }
        set
        {
            instance.gameManagerScore = value;
        }
    }

    // Static getter/setter for player health (for convenience)
    public static int health
    {
        get
        {
            return instance.gameManagerHealth;
        }
        set
        {
            instance.gameManagerHealth = value;
        }
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called when the script is loaded, called before start
    /// 
    /// When this component is first added or activated, setup the global reference
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private void Start()
    {
        this.audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    /// <summary>
    /// Description:
    /// Adds a number to the player's score stored in the gameManager
    /// Input: 
    /// int scoreAmount
    /// Returns: 
    /// void (no return)
    /// </summary>
    /// <param name="scoreAmount">The amount to add to the score</param>
    public static void AddScore(int scoreAmount)
    {
        instance.audioSource.clip = instance.scoreIncrementedSound;
        instance.audioSource.Play();
        score += scoreAmount;
        UpdateUIElements();
    }

    /// <summary>
    /// Description:
    /// Adds a number to the player's health stored in the gameManager
    /// Input: 
    /// int healthAmount
    /// Returns: 
    /// void (no return)
    /// </summary>
    /// <param name="scoreAmount">The amount to add to the health</param>
    public static void AddHealth(int healthAmount)
    {
        instance.audioSource.clip = instance.healthDecrementedSound;
        instance.audioSource.Play();
        health += healthAmount;
        UpdateUIElements();

        if (health == 0)
        {
            instance.GameOver();
        }
    }

    /// <summary>
    /// Description:
    /// Sends out a message to UI elements to update
    /// Input: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    public static void UpdateUIElements()
    {
        instance.healthText.text = health.ToString();

        foreach (Text text in instance.scoreText)
        {
            text.text = score.ToString();
        }
    }

    // Whether or not the game is over
    [HideInInspector]
    public bool gameIsOver = false;

    /// <summary>
    /// Description:
    /// Displays game over screen
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    public void GameOver()
    {
        instance.gameUI.SetActive(false);
        instance.gameOverScreen.SetActive(true);
        gameIsOver = true;
    }
}
