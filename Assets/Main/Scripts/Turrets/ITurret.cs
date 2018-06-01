public interface ITurret : IGrabbableAndThrowable
{
    bool IsShooting();
    
    void Shoot();

    bool ShouldShoot();
}