using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class InitialTextGUIMaterial : StateComponent
    {
        private Material _defaultMaterial = null;
        private uint _totalFrames = 0;

        public InitialTextGUIMaterial(Unit unit, uint totalFrames)
        {
            _unit = unit;
            _totalFrames = totalFrames;
        }

        public override void OnFixedUpdate()
        {
            if (_defaultMaterial == null)
            {
                _defaultMaterial = _unit.unitData.spriteAnimations.currentAnimation.RENDERER.sharedMaterial;
                _unit.unitData.spriteAnimations.currentAnimation.RENDERER.sharedMaterial = GameInitializer.current.white_GUIText_material;
            }

            if (_unit.iStateController.GetCurrentState().updateCount > _totalFrames)
            {
                _unit.unitData.spriteAnimations.currentAnimation.RENDERER.sharedMaterial = _defaultMaterial;
            }
        }
    }
}