using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("Team Settings")]
    [Tooltip("The team associated with this damage")]
    public int teamId = 0;

    [Header("Damage Settings")]
    [Tooltip("How much damage to deal")]
    public int damageAmount = 1;
    [Tooltip("Prefab to spawn after doing damage")]
    public GameObject hitEffect = null;
    [Tooltip("Whether or not to destroy the attached game object after dealing damage")]
    public bool destroyAfterDamage = true;
    [Tooltip("The score value for defeating enemies")]
    public int scoreValue = 5;

    public AudioClip enemyDefeatedSound;
    public AudioClip damageTakenSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called when a Collider2D hits another Collider2D (non-triggers)
    /// Inputs:
    /// Collision2D collision
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="collision">The Collision2D that set of the function call</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DealDamage(collision.gameObject);
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called when a Collider2D hits another Collider2D (non-triggers)
    /// Inputs:
    /// Collision2D collision
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="collision">The Collision2D that set of the function call</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DealDamage(collision.gameObject);
    }

    /// <summary>
    /// Description:
    /// This function deals damage to a health component if the collided 
    /// with gameobject has a health component attached AND it is on a different team.
    /// Inputs:
    /// GameObject collisionGameObject
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="collisionGameObject">The game object that has been collided with</param>
    private void DealDamage(GameObject collisionGameObject)
    {
        Health collidedHealth = collisionGameObject.GetComponent<Health>();
        if (collidedHealth != null)
        {
            if (collidedHealth.teamId != this.teamId)
            {
                collidedHealth.TakeDamage(damageAmount);
                if (hitEffect != null)
                {
                    Instantiate(hitEffect, transform.position, transform.rotation, null);
                }
            }
        }

        if (gameObject.tag == "Projectile" && collisionGameObject.tag == "Enemy")
        {
            if (GameManager.instance != null && !GameManager.instance.gameIsOver)
            {
                GameManager.AddScore(scoreValue);
            }
        }

        if ((gameObject.tag == "Projectile" && collisionGameObject.tag != "Player") || (gameObject.tag == "Enemy" && collisionGameObject.tag == "Player"))
        {
            Destroy(this.gameObject);
        }
    }
}