using Godot;

[GlobalClass]
public partial class BusinessGoblin : CharacterBody2D
{
	#region References

	private RayCast2D vision;
	private FiniteStateMachine finiteStateMachine;
	private EnemyWanderState enemyWanderState;
	private EnemyChaseState enemyChaseState;

	#endregion

	#region Constants

	public float maxSpeed = 40;
	public float acceleration = 100;

	#endregion

	#region Signals

	public override void _Ready()
	{
		vision = GetNode<RayCast2D>("RayCast2D");
		finiteStateMachine = GetNode<FiniteStateMachine>("FiniteStateMachine");

		enemyWanderState = GetNode<EnemyWanderState>("FiniteStateMachine/EnemyWanderState");
		enemyWanderState.HasSeenMouse += () => finiteStateMachine.ChangeState(enemyChaseState);

		enemyChaseState = GetNode<EnemyChaseState>("FiniteStateMachine/EnemyChaseState");
		enemyChaseState.HasLostMouse += () => finiteStateMachine.ChangeState(enemyWanderState);
	}

	public override void _PhysicsProcess(double delta)
	{
		vision.TargetPosition = GetLocalMousePosition();
	}

	#endregion
}
