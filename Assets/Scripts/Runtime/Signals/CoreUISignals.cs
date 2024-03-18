using System;
using Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreUISignals : MonoBehaviour
    {
        #region  singleton

        public static CoreUISignals Instance;
        
        private void Awake()
        {
            if (Instance!=null && Instance!=this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        #endregion

        public UnityAction<UIPanelTypes, int> onOpenPanel = delegate { };
        public UnityAction<int> onClosepanel = delegate { };
        public UnityAction onCloseAllPanel = delegate { };

    }
}