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

	public void OnStateEnter (StateBase WALK)
	{
		stateMachine.SwitchState(PlayerStateMachine.WALK);
		//move();
	}

	public void OnStateExit (StateBase WALK)
	{
		stateMachine.SwitchState(PlayerStateMachine.IDLE);
	}

}
