using UnityEngine;

public interface IGrabbableAndThrowable
{
    void Grab(GameObject holdPoint);
    void Throw(Vector2 velocity);
}
