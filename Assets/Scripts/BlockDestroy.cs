using UnityEngine;

public class BlockDestroy : MonoBehaviour
{
    void Start(){}
    void Update(){}
    
    // Destroy the ground block beyond the camera's field of view
    void  OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
