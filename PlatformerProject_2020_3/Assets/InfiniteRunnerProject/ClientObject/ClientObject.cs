using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB.Client
{
    [System.Serializable]
    public class ClientObject : MonoBehaviour
    {
        [SerializeField]
        int _id = 0;

        [SerializeField]
        Vector3 _pos = new Vector3();

        [SerializeField]
        GameObject _playerPositionSphere = null;

        [SerializeField]
        private List<SpriteAnimation> _listSpriteAnimations = new List<SpriteAnimation>();

        [SerializeField]
        SpriteAnimation _currentAnimation = null;

        private bool _initialized = false;

        public int ID
        {
            get
            {
                return _id;
            }
        }

        public Vector3 POSITION
        {
            get
            {
                return _pos;
            }
        }

        public void SetID(int id)
        {
            _id = id;
        }

        public void SetPosition(Vector3 pos)
        {
            _pos = pos;
        }

        public void UpdatePosition()
        {
            _playerPositionSphere.transform.position = _pos;
        }

        public GameObject GetPlayerSphere()
        {
            return _playerPositionSphere;
        }

        public void UpdateDirection(bool facingRight)
        {
            if (facingRight)
            {
                if (_playerPositionSphere.transform.rotation.y != 0f)
                {
                    _playerPositionSphere.transform.rotation = Quaternion.Euler(_playerPositionSphere.transform.rotation.x, 0f, _playerPositionSphere.transform.rotation.z);
                }
            }
            else
            {
                if (_playerPositionSphere.transform.rotation.y != 180f)
                {
                    _playerPositionSphere.transform.rotation = Quaternion.Euler(_playerPositionSphere.transform.rotation.x, 180f, _playerPositionSphere.transform.rotation.z);
                }
            }
        }

        public void AddSpriteAnimations(UnitCreationSpec creationSpec)
        {
            if (!_initialized)
            {
                _initialized = true;
                _listSpriteAnimations = new List<SpriteAnimation>();
            }

            foreach (SpriteAnimationSpec aniSpec in creationSpec.listSpriteAnimationSpecs)
            {
                AddSpriteAnimation(aniSpec);
            }
        }

        void AddSpriteAnimation(SpriteAnimationSpec spec)
        {
            foreach(string str in spec.listSpriteNames)
            {
                GameObject obj = new GameObject(str);

                obj.transform.parent = _playerPositionSphere.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                _listSpriteAnimations.Add(obj.AddComponent<SpriteAnimation>());

                Sprite[] arr = ResourceLoader.LoadSpriteByString(str);

                if (arr != null)
                {
                    if (arr.Length > 0)
                    {
                        _listSpriteAnimations[_listSpriteAnimations.Count - 1].SetSpriteAnimationSpec(spec);
                        _listSpriteAnimations[_listSpriteAnimations.Count - 1].AddSpriteArray(arr);
                    }
                }
            }
        }

        public void SetAnimation(SpriteType spriteType)
        {
            foreach(SpriteAnimation ani in _listSpriteAnimations)
            {
                if (ani.ANIMATION_SPEC.spriteType == spriteType)
                {
                    ani.gameObject.SetActive(true);
                    
                    ani.ResetSpriteIndex();
                    ani.UpdateSpriteOnIndex();

                    _currentAnimation = ani;
                }
                else
                {
                    ani.gameObject.SetActive(false);
                }
            }
        }

        public SpriteAnimation GetCurrentAnimation()
        {
            return _currentAnimation;
        }
    }
}