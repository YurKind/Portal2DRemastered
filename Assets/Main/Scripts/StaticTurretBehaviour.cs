using System;

public class StaticTurretBehaviour : TurretBehaviourBase
{
    public void Update()
    {
        if (ShouldShoot())
        {
            Shoot();
        }
    }
    
    public override void Shoot()
    {
        
    }

    public override bool ShouldShoot()
    {
        return Math.Abs(player.transform.position.y - transform.position.y) < TURRET_TOLERANCE;
    }
}