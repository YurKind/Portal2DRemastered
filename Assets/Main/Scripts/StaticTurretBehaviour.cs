using UnityEngine;

public class StaticTurretBehaviour : MonoBehaviour, ITurretBehaviour
{
    public GameObject startShootingPoint;
    public GameObject endShootingPoint;
    public int turretDamage = 1;
    private GameObject player;
    private bool isShooting;

    public void Start()
    {
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
    }

    public bool IsShooting()
    {
        return isShooting;
    }

    public void Shoot()
    {
        player.GetComponent<HitBevaviour>().MakeHit(turretDamage, gameObject);
    }

    public bool ShouldShoot()
    {
        var startPosition = startShootingPoint.transform.position;
        var endPosition = endShootingPoint.transform.position;
        var a = Physics2D
            .Raycast(startPosition, endPosition - startPosition, Vector2.Distance(startPosition, endPosition)).collider;

        return a == player.GetComponent<PolygonCollider2D>();
    }
}