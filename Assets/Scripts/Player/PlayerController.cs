using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [Tooltip("The speed at which the player will move.")]
    public float moveSpeed = 10.0f;
    [Header("Movement Variables")]
    [Tooltip("The animation to play when player is not moving.")]
    public RuntimeAnimatorController PlayerIdleAnimation;
    [Tooltip("The animation to play when player is moving.")]
    public RuntimeAnimatorController PlayerWalkAnimation;

    private Animator playerAnimator;
    private SpriteRenderer playerSprite;

    private enum MoveState { Idle, Walking };
    private MoveState playerMoveState = MoveState.Idle;
    private enum DirectionState { Left, Right };
    private DirectionState playerDirection = DirectionState.Right;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = this.gameObject.GetComponent<Animator>();
        playerSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameIsOver)
        {
            HandleInput();
        }
    }

    /// <summary>
    /// Description:
    /// Checks inputs and takes action on them if needed
    /// Inputs:
    /// None
    /// Returns:
    /// void
    /// </summary>
    private void HandleInput()
    {
        UpdateCharacter();
    }

    /// <summary>
    /// Description:
    /// Updates player characters position, animation, and orientation
    /// Inputs:
    /// None
    /// Returns:
    /// void
    /// </summary>
    private void UpdateCharacter()
    {
        // setup a movement translation and move the play if needed
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0);

        this.gameObject.transform.Translate(movement);
        
        // update the players animation
        if (movement.magnitude > 0 && playerMoveState == MoveState.Idle)
        {
            playerMoveState = MoveState.Walking;
            playerAnimator.runtimeAnimatorController = PlayerWalkAnimation;
        }
        else if (movement.magnitude == 0 && playerMoveState == MoveState.Walking)
        {
            playerMoveState = MoveState.Idle;
            playerAnimator.runtimeAnimatorController = PlayerIdleAnimation;
        }

        // update the players direction
        var playerScreenPoint = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        var mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (mouse.x < playerScreenPoint.x && playerDirection == DirectionState.Right)
        {
            playerDirection = DirectionState.Left;
            playerSprite.flipX = true;
        }
        else if (mouse.x > playerScreenPoint.x && playerDirection == DirectionState.Left)
        {
            playerDirection = DirectionState.Right;
            playerSprite.flipX = false;
        }
    }
}
