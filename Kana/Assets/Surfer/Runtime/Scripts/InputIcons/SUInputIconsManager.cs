using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surfer
{
    [DefaultExecutionOrder(-400)]
    public class SUInputIconsManager : MonoBehaviour
    {
        public static SUInputIconsManager I { get; private set; } = default;

        void Awake()
        {
            if (I == null)
                I = this;
            else
                Destroy(this);
        }
        public Sprite GetIconSpriteFromGUID(string guid)
        {
            var platfData = GetPlatformData();
            if (platfData == null)
                return null;

            if (platfData.GetSpriteFromGUID(guid, out var sprite))
            {
                return sprite;
            }

            return null;
        }

        public Sprite GetIconSpriteFromName(string name)
        {
            var platfData = GetPlatformData();
            if (platfData == null)
                return null;

            if (platfData.GetSpriteFromName(name, out var sprite))
            {
                return sprite;
            }

            return null;
        }

        public Sprite GetIconResSpriteFromGUID(string guid)
        {
            var platfData = GetPlatformData();
            if (platfData == null)
                return null;

            if (platfData.GetResSpriteFromGUID(guid, out var sprite))
            {
                return sprite;
            }

            return null;
        }

        public Sprite GetIconResSpriteFromName(string name)
        {
            var platfData = GetPlatformData();
            if (platfData == null)
                return null;

            if (platfData.GetResSpriteFromName(name, out var sprite))
            {
                return sprite;
            }

            return null;
        }

        SUInputIconsData GetPlatformData()
        {
            SUPlatform_ID? platfID = Application.platform.ToPlatformID();

            if (platfID == null)
                return null;

            if (SurferHelper.SO.InputIcons.TryGetValue(platfID.Value, out var data))
            {
                return data;
            }

            return null;
        }

        private void OnDestroy()
        {
            I = null;
        }

    }
}

