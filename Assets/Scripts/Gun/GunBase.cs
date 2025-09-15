using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunBase : MonoBehaviour
{   
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweentShoot = .3f;
    public KeyCode keycode = KeyCode.S;

    private Coroutine _currentCoroutine;


    IEnumerator ShootCoroutine()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweentShoot);
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
    }

    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }
}
