using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class HorizontalParallax : StateComponent
    {
        Vector2 _basePos = Vector2.zero;
        float _percentage = 0f;
        
        public HorizontalParallax(Unit unit, Vector2 basePos, float percentage)
        {
            _unit = unit;
            _basePos = basePos;
            _percentage = percentage;
        }

        public override void OnFixedUpdate()
        {
            Vector3 pos = _unit.OWNER_STAGE.CAMERA_SCRIPT.CAMERA.transform.position * _percentage;
            _unit.transform.position = new Vector3(_basePos.x + pos.x, _unit.transform.position.y, _unit.transform.position.z);
        }
    }
}