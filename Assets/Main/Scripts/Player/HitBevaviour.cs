using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HitBevaviour : MonoBehaviour
{
    public int HP = 100;
    public float knockBackDistance = 0.05f;
    public Text health;
    
    void Update()
    {
        health.text = "Health: " + HP;
        if (HP < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void MakeHit(int damage, GameObject origin)
    {
        transform.position = origin.transform.position.x > transform.position.x
            ? new Vector3(transform.position.x - knockBackDistance, transform.position.y, transform.position.z)
            : new Vector3(transform.position.x + knockBackDistance, transform.position.y, transform.position.z);
        HP -= damage;
    }
}