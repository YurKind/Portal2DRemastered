using UnityEngine;

public class PushTheButton : MonoBehaviour
{
    public Sprite buttonOn;
    public Sprite buttonOff;

    public GameObject player;
    public GameObject cube;
    public GameObject door;

    private SpriteRenderer buttonRenderer;
    private Collider2D playerCollider;
    private Collider2D cubeCollider;
    private Collider2D buttonCollider;

    private void Start()
    {
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
        var doorBehaviour = door.GetComponent<DoorBehaviour>();
        if (buttonCollider.IsTouching(playerCollider) || buttonCollider.IsTouching(cubeCollider))
        {
            buttonRenderer.sprite = buttonOn;
            if (!doorBehaviour.isOpened) doorBehaviour.OpenDoor();
        }
        else
        {
            buttonRenderer.sprite = buttonOff;
            if (doorBehaviour.isOpened) doorBehaviour.CloseDoor();
        }
    }
}
