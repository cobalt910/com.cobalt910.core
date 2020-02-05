using System;
using System.Collections.Generic;
using com.cobalt910.core.Runtime.Core.Misc;
using UnityEngine;

namespace com.cobalt910.core.Runtime.Core.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        public StateMachine StateMachine;
        
        public bool IsRunning { get; private set; } = true;
        public bool IsStarted { get; private set; }
        public bool ResourcesReleased { get; private set; }
        
        public List<State> ChildStates { get; } = new List<State>();
        public State DestroyHandler { get; private set; }
        
        public event Action<State> OnResolve;
        public event Action<State> OnReject;

        protected virtual void Start()
        {
            if (!IsRunning) return;
            
            IsStarted = true;
            OnLoad();
        }

        private void Update()
        {
            if (IsRunning) OnUpdate();
        }

        protected virtual void OnLoad() { }
        protected virtual void OnUpdate(){ }
        protected virtual void OnReleaseResources() { }

        public void Terminate()
        {
            FinishCommand(false);
        }

        public void FinishCommand(bool success)
        {
            if (!IsRunning) return;

            IsRunning = false;
            OnFinishCommand(success);
            Destroy(this);
        }

        private void OnCommandDestroy(State state)
        {
            var commandId = state.GetInstanceID();
            var removeIndex = ChildStates.FindIndex(x => x.GetInstanceID().Equals(commandId));
            if (removeIndex != -1) ChildStates.RemoveAt(removeIndex);
        }

        private void OnFinishCommand(bool success)
        {
            foreach (var childCommand in ChildStates)
                childCommand.FinishCommand(success);

            if (success) OnResolve?.Invoke(this);
            else OnReject?.Invoke(this);
        }

        private void OnDestroy()
        {
            var stateName = GetType().Name;
            if (DestroyHandler != null) Debug.Log("Sub-State <color=green>" + stateName + "</color> destroyed.");
            else Debug.Log("State <color=green>" + stateName + "</color> destroyed.");

            if (!ResourcesReleased && IsStarted)
            {
                if (DestroyHandler) DestroyHandler.OnCommandDestroy(this);

                try
                {
                    OnReleaseResources();
                }
                catch (Exception e)
                {
                    Debug.LogError("Error during release command resources " + e);
                }

                ResourcesReleased = true;
            }
            else
            {
                Debug.LogError($"Resource release for State {stateName} skipped, started:{IsStarted}, released:{ResourcesReleased}");
            }
        }

        public static State ExecuteOn<T>(GameObject target, State state = null) where T : State
        {
            return ExecuteOn(typeof(T), target, state);
        }
        
        public static State ExecuteOn(Type type, GameObject target, State state = null)
        {
            var command = (State) target.AddComponent(type);

            if (state.IsNull()) return command;

            state.ChildStates.Add(command);
            command.DestroyHandler = state;
            command.StateMachine = state.StateMachine;
            
            return command;
        }
    }
}