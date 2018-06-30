using System;
using System.Linq;
using UnityEngine;

public class StaticTurret : GrabbableAndThrowableObject, ITurret
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
        base.Update();

        isShooting = ShouldAttack();

        laser.GetComponent<SpriteRenderer>().sprite = IsGrounded() ? laserSprite : null;
    }

    public bool IsShooting()
    {
        return isShooting;
    }

    public void Attack()
    {
        player.GetComponent<HitBevaviour>().MakeHit(turretDamage, gameObject);
        if (!audioSource.isPlaying) audioSource.PlayOneShot(shootingSound);
    }

    public bool ShouldAttack()
    {
        if (!player.GetComponent<Platformer2DUserControl>().isPaused)
        {
            var startPosition = startShootingPoint.transform.position;
            var endPosition = endShootingPoint.transform.position;

            var playerCollider = player.GetComponent<PolygonCollider2D>();
            var collider2Ds = Physics2D.RaycastAll(startPosition, endPosition - startPosition,
                Vector2.Distance(startPosition, endPosition)).Select(hit => hit.collider).ToList();
            var rayCastHits = collider2Ds;
            var firstWallOrBulletProof =
                rayCastHits.FirstOrDefault(col => col.CompareTag("BulletProof") || col.CompareTag("Wall"));

            var playerInSight = firstWallOrBulletProof != null && rayCastHits.IndexOf(playerCollider) != -1 &&
                                rayCastHits.IndexOf(playerCollider) < rayCastHits.IndexOf(firstWallOrBulletProof) ||
                                firstWallOrBulletProof == null && rayCastHits.IndexOf(playerCollider) != -1;

            return playerInSight && IsGrounded();
        }

        return false;
    }

    private bool IsGrounded()
    {
        return Math.Abs(transform.rotation.z) < allowedRotation;
    }
}