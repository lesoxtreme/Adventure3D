using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
	
	public GunBase gunBase;
	public GunBase gunAngle;
	public GunBase gunShootLimit;
	public Transform gunPosition;

	private GunBase _currentGun;

	protected override void Init()
	{
		base.Init();

		CreateGun();
		inputs.GamePlay.Shoot.performed += cts => StartShoot();
		inputs.GamePlay.Shoot.canceled += cts => CancelShoot();
		inputs.GamePlay.Change1.performed += cts => Change1();
		inputs.GamePlay.Change2.performed += cts => Change2();	
		inputs.GamePlay.Change3.performed += cts => Change3();

	}

	private void CreateGun()
	{
		_currentGun = Instantiate(gunBase, gunPosition);

		_currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
		
	}

	private void StartShoot()
	{
		_currentGun.StartShoot();
		Debug.Log("Start Shoot");
	}

	private void CancelShoot()
	{
		Debug.Log("Shoot cancelled");
		_currentGun.StopShoot();
	}

	public virtual void Change1()
    {
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			_currentGun = Instantiate(gunBase, gunPosition);
		}
    }
    public virtual void Change2()
    {
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			_currentGun = Instantiate(gunAngle, gunPosition);
		}
    }
    public virtual void Change3()
    {
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			_currentGun = Instantiate(gunShootLimit, gunPosition);
		}
    }
}
