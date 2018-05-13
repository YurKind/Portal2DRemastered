using UnityEngine;
using UnityEngine.SceneManagement;

public class HitBevaviour : MonoBehaviour
{
    public int HP = 100;
    public float knockBackDistance = 0.1f;

    void Update()
    {
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