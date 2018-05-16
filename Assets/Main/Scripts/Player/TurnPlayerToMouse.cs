using UnityEngine;

public class TurnPlayerToMouse : MonoBehaviour {

    private bool m_FacingRight = true;

    private void Update () {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (m_FacingRight && transform.position.x > mousePosition.x)
        {
            Flip();
        }
        if (!m_FacingRight && transform.position.x < mousePosition.x)
        {
            Flip();
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
