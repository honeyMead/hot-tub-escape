using UnityEngine;

public class DestoryIfInvisible : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
