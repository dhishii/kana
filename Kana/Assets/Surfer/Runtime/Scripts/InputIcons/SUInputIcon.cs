using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Surfer
{
    public class SUInputIcon : MonoBehaviour
    {

        [SerializeField]
        string _spriteGUID = default;

        [SerializeField]
        string _spriteResGUID = default;

        Image _imgCp = default;

        private void Awake()
        {

            UpdateGraphics();

        }

        void UpdateGraphics()
        {
            var sprite = SUInputIconsManager.I.GetIconSpriteFromGUID(_spriteGUID);
            if (sprite != null)
            {
                GetImageComponent();

                if (_imgCp != null)
                {
                    _imgCp.sprite = sprite;
                }
            }

            var resSprite = SUInputIconsManager.I.GetIconResSpriteFromGUID(_spriteResGUID);
            if (resSprite != null)
            {
                GetImageComponent();

                if (_imgCp != null)
                {
                    _imgCp.sprite = resSprite;
                }
            }
        }

        void GetImageComponent()
        {
            if (_imgCp != null)
                return;

            _imgCp = GetComponent<Image>();

        }

    }
}
