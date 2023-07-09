using UnityEngine;

public class DestoryIfInvisible : MonoBehaviour
{
    private bool canDestroy = false;

    void OnBecameVisible()
    {
        canDestroy = true;
    }

    void OnBecameInvisible()
    {
        if (canDestroy)
        {
            Destroy(gameObject);
        }
    }
}
