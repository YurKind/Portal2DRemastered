using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandButtonBehaviour : MonoBehaviour
{
    public GameObject cubeSpawner;
    private GameObject player;
    private int distance = 1;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(player.transform.position, transform.position) < distance)
        {
            cubeSpawner.GetComponent<CubeSpawnerBehaviour>().SpawnCube();
        }
    }
}