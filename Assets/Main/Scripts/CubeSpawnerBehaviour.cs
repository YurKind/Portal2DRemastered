using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class CubeSpawnerBehaviour : MonoBehaviour
{
    public GameObject cube;

    public void SpawnCube()
    {
        cube.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        cube.transform.position = transform.position;
    }
}