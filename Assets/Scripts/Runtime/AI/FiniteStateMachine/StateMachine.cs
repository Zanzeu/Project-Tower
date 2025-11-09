using Tower.Runtime.Gameplay;
using System.Collections.Generic;

namespace Tower.Runtime.AI
{
    public class StateMachine : IDecisionMaker
    {
        private AgentEntity _entity;
        private Dictionary<int, State> _states = new Dictionary<int, State>();
        private State _currentState;
        public StateMachine(AgentEntity entity)
        {
            _entity = entity;
        }

        public void AddState(State s)
        {
            s.Entity = _entity;
            s.SetStateMachine(this);
            _states[s.StateType] = s;
        }

        /// <summary>
        /// ÉèÖÃÄ¬ÈÏ×´Ì¬
        /// </summary>
        /// <param name="t">Ä¿±ê×´Ì¬</param>
        public void SetDefaultState(int t)
        {
            if (_states.TryGetValue(t, out _currentState))
            {
                _currentState.OnEnter();
            }
        }

        /// <summary>
        /// ÇÐ»»×´Ì¬
        /// </summary>
        /// <param name="t">Ä¿±ê×´Ì¬</param>
        /// <returns>×´Ì¬</returns>
        public State SwitchState(int t)
        {
            _states.TryGetValue(t, out var s);
            return s;
        }

        /// <summary>
        /// Ç¿ÖÆÇÐ»»×´Ì¬
        /// </summary>
        /// <param name="t">Ä¿±ê×´Ì¬</param>
        public void SwitchStateImmediate(int t)
        {
            if (_states.TryGetValue(t, out var nextState))
            {
                if (_currentState != null)
                {
                    _currentState.OnExit();
                }

                _currentState = nextState;

                if (_currentState != null)
                {
                    _currentState.OnEnter();
                }
            }
        }

        /// <summary>
        /// ×´Ì¬»ú¸üÐÂ
        /// </summary>
        public void OnUpdate()
        {
            if (_currentState == null)
            {
                return;
            }

            State nextState = _currentState.OnUpdate();

            if (nextState != _currentState)
            {
                _currentState.OnExit();
                _currentState = nextState;
                if (_currentState != null)
                {
                    _currentState.OnEnter();
                }
            }
        }
    }
}