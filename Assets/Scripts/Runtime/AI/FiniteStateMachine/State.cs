using Tower.Runtime.Gameplay;

namespace Tower.Runtime.AI
{
    public abstract class State
    {
        public abstract int StateType{ get; protected set; }
        public abstract string StateName { get; protected set; }

        public AgentEntity Entity{ get; set; }
        protected StateMachine _stateMachine;
        public void SetStateMachine(StateMachine stataMachine)
        {
            _stateMachine = stataMachine;
        }
        public virtual void OnEnter() { }
        public virtual State OnUpdate() { return null; }
        public virtual void OnExit() { }
    }
}
