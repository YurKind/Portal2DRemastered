using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cube;

    public void SpawnCube()
    {
        cube.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        cube.transform.position = transform.position;
    }
}