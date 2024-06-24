using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody> ();
    }

    public void Initialize() 
    {
        _rigidbody.velocity = _rigidbody.transform.up * _speed;
    }
}
