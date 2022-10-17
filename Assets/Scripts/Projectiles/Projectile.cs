using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("The distance this projectile will move each second.")]
    public float projectileSpeed = 3;

    [Tooltip("The distance this projectile will move before being destroyed.")]
    public float projectileDistance = 10;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(initialPosition, this.transform.position) > projectileDistance)
        {
            Destroy(this.gameObject);
        } else
        {
            transform.position = transform.position + transform.up * projectileSpeed * Time.deltaTime;
        }
    }
}
