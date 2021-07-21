using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class Units
    {
        public static Units instance = null;
        public BaseMessageHandler unitsMessageHandler = null;

        private List<Unit> _listUnits = new List<Unit>();
        private List<BaseUnitCreator> _listUnitCreators = new List<BaseUnitCreator>();

        public Units()
        {
            instance = this;
            unitsMessageHandler = new UnitsMessageHandler(_listUnits);
        }

        public void AddCreator(BaseUnitCreator creator)
        {
            _listUnitCreators.Add(creator);
        }

        public void AddUnit(Unit unit)
        {
            _listUnits.Add(unit);
        }

        public Unit GetUnit<T>()
        {
            for (int i = _listUnits.Count - 1; i >= 0; i--)
            {
                if (_listUnits[i] is T)
                {
                    return _listUnits[i];
                }
            }

            return null;
        }

        public Unit GetLatestUnitByState<T>()
        {
            for (int i = _listUnits.Count - 1; i >= 0; i--)
            {
                if (_listUnits[i].iStateController != null)
                {
                    if (_listUnits[i].iStateController.GetCurrentState() != null)
                    {
                        if (_listUnits[i].iStateController.GetCurrentState() is T)
                        {
                            return _listUnits[i];
                        }
                    }
                }
            }

            return null;
        }

        public void ProcessCreators()
        {
            foreach (BaseUnitCreator creator in _listUnitCreators)
            {
                creator.AddUnits(_listUnits);
            }

            _listUnitCreators.Clear();
        }

        public void OnUpdate()
        {
            foreach(Unit unit in _listUnits)
            {
                unit.OnUpdate();

                if (unit.hpBar != null)
                {
                    unit.hpBar.Update();
                }
            }
        }

        public void OnFixedUpdate()
        {
            foreach (Unit unit in _listUnits)
            {
                if (unit.unitData.hp <= 0)
                {
                    BaseMessage zeroHealthMessage = new ZeroHealthMessage(unit);
                    zeroHealthMessage.Register();
                }
            }

            //death by fall
            foreach(Unit unit in _listUnits)
            {
                if (unit.transform.position.y < GameInitializer.current.gameDataSO.DefaultFallDeathY)
                {
                    if (unit.unitData.rigidBody2D != null)
                    {
                        unit.unitData.rigidBody2D.gravityScale = 0f;
                        unit.unitData.rigidBody2D.velocity = Vector2.zero;
                        unit.unitData.hp = 0;
                    }
                }
            }

            //main fixed update
            for (int i = _listUnits.Count - 1; i >= 0; i--)
            {
                if (_listUnits[i].destroy)
                {
                    GameObject.Destroy(_listUnits[i].gameObject);
                    _listUnits.RemoveAt(i);
                    continue;
                }

                if (_listUnits[i].unitData.facingRight)
                {
                    if (_listUnits[i].transform.rotation.y != 0f)
                    {
                        _listUnits[i].transform.rotation = Quaternion.Euler(_listUnits[i].transform.rotation.x, 0f, _listUnits[i].transform.rotation.z);
                    }
                }
                else
                {
                    if (_listUnits[i].transform.rotation.y != 180f)
                    {
                        _listUnits[i].transform.rotation = Quaternion.Euler(_listUnits[i].transform.rotation.x, 180f, _listUnits[i].transform.rotation.z);
                    }
                }
            
                _listUnits[i].OnFixedUpdate();
            }
        }

        public void OnLateUpdate()
        {
            for (int i = _listUnits.Count - 1; i >= 0; i--)
            {
                _listUnits[i].OnLateUpdate();
            }

            unitsMessageHandler.HandleMessages();
            unitsMessageHandler.ClearMessages();

            foreach (Unit unit in _listUnits)
            {
                if (unit.messageHandler != null)
                {
                    unit.messageHandler.HandleMessages();
                    unit.messageHandler.ClearMessages();
                }
            }
        }
    }
}