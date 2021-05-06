using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public abstract class State
    {
        public State nextState = null;

        protected UnitData _unitData = null;
        protected UserInput _userInput = null;

        public virtual void OnEnter()
        {

        }

        public virtual void Update()
        {

        }
    }
}