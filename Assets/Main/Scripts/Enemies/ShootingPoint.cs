using UnityEngine;

public class ShootingPoint : MonoBehaviour
{
	public Sprite shootingSprite;
	public GameObject turret;

	private SpriteRenderer spriteRenderer;
	void Start ()
	{
		spriteRenderer = transform.GetComponent<SpriteRenderer>();
	}
	
	void Update ()
	{
		var isShooting = turret.GetComponent<ITurret>().IsShooting();
		if (isShooting)
		{
			spriteRenderer.sprite = spriteRenderer.sprite == null ? shootingSprite : null;
		}
		else
		{
			spriteRenderer.sprite = null;
		}
	}
}
