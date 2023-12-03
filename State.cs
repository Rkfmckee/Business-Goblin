using Godot;

[GlobalClass]
public partial class State : Node
{
	#region Fields

	[Export] protected BusinessGoblin actor;
	[Export] protected AnimatedSprite2D animator;
	[Export] protected RayCast2D vision;

	#endregion

	#region Signals

	[Signal]
	public delegate void StateFinishedEventHandler();

	public override void _Ready()
	{
		SetPhysicsProcess(false);
	}

	public override void _PhysicsProcess(double delta)
	{
		RotateSpriteWithMovement();
	}

	#endregion

	#region Methods


	public virtual void EnterState()
	{
		SetPhysicsProcess(true);
		animator.Play("move");
	}
	public virtual void ExitState()
	{
		SetPhysicsProcess(false);
	}

	private void RotateSpriteWithMovement()
	{
		// Rotate the sprite in the current velocity direction
		// Default to facing right
		var scale = animator.Scale;
		scale.X = scale.X == 0 ? 1 : -Mathf.Sign(actor.Velocity.X);
		animator.Scale = scale;
	}

	#endregion
}
