using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Ebac.StateMachine;
using DG.Tweening;

namespace Boss
{
	public enum BossAction
	{
		INIT,
		IDLE,
		WALK,
		ATTACK,
		DEATH
	}

	public class BossBase : MonoBehaviour
	{
		[Header("Animation")]
		public float startAnimationDuration = .5f;
		public Ease startAnimationEase = Ease.OutBack;

		[Header("AttackAnimation")]
		public float attackAmount = 5;
		public float timeBetweenAttacks = .5f;

		public GunBase gunBase;
		private Player _player;
		public float speed = 5f;
		public List<Transform> waypoints;

		public HealthBase healthBase;


		private StateMachine<BossAction> stateMachine;

		private void OnValidate()
		{
		if(healthBase == null) healthBase = GetComponent<HealthBase>();
		}


		private void Awake()
		{
			OnValidate();
			Init();
			healthBase.OnKill += OnBossKill;
			StartAttack();
		}

		private void Start()
		{
		_player = GameObject.FindObjectOfType<Player>();
		}

		private void Init()
		{
			stateMachine = new StateMachine<BossAction>();
			stateMachine.Init();

			stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
			stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
			stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
			stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
		}

		#region ATTACK
		public void StartAttack(Action endCallback = null)
		{
			StartCoroutine(AttackCoroutine(endCallback));
		}

		IEnumerator AttackCoroutine(Action endCallback)
		{
			int attacks = 0;
			while (attacks < attackAmount)
			{
				attacks++;
				gunBase.StartShoot();
				transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
				transform.LookAt(_player.transform.position);
				yield return new WaitForSeconds(timeBetweenAttacks); 
			}

			endCallback?.Invoke();
		}
		#endregion
		
		public void GoToRandomPoint(Action onArrive = null)
		{
			StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
		}

		IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
		{
			while(Vector3.Distance(transform.position, t.position) > 1f)
			{
				transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
				transform.LookAt(t);
				yield return new WaitForEndOfFrame();
			}
			onArrive?.Invoke();
		}


		private void OnBossKill(HealthBase h)
		{
			SwitchState(BossAction.DEATH);
		}

		#region ANIMATION
		public void StartInitAnimation()
		{
			transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
		}
		#endregion
		#region DEBUG
		[NaughtyAttributes.Button]

		private void SwitchInit()
		{
			SwitchState(BossAction.INIT);
		}
		[NaughtyAttributes.Button]
		private void SwitchWalk()
		{
			SwitchState(BossAction.WALK);
		}

		[NaughtyAttributes.Button]
		private void SwitchAttack()
		{
			SwitchState(BossAction.ATTACK);
			gunBase.StartShoot();
		}

		#endregion
		#region STATE MACHINE
		public void SwitchState(BossAction state)
		{
			stateMachine.SwitchState(state, this);
		}
		#endregion
	}
}