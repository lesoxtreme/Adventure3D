using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class DestructibleItemBase : MonoBehaviour
{
	public HealthBase healthBase;

	public float shakeDuration = .1f;
	public int shakeForce = 5;

	public int dropCoinsAmount = 10;
	public GameObject coinPrefab;
	public Transform dropPosition;
	public Vector2 randomRange = new Vector2(-2f,2f);

	private void OnValidate()
	{
		if (healthBase == null) healthBase = GetComponent<HealthBase>();
	}

	private void Awake()
	{
		OnValidate();
		healthBase.OnDamage += OnDamage;
	}

	private void OnDamage(HealthBase h)
	{
		transform.DOShakeScale(shakeDuration, Vector3.up/2, shakeForce);
		DropGroupOfCoins();
	}

	[NaughtyAttributes.Button]
	private void DropCoins()
	{
		var i = Instantiate(coinPrefab);
		i.transform.position = dropPosition.position + Vector3.forward * Random.Range(randomRange.x,randomRange.y) + Vector3.right * Random.Range(randomRange.x,randomRange.y);
		i.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
	}

	private void DropGroupOfCoins()
	{
		StartCoroutine(DropGroupOfCoinsCoroutine());
	}

	IEnumerator DropGroupOfCoinsCoroutine()
	{
		for(int i =0;i < dropCoinsAmount; i++)
		{
			DropCoins();
			yield return new WaitForSeconds(.1f);
		}
	}
}
