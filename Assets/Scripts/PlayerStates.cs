using UnityEngine;
using Ebac.StateMachine;

public class PlayerStates : MonoBehaviour
{
	public enum PlayerStateMachine
	{
		WALK,
		IDLE,
		JUMP
	}

	public StateMachine<PlayerStateMachine> stateMachine;

	private void Start()
	{
		Init();
	}

	public void Init()
	{
		stateMachine = new StateMachine<PlayerStateMachine>();
		stateMachine.Init();
		stateMachine.RegisterStates(PlayerStateMachine.IDLE, new StateBase());
		stateMachine.RegisterStates(PlayerStateMachine.JUMP, new StateBase());
		stateMachine.RegisterStates(PlayerStateMachine.WALK, new StateBase());

		stateMachine.SwitchState(PlayerStateMachine.IDLE);
	}

	public void Walking()
	{
		stateMachine.SwitchState(PlayerStateMachine.WALK);
		//Move();
	}

	public void Jumping()
	{
		stateMachine.SwitchState(PlayerStateMachine.JUMP);
		//Jump();
	}
}
