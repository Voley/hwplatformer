using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderDetector : MonoBehaviour
{
    public bool IsColliding {  get; private set; }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsColliding = false;
    }
}
