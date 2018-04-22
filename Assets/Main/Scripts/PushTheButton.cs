using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTheButton : MonoBehaviour
{
    public Sprite buttonOn;
    public Sprite buttonOff;
    public Sprite doorOpen;
    public Sprite doorClosed;

    SpriteRenderer buttonRenderer = null;
    SpriteRenderer doorRenderer = null;

    GameObject player = null;
    Collider2D playerCollider = null;
    Collider2D cubeCollider = null;
    GameObject door = null;
    GameObject cube = null;

    void Start()
    {
        player = GameObject.Find("Player");
        cube = GameObject.Find("Cube");
        door = GameObject.Find("Door");
        playerCollider = player.GetComponent<Collider2D>();
        cubeCollider = cube.GetComponent<Collider2D>();

        if (gameObject.GetComponent<SpriteRenderer>() == null)
        {
            buttonRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        else
        {
            buttonRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        if (door.GetComponent<SpriteRenderer>() == null)
        {
            doorRenderer = door.AddComponent<SpriteRenderer>();
        }
        else
        {
            doorRenderer = door.GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        var buttonCollider = transform.GetComponent<PolygonCollider2D>();
        if (buttonCollider.IsTouching(playerCollider) || buttonCollider.IsTouching(cubeCollider))
        {
            buttonRenderer.sprite = buttonOn;
            doorRenderer.sprite = doorOpen;
        }
        else
        {
            buttonRenderer.sprite = buttonOff;
            doorRenderer.sprite = doorClosed;
        }
    }
}
