using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool isOpened;
    public Sprite openedDoor;
    public Sprite closedDoor;
    public AudioClip openSound;
    public AudioClip closeSound;

    private SpriteRenderer doorRenderer;
    private GameObject door;
    private GameObject player;
    private AudioSource audioSource;

    private const int FINAL_LEVEL = 3;

    public void OpenDoor()
    {
        isOpened = true;
        audioSource.PlayOneShot(openSound);
    }

    public void CloseDoor()
    {
        isOpened = false;
        audioSource.PlayOneShot(closeSound);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        door = GameObject.Find("Door");
        doorRenderer = door.GetComponent<SpriteRenderer>() ?? door.AddComponent<SpriteRenderer>();
    }

    private void Update()
    {
        doorRenderer.sprite = isOpened ? openedDoor : closedDoor;
        if (player.GetComponent<Rigidbody2D>().IsTouching(door.GetComponent<Collider2D>()) && isOpened)
        {
            if (Input.GetKey(KeyCode.W))
            {
                var currentLevel =
                    int.Parse(SceneManager.GetActiveScene().name.Split(new[] {"Level"}, StringSplitOptions.None)[1]);

                if (currentLevel == FINAL_LEVEL)
                {
                    SceneManager.LoadScene("TheEnd");
                    File.WriteAllText("save.txt", "TheEnd");
                    return;
                }
                
                File.WriteAllText("save.txt", string.Format("Level{0}", currentLevel + 1));

                SceneManager.LoadScene(string.Format("Level{0}", currentLevel + 1));
            }
        }
    }
}