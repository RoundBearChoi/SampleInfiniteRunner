using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class InitialPushBack : StateComponent
    {
        private bool _initialPushBack = false;
        private Vector2 _force = Vector2.zero;
        private Unit _attacker = null;

        public InitialPushBack(Unit unit, Vector2 force, Unit attacker)
        {
            _unit = unit;
            _force = force;
            _attacker = attacker;
        }

        public override void OnFixedUpdate()
        {
            if (!_initialPushBack)
            {
                _initialPushBack = true;

                Vector3 push = Vector3.zero;

                if (_attacker != null)
                {
                    Vector2 dir = _attacker.transform.position - _unit.transform.position;

                    //attacker on rightside
                    if (dir.x > 0f)
                    {
                        push = new Vector3(_force.x * -1f, _force.y, 0f);
                    }
                    //attacker on leftside
                    else
                    {
                        push = new Vector3(_force.x, _force.y, 0f);
                    }
                }

                _unit.unitData.rigidBody2D.velocity = push;
            }
        }
    }
}