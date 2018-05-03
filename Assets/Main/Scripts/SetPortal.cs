using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPortal : MonoBehaviour
{
    private const int LEFT_MOUSE_BUTTON = 0;
    private const int RIGHT_MOUSE_BUTTON = 1;

    GameObject redPortal;
    GameObject bluePortal;

    public Sprite redPortalSprite;
    public Sprite bluePortalSprite;

    void Update()
    {
        GameObject walls = GameObject.Find("Environment");

        if (Input.GetMouseButtonDown(RIGHT_MOUSE_BUTTON) && IsPortalAbleToSet(walls, bluePortal))
        {
            if (redPortal == null)
            {
                redPortal = new GameObject("RedPortal");
                redPortal.AddComponent<SpriteRenderer>().sprite = redPortalSprite;
                redPortal.AddComponent<CapsuleCollider2D>().isTrigger = true;
            }

            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0.0f;

            redPortal.transform.position = spawnPosition;
        }

        if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON) && IsPortalAbleToSet(walls, redPortal))
        {
            if (bluePortal == null)
            {
                bluePortal = new GameObject("BluePortal");
                bluePortal.AddComponent<SpriteRenderer>().sprite = bluePortalSprite;
                bluePortal.AddComponent<CapsuleCollider2D>().isTrigger = true;
            }

            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0.0f;

            bluePortal.transform.position = spawnPosition;
        }
    }

    private bool IsPortalAbleToSet(GameObject walls, GameObject portal)
    {
        bool wallsCondition = true;
        bool portalCondition = true;

        const float OFFSET_X = 0.75f;
        const float OFFSET_Y = 1.15f;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mPositions = new List<Vector2>();
        mPositions.Add(new Vector2(mousePosition.x - OFFSET_X, mousePosition.y + OFFSET_Y));
        mPositions.Add(new Vector2(mousePosition.x + OFFSET_X, mousePosition.y + OFFSET_Y));
        mPositions.Add(new Vector2(mousePosition.x - OFFSET_X, mousePosition.y - OFFSET_Y));
        mPositions.Add(new Vector2(mousePosition.x + OFFSET_X, mousePosition.y - OFFSET_Y));
        mPositions.Add(mousePosition);

        var colliders = walls.GetComponentsInChildren<Collider2D>();
        foreach (var collider in colliders)
        {
            foreach (var mPos in mPositions)
            {
                if (collider.OverlapPoint(mPos))
                {
                    wallsCondition = false;
                    break;
                }
            }

            if (!wallsCondition) break;
        }

        if (portal != null)
        {
            portalCondition = !portal.GetComponent<Collider2D>().OverlapPoint(mousePosition);
        }

        return wallsCondition && portalCondition;
    }
}
