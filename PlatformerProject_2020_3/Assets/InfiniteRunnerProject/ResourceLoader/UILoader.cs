using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RB
{
    public class UILoader : GameResources<UIType>
    {
        public UILoader()
        {
            Debugger.Log("loading ui..");

            LoadObj<UITest.tempUI>(UIType.UI, "tempUI");
            LoadObj<UITest.DefaultUIBlock>(UIType.DEFAULT_UI_BLOCK, "DefaultUIBlock");
            LoadObj<UITest.RunnerDeathNotification>(UIType.RUNNER_DEATH_NOTIFICATION, "RunnerDeathNotification");

            LoadObj<CompatibleBaseUI>(UIType.COMPATIBLE_BASE_UI, "CompatibleBaseUI");
            LoadObj<CompatibleUILayer>(UIType.COMPATIBLE_UI_LAYER, "CompatibleUILayer");
        }
    }
}