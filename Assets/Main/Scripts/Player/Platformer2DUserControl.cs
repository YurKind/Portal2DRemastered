using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    public bool isPaused;

    private const int LeftMouseButton = 0;
    private const int RightMouseButton = 1;

    private PlatformerCharacter2D character;
    private PortalGun portalGun;
    private bool isJumping;
    private bool facingRight = true;
    private GameObject grabbedObject;
    private GameObject holdPoint;


    private void Awake()
    {
        character = GetComponent<PlatformerCharacter2D>();
        holdPoint = GameObject.Find("HoldPoint");
        portalGun = GetComponentInChildren<PortalGun>();
    }


    private void Update()
    {
        if (!isJumping)
        {
            isJumping = Input.GetKeyDown(KeyCode.Space);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = isPaused ? 1f : 0f;
            isPaused = !isPaused;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryToGrabOrThrowObject();
            TryToUseObject();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (!isPaused)
        {
            FacePortalGunToMouse();
            LookAtMouse();

            if (Input.GetMouseButtonDown(LeftMouseButton))
            {
                portalGun.SetBluePortal();
            }

            if (Input.GetMouseButtonDown(RightMouseButton))
            {
                portalGun.SetRedPortal();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                EnterThePortal();
            }
        }
    }

    private void TryToUseObject()
    {
        var collsNearPlayer = Physics2D.OverlapCircleAll(transform.position, 1f);
        var usableCollider = collsNearPlayer.FirstOrDefault(col =>
            col.gameObject.GetComponent<ICanBeUsed>() != null
        );

        if (usableCollider == null)
        {
            return;
        }
        usableCollider.gameObject.GetComponent<ICanBeUsed>().Use();
    }

    private void TryToGrabOrThrowObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.GetComponent<IGrabbableAndThrowable>().Throw(transform.GetComponent<Rigidbody2D>().velocity);
            grabbedObject = null;
        }
        else
        {
            var grabbableCollider = Physics2D.OverlapCircleAll(holdPoint.transform.position, 1f).FirstOrDefault(col =>
                col.gameObject.GetComponent<IGrabbableAndThrowable>() != null
            );

            if (grabbableCollider == null)
            {
                return;
            }

            grabbedObject = grabbableCollider.gameObject;
            grabbedObject.GetComponent<IGrabbableAndThrowable>().Grab(holdPoint);
        }
    }


    private void FixedUpdate()
    {
        var h = CrossPlatformInputManager.GetAxis("Horizontal");

        character.Move(h, isJumping);
        isJumping = false;
    }

    private void FacePortalGunToMouse()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var direction = new Vector2(
            mousePosition.x - portalGun.transform.position.x,
            mousePosition.y - portalGun.transform.position.y
        );

        portalGun.transform.up = direction;
    }

    private void LookAtMouse()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (facingRight && transform.position.x > mousePosition.x)
        {
            Flip();
        }

        if (!facingRight && transform.position.x < mousePosition.x)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void EnterThePortal()
    {
        var redPortal = GameObject.Find("RedPortal");
        var bluePortal = GameObject.Find("BluePortal");

        if (redPortal == null || bluePortal == null) return;

        var playerX = transform.position.x;
        var playerY = transform.position.y;

        var redPortalX = redPortal.transform.position.x;
        var redPortalY = redPortal.transform.position.y;
        var bluePortalX = bluePortal.transform.position.x;
        var bluePortalY = bluePortal.transform.position.y;

        if (System.Math.Abs(redPortalX - playerX) <= 1 && System.Math.Abs(redPortalY - playerY) <= 1)
        {
            transform.position = bluePortal.transform.position;
        }
        else if (System.Math.Abs(bluePortalX - playerX) <= 1 && System.Math.Abs(bluePortalY - playerY) <= 1)
        {
            transform.position = redPortal.transform.position;
        }
    }
}