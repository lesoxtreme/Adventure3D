using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GunShootLimit : GunBase
{
	public List<UIGunUpdater> uiGunUpdaters;

	public float maxShoot = 5;
	public float timeToRecharge = 1f;

	private float _currentShoots;
	private bool _recharging = false;

	private void Awake()
	{
		GetAllUis();
	}

	protected override IEnumerator ShootCoroutine()
	{
       /* while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweentShoot);
        }*/
		if (_recharging) yield break;
		while(true)
		{
			if(_currentShoots < maxShoot)
			{
				Shoot();
				_currentShoots++;
				CheckRecharge();
				UpdateUI();
				yield return new WaitForSeconds(timeBetweentShoot);
			}
		}
	}

	private void CheckRecharge()
	{
		if (_currentShoots >= maxShoot)
		{
			StopShoot();
			StartRecharge();
		}
	}

	private void StartRecharge()
	{
		_recharging =true;
		StartCoroutine(RechargeCoroutine());

	}

	IEnumerator RechargeCoroutine()
	{
		float time = 0;
		while(time < timeToRecharge)
		{
			time += Time.deltaTime;
			uiGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
			yield return new WaitForEndOfFrame();
		}
		_currentShoots = 0;
		_recharging = false;
	}

	private void UpdateUI()
	{
		uiGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
	}

	private void GetAllUis()
	{
		uiGunUpdaters = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();
	}
}
