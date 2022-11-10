using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;

namespace Surfer
{

    [System.Serializable]
    public class SUTKSizeData : SUAnimationData
    {
        [SerializeField]
        SUTKValueData _fromWidth, _toWidth, _fromHeight, _toHeight = default;

        protected override bool IsToolkit => true;
        protected override bool IsAvailable => true;

        protected override void OnPlay(VisualElement vEle)
        {

            _idTween = SizePrefix + this.GetHashCode();

            SetFrom(vEle);
            SetTo(vEle);
        }


        void SetFrom(VisualElement vEle)
        {
            if(_fromWidth.HasValue)
                vEle.style.width = _fromWidth.GetStyleLength();

            if(_fromHeight.HasValue)
                vEle.style.height =  _fromHeight.GetStyleLength();
        }

        void SetTo(VisualElement vEle)
        {

            if(float.IsNaN(vEle.resolvedStyle.width))
            {
                SurferManager.I.StartCoroutine(WaitToSet(vEle));
                return;
            }

            if(_toWidth.HasValue)
            {
                var isPercent = _toWidth.Unit == LengthUnit.Percent;

                DOTween.To(() => isPercent ? ((vEle.layout.width/vEle.parent.layout.width)*100f) : vEle.layout.width, 
                newValue => 
                {
                    vEle.style.width = new StyleLength(new Length(newValue,_toWidth.Unit));
                }, 
                _toWidth.GetStyleLength().value.value, _duration)
                .SetEase(_ease,6).SetLoops(_looped ? -1 : 0,_loop)
                .SetDelay(Delay)
                .SetId(_idTween)
                .SetUpdate(_useUnscaledTime)
                .Play();
            }
            
            if(_toHeight.HasValue)
            {
                var isPercent = _toHeight.Unit == LengthUnit.Percent;

                DOTween.To(() => isPercent ? ((vEle.layout.height/vEle.parent.layout.height)*100f) : vEle.layout.height, 
                newValue => 
                {
                    vEle.style.height = new StyleLength(new Length(newValue,_toHeight.Unit));
                }, 
                _toHeight.GetStyleLength().value.value, _duration)
                .SetEase(_ease,6).SetLoops(_looped ? -1 : 0,_loop)
                .SetDelay(Delay)
                .SetId(_idTween)
                .SetUpdate(_useUnscaledTime)
                .Play();
            }

        }

        IEnumerator WaitToSet(VisualElement vEle)
        {
            yield return new WaitForEndOfFrame();
            SetTo(vEle);
        }

    }

}


#endif