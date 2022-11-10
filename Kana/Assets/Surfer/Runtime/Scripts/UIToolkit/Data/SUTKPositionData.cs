using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;

namespace Surfer
{


    [System.Serializable]
    public class SUTKPositionData : SUAnimationData
    {

        [SerializeField]
        Position _positionType = default;

        [SerializeField]
        SUTKValueData _fromLeft, _fromRight, _fromTop, _fromBottom,
        _toLeft, _toRight, _toTop, _toBottom = default;

        protected override bool IsToolkit => true;
        protected override bool IsAvailable => true;

        protected override void OnPlay(VisualElement vEle)
        {

            _idTween = PositionPrefix + this.GetHashCode();

            vEle.style.position = _positionType;

            SetFrom(vEle);
            SetTo(vEle);
        }


        void SetFrom(VisualElement vEle)
        {

            if(_fromLeft.HasValue) vEle.style.left = _fromLeft.GetStyleLength();
            if(_fromRight.HasValue) vEle.style.right = _fromRight.GetStyleLength();
            if(_fromTop.HasValue) vEle.style.top = _fromTop.GetStyleLength();
            if(_fromBottom.HasValue) vEle.style.bottom = _fromBottom.GetStyleLength();

        }

        void SetTo(VisualElement vEle)
        {

            if(_toLeft.HasValue)
            {
                DOTween.To(() => vEle.style.left.value.value, 
                newValue => 
                {
                    vEle.style.left = new StyleLength(new Length(newValue,_toLeft.Unit));
                }, 
                _toLeft.GetStyleLength().value.value, _duration)
                .SetEase(_ease,6).SetLoops(_looped ? -1 : 0,_loop)
                .SetDelay(Delay)
                .SetId(_idTween)
                .SetUpdate(_useUnscaledTime)
                .Play();
            }
            
            if(_toRight.HasValue)
            {
                DOTween.To(() => vEle.style.right.value.value, 
                newValue => 
                {
                    vEle.style.right = new StyleLength(new Length(newValue,_toRight.Unit));
                }, 
                _toRight.GetStyleLength().value.value, _duration)
                .SetEase(_ease,6).SetLoops(_looped ? -1 : 0,_loop)
                .SetDelay(Delay)
                .SetId(_idTween)
                .SetUpdate(_useUnscaledTime)
                .Play();
            }

            if(_toTop.HasValue)
            {
                DOTween.To(() => vEle.style.top.value.value, 
                newValue => 
                {
                    vEle.style.top = new StyleLength(new Length(newValue,_toTop.Unit));
                }, 
                _toTop.GetStyleLength().value.value, _duration)
                .SetEase(_ease,6).SetLoops(_looped ? -1 : 0,_loop)
                .SetDelay(Delay)
                .SetId(_idTween)
                .SetUpdate(_useUnscaledTime)
                .Play();
            }

            if(_toBottom.HasValue)
            {
                DOTween.To(() => vEle.style.bottom.value.value, 
                newValue => 
                {
                    vEle.style.bottom = new StyleLength(new Length(newValue,_toBottom.Unit));
                }, 
                _toBottom.GetStyleLength().value.value, _duration)
                .SetEase(_ease,6).SetLoops(_looped ? -1 : 0,_loop)
                .SetDelay(Delay)
                .SetId(_idTween)
                .SetUpdate(_useUnscaledTime)
                .Play();
            }

        }

    }

}


#endif
