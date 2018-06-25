using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : GrabbableAndThrowableObject, IEnemy
{
    public AudioClip explosionSound;

    private GameObject player;
    private const int ATTACK_DISTANCE = 1;
    private const int DAMAGE = 80;
    private AudioSource audioSource;
    private bool exploded = false;

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
        audioSource.PlayOneShot(explosionSound);
        player.GetComponent<HitBevaviour>().HP -= DAMAGE;

        yield return new WaitWhile(() => audioSource.isPlaying);
        Destroy(transform.gameObject);
    }

    private void Update()
    {
        if (ShouldAttack())
        {
            Attack();
        }
    }

    public bool ShouldAttack()
    {
        return Vector2.Distance(player.transform.position, transform.position) < ATTACK_DISTANCE;
    }
}