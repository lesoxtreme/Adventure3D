using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HealthBase : MonoBehaviour , IDamageable
{
	public float startLife = 10f;
	public bool destroyOnKill = false;
	[SerializeField] private float _currentLife;

	public Action<HealthBase> OnDamage;
	public Action<HealthBase> OnKill;
	public List<UIUpdater> uiUpdater;


	private void Awake()
	{
		Init();
	}

	public void Init()
	{
		ResetLife();
	}

	public void ResetLife()
	{
		_currentLife = startLife;
	}

	protected virtual void Kill()
	{
		if(destroyOnKill)
			Destroy(gameObject, 3f);
		
		OnKill?.Invoke(this);
	}
	[NaughtyAttributes.Button]
	public void Damage()
	{
		Damage(5);
	}

	public void Damage(float f)
	{
		_currentLife -= f; 
		if(_currentLife <= 0)
		{
			Kill();
		}
		UpdateUI();
		OnDamage?.Invoke(this);
	}

	public void Damage(float damage, Vector3 dir)
	{
		Damage(damage);
	}

	private void UpdateUI()
	{
		if(uiUpdater != null)
		{
			uiUpdater.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
		}
	}
}
