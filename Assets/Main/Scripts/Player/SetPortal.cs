using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

public class SetPortal : MonoBehaviour
{
    private enum Portal
    {
        Blue,
        Red
    }

    private const int LEFT_MOUSE_BUTTON = 0;
    private const int RIGHT_MOUSE_BUTTON = 1;

    private GameObject redPortal;
    private GameObject bluePortal;

    private AudioSource audioSource;

    private readonly Func<RaycastHit2D, bool> wallsAndRedPortalPredicate =
        hit => hit.transform.CompareTag("Wall") || hit.transform.CompareTag("RedPortal");

    private readonly Func<RaycastHit2D, bool> wallsAndBluePortalPredicate =
        hit => hit.transform.CompareTag("Wall") || hit.transform.CompareTag("BluePortal");

    private readonly Func<RaycastHit2D, bool> wallsAndPortalsPredicate =
        hit => hit.transform.CompareTag("Wall") || hit.transform.CompareTag("BluePortal") ||
               hit.transform.CompareTag("RedPortal");

    public Sprite redPortalSprite;
    public Sprite bluePortalSprite;
    public AudioClip blueSound;
    public AudioClip redSound;
    public AudioClip failtureSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        var walls = GameObject.FindGameObjectsWithTag("Wall");

        if (Input.GetMouseButtonDown(RIGHT_MOUSE_BUTTON))
        {
            var spawnPosition = ComputeSpawnPosition(Portal.Red);
            if (IsPortalAbleToSet(walls, redPortal, bluePortal, spawnPosition))
            {
                if (redPortal == null)
                {
                    redPortal = new GameObject("RedPortal");
                    redPortal.AddComponent<SpriteRenderer>().sprite = redPortalSprite;
                    redPortal.GetComponent<SpriteRenderer>().sortingLayerName = "Portals";
                    redPortal.AddComponent<CapsuleCollider2D>().isTrigger = true;
                    redPortal.tag = "RedPortal";
                }

                audioSource.PlayOneShot(redSound);

                redPortal.transform.position = spawnPosition;
            }
        }

        if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
        {
            var spawnPosition = ComputeSpawnPosition(Portal.Blue);
            if (IsPortalAbleToSet(walls, bluePortal, redPortal, spawnPosition))
            {
                if (bluePortal == null)
                {
                    bluePortal = new GameObject("BluePortal");
                    bluePortal.AddComponent<SpriteRenderer>().sprite = bluePortalSprite;
                    bluePortal.GetComponent<SpriteRenderer>().sortingLayerName = "Portals";
                    bluePortal.AddComponent<CapsuleCollider2D>().isTrigger = true;
                    bluePortal.tag = "BluePortal";
                }

                audioSource.PlayOneShot(blueSound);

                bluePortal.transform.position = spawnPosition;
            }
        }
    }

    private Vector3 ComputeSpawnPosition(Portal currentPortal)
    {
        const float offsetX = 0.8f;
        const float offsetY = 1.3f;
        const float checkOffsetX = 0.75f;
        const float checkOffsetY = 1.25f;

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        var spawnPosition = mousePosition;

        var predicate = currentPortal == Portal.Blue ? wallsAndRedPortalPredicate : wallsAndBluePortalPredicate;

        var upHit = Physics2D.RaycastAll(mousePosition, Vector2.up, offsetY)
            .FirstOrDefault(predicate);
        var downHit = Physics2D.RaycastAll(mousePosition, Vector2.down, offsetY)
            .FirstOrDefault(predicate);
        var rightHit = Physics2D.RaycastAll(mousePosition, Vector2.right, offsetX)
            .FirstOrDefault(predicate);
        var leftHit = Physics2D.RaycastAll(mousePosition, Vector2.left, offsetX)
            .FirstOrDefault(predicate);

        if (upHit.collider != null) spawnPosition.y -= mousePosition.y + offsetY - upHit.collider.bounds.min.y;
        if (downHit.collider != null) spawnPosition.y += downHit.collider.bounds.max.y - (mousePosition.y - offsetY);
        if (rightHit.collider != null) spawnPosition.x -= mousePosition.x + offsetX - rightHit.collider.bounds.min.x;
        if (leftHit.collider != null) spawnPosition.x += leftHit.collider.bounds.max.x - (mousePosition.x - offsetX);

        var diagDistance = Math.Sqrt(Math.Pow(checkOffsetX, 2) + Math.Pow(checkOffsetY, 2)) * 0.75f;
        var diagUpRightHit = Physics2D.RaycastAll(spawnPosition, Vector2.up + Vector2.right, (float) diagDistance)
            .FirstOrDefault(predicate);

        if (diagUpRightHit.collider != null)
        {
            spawnPosition.y -= spawnPosition.y + offsetY - diagUpRightHit.collider.bounds.min.y;
            spawnPosition.x -= spawnPosition.x + offsetX - diagUpRightHit.collider.bounds.min.x;
        }

        var diagUpLeftHit = Physics2D.RaycastAll(spawnPosition, Vector2.up + Vector2.left, (float) diagDistance)
            .FirstOrDefault(predicate);
        if (diagUpLeftHit.collider != null)
        {
            spawnPosition.y -= spawnPosition.y + offsetY - diagUpLeftHit.collider.bounds.min.y;
            spawnPosition.x += diagUpLeftHit.collider.bounds.max.x - (spawnPosition.x - offsetX);
        }

        var diagDownRightHit = Physics2D.RaycastAll(spawnPosition, Vector2.down + Vector2.right, (float) diagDistance)
            .FirstOrDefault(predicate);
        if (diagDownRightHit.collider != null)
        {
            spawnPosition.y += diagDownRightHit.collider.bounds.max.y - (spawnPosition.y - offsetY);
            spawnPosition.x -= spawnPosition.x + offsetX - diagDownRightHit.collider.bounds.min.x;
        }

        var diagDownLeftHit = Physics2D.RaycastAll(spawnPosition, Vector2.down + Vector2.left, (float) diagDistance)
            .FirstOrDefault(predicate);
        if (diagDownLeftHit.collider != null)
        {
            spawnPosition.y += diagDownLeftHit.collider.bounds.max.y - (spawnPosition.y - offsetY);
            spawnPosition.x += diagDownLeftHit.collider.bounds.max.x - (spawnPosition.x - offsetX);
        }

        return Vector2.Distance(mousePosition, spawnPosition) > 3 ? Vector3.negativeInfinity : spawnPosition;
    }

    private bool IsPortalAbleToSet(GameObject[] walls, GameObject currentPortal, GameObject otherPortal,
        Vector3 position)
    {
        var isAbleToSet = false;
        if (!position.Equals(Vector3.negativeInfinity))
        {
            const float offsetX = 0.75f;
            const float offsetY = 1.25f;
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var colliders = walls.Select(gameObj => gameObj.GetComponent<Collider2D>()).ToList();
            var wallsCondition = !colliders.Any(col => col.OverlapPoint(mousePosition));

            var holdPointPosition = transform.position;
            var hitColliders = Physics2D.RaycastAll(holdPointPosition, mousePosition - holdPointPosition,
                    Vector2.Distance(holdPointPosition, mousePosition))
                .Select(rayCast => rayCast.collider).ToList();

            wallsCondition &= !colliders.Intersect(hitColliders).Any();

            bool portalCondition = true;

            if (wallsCondition && otherPortal != null)
            {
                portalCondition = !otherPortal.GetComponent<Collider2D>().OverlapPoint(mousePosition);
            }

            isAbleToSet = wallsCondition && portalCondition;

            if (isAbleToSet)
            {
                var upHit = Physics2D.RaycastAll(position, Vector2.up, offsetY)
                    .FirstOrDefault(wallsAndPortalsPredicate).collider;
                var downHit = Physics2D.RaycastAll(position, Vector2.down, offsetY)
                    .FirstOrDefault(wallsAndPortalsPredicate).collider;
                var rightHit = Physics2D.RaycastAll(position, Vector2.right, offsetX)
                    .FirstOrDefault(wallsAndPortalsPredicate).collider;
                var leftHit = Physics2D.RaycastAll(position, Vector2.left, offsetX)
                    .FirstOrDefault(wallsAndPortalsPredicate).collider;

                var diagDistance = Math.Sqrt(Math.Pow(offsetX, 2) + Math.Pow(offsetY, 2)) * 0.75f;
                var diagUpRightHit = Physics2D.RaycastAll(position, Vector2.up + Vector2.right, (float) diagDistance)
                    .FirstOrDefault(wallsAndPortalsPredicate).collider;
                var diagUpLeftHit = Physics2D.RaycastAll(position, Vector2.up + Vector2.left, (float) diagDistance)
                    .FirstOrDefault(wallsAndPortalsPredicate).collider;
                var diagDownRightHit = Physics2D
                    .RaycastAll(position, Vector2.down + Vector2.right, (float) diagDistance)
                    .FirstOrDefault(wallsAndPortalsPredicate).collider;
                var diagDownLeftHit = Physics2D.RaycastAll(position, Vector2.down + Vector2.left, (float) diagDistance)
                    .FirstOrDefault(wallsAndPortalsPredicate).collider;

                var hitColls = new List<Collider2D>
                {
                    upHit,
                    downHit,
                    rightHit,
                    leftHit,
                    diagDownLeftHit,
                    diagUpRightHit,
                    diagUpLeftHit,
                    diagDownRightHit
                };

                isAbleToSet &= hitColls.All(col =>
                    col == null || col == (currentPortal != null ? currentPortal.GetComponent<Collider2D>() : null));
            }
        }

        if (!isAbleToSet)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(failtureSound);
            }
        }

        return isAbleToSet;
    }
}