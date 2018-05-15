using System;
using UnityEngine;

public class StaticTurretBehaviour : GrabbableObject, ITurretBehaviour
{
    public GameObject startShootingPoint;
    public GameObject endShootingPoint;
    public GameObject laser;
    public Sprite laserSprite;
    public AudioClip shootingSound;

    private GameObject player;
    private bool isShooting;
    private const float allowedRotation = 0.5f;
    private const int turretDamage = 1;
    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        if (ShouldShoot())
        {
            isShooting = true;
            Shoot();
        }
        else
        {
            isShooting = false;
        }

        laser.GetComponent<SpriteRenderer>().sprite = IsGrounded() ? laserSprite : null;

        if (grabbed)
        {
            transform.position = hold.transform.position;
        }
    }

    public bool IsShooting()
    {
        return isShooting;
    }

    public void Shoot()
    {
        player.GetComponent<HitBevaviour>().MakeHit(turretDamage, gameObject);
        if (!audioSource.isPlaying) audioSource.PlayOneShot(shootingSound);
    }

    public bool ShouldShoot()
    {
        var startPosition = startShootingPoint.transform.position;
        var endPosition = endShootingPoint.transform.position;

        bool playerInSight = Physics2D.Raycast(startPosition, endPosition - startPosition,
                                 Vector2.Distance(startPosition, endPosition)).collider ==
                             player.GetComponent<PolygonCollider2D>();
        return playerInSight && IsGrounded();
    }

    private bool IsGrounded()
    {
        return Math.Abs(transform.rotation.z) < allowedRotation;
    }
}