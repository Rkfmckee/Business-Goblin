using System;
using Godot;

[GlobalClass]
public partial class EnemyWanderState : State
{
	#region Signals

	[Signal] public delegate void HasSeenMouseEventHandler();

	public override void _Ready()
	{
		base._Ready();

		SetPhysicsProcess(false);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		MoveAround((float)delta);
		BounceOffWalls((float)delta);
		LookForMouse();
	}

	#endregion

	#region Methods


	public override void EnterState()
	{
		base.EnterState();

		if (actor.Velocity == Vector2.Zero)
		{
			actor.Velocity = Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau)) * actor.maxSpeed;
		}
	}

	private void MoveAround(float delta)
	{
		actor.Velocity = actor.Velocity.MoveToward(actor.Velocity.Normalized() * actor.maxSpeed, actor.acceleration * delta);
	}

	private void BounceOffWalls(float delta)
	{
		var velocityWithDelta = new Vector2(actor.Velocity.X * delta, actor.Velocity.Y * delta);
		var collision = actor.MoveAndCollide(velocityWithDelta);

		if (collision == null) return;

		var bounceVelocity = actor.Velocity.Bounce(collision.GetNormal());
		actor.Velocity = bounceVelocity;
	}

	private void LookForMouse()
	{
		if (!vision.IsColliding())
		{
			EmitSignal(SignalName.HasSeenMouse);
		}
	}

	#endregion
}
