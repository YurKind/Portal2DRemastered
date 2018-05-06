using System.CodeDom;
using UnityEngine;

public abstract class TurretBehaviourBase : MonoBehaviour, ITurretBehaviour
{
    protected static float TURRET_TOLERANCE = 0.1f;
    
    protected GameObject player;
    
    public void Start()
    {
        player = GameObject.Find("Player");
    }
    
    public abstract void Shoot();
    public abstract bool ShouldShoot();
}