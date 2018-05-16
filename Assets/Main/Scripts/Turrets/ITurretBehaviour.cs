public interface ITurretBehaviour : IGrabbableAndThrowable
{
    bool IsShooting();
    
    void Shoot();

    bool ShouldShoot();
}