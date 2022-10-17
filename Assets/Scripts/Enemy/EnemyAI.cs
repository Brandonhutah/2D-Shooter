using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Variables")]
    [Tooltip("The speed at which the enemy will move.")]
    public float moveSpeed = 10.0f;

    private SpriteRenderer enemySprite;
    private GameObject playerTarget;

    // Start is called before the first frame update
    void Start()
    {
        enemySprite = this.gameObject.GetComponent<SpriteRenderer>();
        playerTarget = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameIsOver)
        {
            return;
        }

        // update the enemies direction
        var player = new Vector2(playerTarget.transform.position.x, playerTarget.transform.position.y);

        if (player.x < this.gameObject.transform.position.x)
        {
            enemySprite.flipX = false;
        }
        else if (player.x > this.gameObject.transform.position.x)
        {
            enemySprite.flipX = true;
        }

        // setup a movement translation and move the enemy if needed
        int xMove = player.x > this.gameObject.transform.position.x ? 1 : -1;
        int yMove = player.y > this.gameObject.transform.position.y ? 1 : -1;
        Vector3 movement = new Vector3(xMove * moveSpeed * Time.deltaTime, yMove * moveSpeed * Time.deltaTime, 0);

        this.gameObject.transform.Translate(movement);
    }
}
