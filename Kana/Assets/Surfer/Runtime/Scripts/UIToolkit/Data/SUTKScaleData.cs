using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;

namespace Surfer
{

    [System.Serializable]
    public class SUTKScaleData : SUAnimationData
    {
        [SerializeField]
        Vector3 _from = Vector3.one , _to = Vector3.one;

        protected override bool IsToolkit => true;
        protected override bool IsAvailable => true;

        protected override void OnPlay(VisualElement vEle)
        {

            _idTween = ScalePrefix + this.GetHashCode();

            SetFrom(vEle);
            SetTo(vEle);
        }


        void SetFrom(VisualElement vEle)
        {
            vEle.style.scale = new StyleScale(new Scale(_from));
        }

        void SetTo(VisualElement vEle)
        {

            DOTween.To(() => vEle.style.scale.value.value, 
            newValue => 
            {
                vEle.style.scale = new StyleScale(new Scale(newValue));
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