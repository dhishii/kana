using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;

namespace Surfer
{

    [System.Serializable]
    public class SUTKRotationData : SUAnimationData
    {
        [SerializeField]
        float _from, _to = default;

        protected override bool IsToolkit => true;
        protected override bool IsAvailable => true;

        protected override void OnPlay(VisualElement vEle)
        {

            _idTween = RotationPrefix + this.GetHashCode();

            SetFrom(vEle);
            SetTo(vEle);
        }


        void SetFrom(VisualElement vEle)
        {
            vEle.style.rotate = new StyleRotate(new Rotate(new Angle(_from,AngleUnit.Degree)));
        }

        void SetTo(VisualElement vEle)
        {

            DOTween.To(() => vEle.style.rotate.value.angle.value, 
            newValue => 
            {
                vEle.style.rotate = new StyleRotate(new Rotate(new Angle(newValue,AngleUnit.Degree)));
            }, 
            _to, _duration)
            .SetEase(_ease,6).SetLoops(_looped ? -1 : 0,_loop)
            .SetDelay(Delay)
            .SetId(_idTween)
            .SetUpdate(_useUnscaledTime)
            .Play();

        }

    }

}


#endif