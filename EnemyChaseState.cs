using Godot;

[GlobalClass]
public partial class EnemyChaseState : State
{
	#region Signals

	[Signal] public delegate void HasLostMouseEventHandler();

	public override void _Ready()
	{
		SetPhysicsProcess(false);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		var direction = Vector2.Zero.DirectionTo(actor.GetLocalMousePosition());
		actor.Velocity = actor.Velocity.MoveToward(direction * actor.maxSpeed, actor.acceleration * (float)delta);

		actor.MoveAndSlide();

		if (vision.IsColliding())
		{
			EmitSignal(SignalName.HasLostMouse);
		}
	}

	#endregion

	#region Methods



	public override void EnterState()
	{
		base.EnterState();
	}

	public override void ExitState()
	{
		base.ExitState();
	}

	#endregion
}
