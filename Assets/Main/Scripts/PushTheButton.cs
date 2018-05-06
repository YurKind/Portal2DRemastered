using UnityEngine;

public class PushTheButton : MonoBehaviour
{
    public Sprite buttonOn;
    public Sprite buttonOff;

    private SpriteRenderer buttonRenderer;

    private GameObject player;
    private GameObject cube;
    private GameObject door;

    private Collider2D playerCollider;
    private Collider2D cubeCollider;
    private Collider2D buttonCollider;

    private void Start()
    {
        player = GameObject.Find("Player");
        cube = GameObject.Find("Cube");
        door = GameObject.Find("Door");

        playerCollider = player.GetComponent<Collider2D>();
        cubeCollider = cube.GetComponent<Collider2D>();
        buttonCollider = transform.GetComponent<Collider2D>();

        if (gameObject.GetComponent<SpriteRenderer>() == null)
        {
            buttonRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        else
        {
            buttonRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (buttonCollider.IsTouching(playerCollider) || buttonCollider.IsTouching(cubeCollider))
        {
            buttonRenderer.sprite = buttonOn;
            door.GetComponent<DoorBehaviour>().isOpened = true;
        }
        else
        {
            buttonRenderer.sprite = buttonOff;
            door.GetComponent<DoorBehaviour>().isOpened = false;
        }
    }
}
