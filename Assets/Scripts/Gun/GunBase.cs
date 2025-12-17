using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunBase : MonoBehaviour
{   
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweentShoot = .3f;
    public float speed = 50f;
    public SFXType sfxType;

    private Coroutine _currentCoroutine;

    private void PlaySFX()
    {
        SFXPool.Instance.Play(sfxType);
    }

    protected virtual IEnumerator ShootCoroutine()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweentShoot);
        }
    }

    public virtual void Shoot()
    {
        PlaySFX();
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;

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
