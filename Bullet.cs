using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody> ();
    }
}
