using UnityEngine;

public class EnterThePortal : MonoBehaviour {
    private void Update () {

        if (Input.GetButtonDown("EnterThePortal"))
        {
            GameObject redPortal = GameObject.Find("RedPortal");
            GameObject bluePortal = GameObject.Find("BluePortal");


            if (redPortal == null || bluePortal == null) return;

            float playerX = transform.position.x;
            float playerY = transform.position.y;

            float redPortalX = redPortal.transform.position.x;
            float redPortalY = redPortal.transform.position.y;
            float bluePortalX = bluePortal.transform.position.x;
            float bluePortalY = bluePortal.transform.position.y;

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
}
