using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private List<GameObject> enemies;
    
    private void Awake()
    {
        enemies = new List<GameObject>();
        var turrets = FindObjectsOfType(typeof(StaticTurret)) as StaticTurret[];
        var spheres = FindObjectsOfType(typeof(Sphere)) as Sphere[];
        foreach (var turret in turrets)
        {
            enemies.Add(turret.gameObject);
        }

        foreach (var sphere in spheres)
        {
            enemies.Add(sphere.gameObject);
        }
    }

    private void Update()
    {
        foreach (var enemy in enemies.ToList())
        {
            if (enemy == null) //Если у gameObject был вызван Destroy()
            {
                enemies.Remove(enemy);
                continue;
            }
            
            if (enemy.GetComponent<IEnemy>().ShouldAttack())
            {
                enemy.GetComponent<IEnemy>().Attack();
            }
        }
    }
}