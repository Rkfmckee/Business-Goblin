using Godot;

[GlobalClass]
public partial class FiniteStateMachine : Node
{
	#region Fields

	[Export]
	private State state;

	#endregion

	#region Properties

	public State State => state;

	#endregion

	#region Signals

	public override void _Ready()
	{
		ChangeState(state);
	}

	#endregion

	#region Methods

	public void ChangeState(State newState)
	{
		if (state is State) state.ExitState();

		newState.EnterState();
		state = newState;
	}

	#endregion
}
