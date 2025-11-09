using System;
using System.Collections.Generic;

namespace Tower.Runtime.GameSystem
{   
    public interface ISystem
    {
        void OnAwake();
        void OnStart();
        void OnDestory();
    }

    public interface ISystemUpdate
    {
        void OnUpdate();
    }

    public abstract class SystemBase : ISystem
    {

        public virtual void OnAwake() { }
        public virtual void OnStart() { }
        public virtual void OnDestory() { }
    }
}