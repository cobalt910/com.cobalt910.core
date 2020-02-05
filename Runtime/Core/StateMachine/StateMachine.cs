using System;
using UnityEngine;

namespace ProjectCore.StateMachine
{
    public class StateMachine
    {
        private readonly GameObject _container;
        public event Action<Type> OnStateChanged;
        public State ActiveState { get; private set; }

        public StateMachine(GameObject container, Type initialState = null)
        {
            _container = container;
            ApplyState(initialState);
        }

        public void ApplyState<T>() where T : State
        {
            ChangeState(typeof(T));
            OnStateChanged?.Invoke(typeof(T));
        }
        
        public void ApplyState(Type type)
        {
            ChangeState(type);
            OnStateChanged?.Invoke(type);
        }

        public State Execute(Type type) => State.ExecuteOn(type, _container, ActiveState);
        public State Execute<T>() where T : State =>  State.ExecuteOn(typeof(T), _container, ActiveState);
        
        private void ChangeState(Type type)
        {
            var prevState = ActiveState;
            ActiveState = null;
            if (prevState != null) prevState.FinishCommand(true);
            ActiveState = CreateState(type);
        }
        
        private State CreateState(Type type)
        {
            var state = (State) _container.AddComponent(type);
            state.StateMachine = this;
            
            return state;
        }
    }
}
