using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Sphere : GrabbableAndThrowableObject, IEnemy
{
    public AudioClip explosionSound;
    public Sprite explosionSprite;

    private GameObject player;
    private AudioSource audioSource;
    private bool exploded;
    private const float ATTACK_DISTANCE = 1.5f;
    private const float DETECTION_DISTANCE = 3f;
    private const int DAMAGE = 80;
    private const float ROTATION_VELOCITY = 20;
    private const float MAX_ROTATION_VELOCITY = 800;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    public void Attack()
    {
        if (!exploded)
        {
            StartCoroutine(Expload());
        }
    }

    private IEnumerator Expload()
    {
        exploded = true;

        GetComponent<SpriteRenderer>().sprite = explosionSprite;
        GetComponent<Collider2D>().isTrigger = true;
        var rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        audioSource.PlayOneShot(explosionSound);
        player.GetComponent<HitBevaviour>().HP -= DAMAGE;

        yield return new WaitWhile(() => audioSource.isPlaying);
        Destroy(transform.gameObject);
    }

    private void Update()
    {
        if (ShouldFollow())
        {
            var currentAngularVelocity = transform.GetComponent<Rigidbody2D>().angularVelocity;
            if (currentAngularVelocity < MAX_ROTATION_VELOCITY)
            {
                transform.GetComponent<Rigidbody2D>().angularVelocity =
                    transform.position.x - player.transform.position.x > 0
                        ? currentAngularVelocity + ROTATION_VELOCITY
                        : currentAngularVelocity - ROTATION_VELOCITY;
            }
        }
    }

    public bool ShouldAttack()
    {
        return Vector2.Distance(player.transform.position, transform.position) < ATTACK_DISTANCE;
    }

    private bool ShouldFollow()
    {
        return Math.Abs(transform.position.y - player.transform.position.y) < DETECTION_DISTANCE;
    }
}