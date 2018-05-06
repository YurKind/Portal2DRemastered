using UnityEngine;

public class DoorBehaviour : MonoBehaviour {

    public bool isOpened;
    public Sprite openedDoor;
    public Sprite closedDoor;

    private SpriteRenderer doorRenderer;
    private GameObject door;

    private void Start ()
    {
        door = GameObject.Find("Door");
        doorRenderer = door.GetComponent<SpriteRenderer>() ?? door.AddComponent<SpriteRenderer>();
    }

    private void Update ()
    {
        doorRenderer.sprite = isOpened ? openedDoor : closedDoor;
	}
}
