using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
	public GunBase gunBase;

	protected override void Init()
	{
		base.Init();

		inputs.GamePlay.Shoot.performed += cts => StartShoot();
		inputs.GamePlay.Shoot.canceled += cts => CancelShoot();
	}


	private void StartShoot()
	{
		gunBase.StartShoot();
		Debug.Log("Start Shoot");
	}

	private void CancelShoot()
	{
		Debug.Log("Shoot cancelled");
		gunBase.StopShoot();
	}
}
