using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    public float spinSpeed = 30f;

    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }
}
