using UnityEngine;

public class StandButton : MonoBehaviour, ICanBeUsed
{
    public GameObject cubeSpawner;
    public AudioClip touchSound;
    
    private GameObject player;
    private int distance = 1;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
//        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(player.transform.position, transform.position) < distance)
//        {
//            Use();
//        }
    }

    public void Use()
    {
        audioSource.PlayOneShot(touchSound);
        cubeSpawner.GetComponent<CubeSpawner>().SpawnCube();
    }
}