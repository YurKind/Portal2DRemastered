using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    public bool isPaused;
    
    private const int LeftMouseButton = 0;
    private const int RightMouseButton = 1;

    private PlatformerCharacter2D character;
    private PortalGun portalGun;
    private bool isJumping;
    private bool facingRight = true;


    private void Awake()
    {
        character = GetComponent<PlatformerCharacter2D>();
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");   
        }

        if (Input.GetMouseButtonDown(LeftMouseButton) && !isPaused)
        {
            portalGun.SetBluePortal();
        }

        if (Input.GetMouseButtonDown(RightMouseButton) && !isPaused)
        {
            portalGun.SetRedPortal();
        }

        if (!isPaused)
        {
            FacePortalGunToMouse();
            LookAtMouse();
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
}
