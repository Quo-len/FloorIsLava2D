using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public delegate void TouchAction();
    public static event TouchAction OnTouched;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            if (OnTouched != null)
            {
                OnTouched();
            }
        }
    }


}
