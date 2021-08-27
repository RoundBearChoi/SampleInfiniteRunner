using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public abstract class UISelection : MonoBehaviour
    {
        //public BaseMessageHandler messageHandler = null;

        [SerializeField]
        protected List<UIOption> _listOptions = new List<UIOption>();

        [SerializeField]
        protected int _currentSelectionIndex = 0;

        public abstract void InitSelection();
        public abstract void OnFixedUpdate();
        public abstract void OnUpdate();

        public virtual void UpSelection()
        {
            _currentSelectionIndex++;

            if (_currentSelectionIndex >= _listOptions.Count)
            {
                _currentSelectionIndex = 0;
            }
        }

        public virtual void DownSelection()
        {
            _currentSelectionIndex++;

            if (_currentSelectionIndex < 0)
            {
                _currentSelectionIndex = _listOptions.Count - 1;
            }
        }
    }
}