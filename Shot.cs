using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Shot : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _intervalToShot;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _targetToShot;

    private bool _isShooting = true;
    private Coroutine _shootingCoroutine;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_shootingCoroutine == null)
            {
                _shootingCoroutine = StartCoroutine(ShootingWorker());
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_shootingCoroutine != null)
            {
                StopCoroutine(_shootingCoroutine);
                _shootingCoroutine = null;
            }
        }
    }

    private IEnumerator ShootingWorker()
    {
        while (_isShooting)
        {
            ShootBullet();

            yield return new WaitForSeconds(_intervalToShot);
        }
    }

    private void ShootBullet()
    {
        Vector3 direction = (_targetToShot.position - transform.position).normalized;
        Bullet newBullet = Instantiate(_bulletPrefab, transform.position + direction, Quaternion.identity);

        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
        bulletRigidbody.transform.up = direction;
        bulletRigidbody.velocity = direction * _bulletSpeed;
    }
}