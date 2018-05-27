using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    private const int LeftMouseButton = 0;
    private const int RightMouseButton = 1;
    
    private PlatformerCharacter2D character;
    private PortalGun portalGun;
    private bool isJumping;


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
    }


    private void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        character.Move(h, isJumping);
        isJumping = false;
    }
}
