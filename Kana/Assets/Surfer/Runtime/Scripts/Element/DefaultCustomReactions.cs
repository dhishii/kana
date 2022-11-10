using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Surfer.SUActionData;
using Toggle = UnityEngine.UI.Toggle;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;
using Cursor = UnityEngine.Cursor;

#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;
#endif

namespace Surfer
{


    public static class DefaultCustomReactions
    {

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void GetUnifiedNames()
        {

            UnifiedNames = GetAllNames().Union(CustomReactions.GetAllNames()).ToArray();
            UnifiedCanvasNames = GetAllCanvasNames().Union(CustomReactionsExtensions.GetAllCanvasNames()).ToArray();
            UnifiedUIToolkitNames = GetAllUIToolkitNames().Union(CustomReactionsExtensions.GetAllUIToolkitNames()).ToArray();

        }
#endif

        public static string[] UnifiedNames { get; private set; } = default;
        public static string[] UnifiedCanvasNames { get; private set; } = default;
        public static string[] UnifiedUIToolkitNames { get; private set; } = default;

        public const string kSetAnchoredPosition = "_SUtr2";
        public const string kGODisable = "_SUgo2";
        public const string kGOEnable = "_SUgo3";
        public const string kOpenState = "_SUsu1";

        public readonly static Dictionary<string, PathAction> All = new Dictionary<string, PathAction>()
        {

#region Mixed 
            //canvas group
            {"_SUcg1",
            new PathAction("CanvasGroup/Alpha1",
            new List<PathField>()
            {
                new PathField("CanvasGroup",PathFieldType_ID.Object_1,typeof(CanvasGroup)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                (input.fieldsValues.Object_1 as CanvasGroup).alpha = 1;

            })},

            {"_SUcg2",
            new PathAction("CanvasGroup/Alpha0",
            new List<PathField>()
            {
                new PathField("CanvasGroup",PathFieldType_ID.Object_1,typeof(CanvasGroup)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as CanvasGroup).alpha = 0;

            })},

             {"_SUcg3",
            new PathAction("CanvasGroup/IgnoreParentGroupsOff",
            new List<PathField>()
            {
                new PathField("CanvasGroup",PathFieldType_ID.Object_1,typeof(CanvasGroup)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as CanvasGroup).ignoreParentGroups = false;

            })},

             {"_SUcg4",
            new PathAction("CanvasGroup/IgnoreParentGroupsOn",
            new List<PathField>()
            {
                new PathField("CanvasGroup",PathFieldType_ID.Object_1,typeof(CanvasGroup)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as CanvasGroup).ignoreParentGroups = true;

            })},



             {"_SUcg5",
            new PathAction("CanvasGroup/InteractableOff",
            new List<PathField>()
            {
                new PathField("CanvasGroup",PathFieldType_ID.Object_1,typeof(CanvasGroup)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as CanvasGroup).interactable = false;

            })},


             {"_SUcg6",
            new PathAction("CanvasGroup/InteractableOn",
            new List<PathField>()
            {
                new PathField("CanvasGroup",PathFieldType_ID.Object_1,typeof(CanvasGroup)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as CanvasGroup).interactable = true;

            })},


             {"_SUcg7",
            new PathAction("CanvasGroup/BlockRaycastOff",
            new List<PathField>()
            {
                new PathField("CanvasGroup",PathFieldType_ID.Object_1,typeof(CanvasGroup)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as CanvasGroup).blocksRaycasts = false;

            })},


            {"_SUcg8",
            new PathAction("CanvasGroup/BlockRaycastOn",
            new List<PathField>()
            {
                new PathField("CanvasGroup",PathFieldType_ID.Object_1,typeof(CanvasGroup)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as CanvasGroup).blocksRaycasts = true;

            })},


            {"_SUcg9",
            new PathAction("CanvasGroup/SetAlpha",
            new List<PathField>()
            {
                new PathField("CanvasGroup",PathFieldType_ID.Object_1,typeof(CanvasGroup)),
                new PathField("Value",PathFieldType_ID.Float_1)
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as CanvasGroup).alpha = input.fieldsValues.Float_1;

            })},



            //gameobject

            {"_SUgo1",
            new PathAction("GameObject/SetLayer",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
                new PathField("Layer",PathFieldType_ID.CustomChoices_1,SurferHelper.SO.Layers)
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as GameObject).layer
                = LayerMask.NameToLayer(SurferHelper.SO.Layers[input.fieldsValues.CustomChoices_1]);


            })},

            {kGODisable,
            new PathAction("GameObject/Disable",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as GameObject).SetActive(false);


            })},


            {kGOEnable,
            new PathAction("GameObject/Enable",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as GameObject).SetActive(true);


            })},


            {"_SUgo4",
            new PathAction("GameObject/SetTag",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
                new PathField("Tags",PathFieldType_ID.CustomChoices_1,SurferHelper.SO.Tags)

            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as GameObject).tag
                = SurferHelper.SO.Tags[input.fieldsValues.CustomChoices_1];


            })},




            {"_SUgo5",
            new PathAction("GameObject/SetName",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
                new PathField("Name",PathFieldType_ID.String_1)

            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as GameObject).name =
                input.fieldsValues.String_1;

            })},



            //selectable

            {"_SUse1",
            new PathAction("Selectable/InteractableOff",
            new List<PathField>()
            {
                new PathField("Selectable",PathFieldType_ID.Object_1,typeof(Selectable)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as Selectable).interactable = false ;


            },SUCompatibility_ID.Canvas )},


            {"_SUse2",
            new PathAction("Selectable/InteractableOn",
            new List<PathField>()
            {
                new PathField("Selectable",PathFieldType_ID.Object_1,typeof(Selectable)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as Selectable).interactable = true ;


            },SUCompatibility_ID.Canvas )},


            //graphic

            {"_SUgr1",
            new PathAction("Graphic/RaycastOn",
            new List<PathField>()
            {
                new PathField("Graphic",PathFieldType_ID.Object_1,typeof(Graphic)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as Graphic).raycastTarget = true;


            },SUCompatibility_ID.Canvas )},

            {"_SUgr2",
            new PathAction("Graphic/RaycastOff",
            new List<PathField>()
            {
                new PathField("Graphic",PathFieldType_ID.Object_1,typeof(Graphic)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as Graphic).raycastTarget = false;


            },SUCompatibility_ID.Canvas )},

            {"_SUgr3",
            new PathAction("Graphic/SetColor",
            new List<PathField>()
            {
                new PathField("Graphic",PathFieldType_ID.Object_1,typeof(Graphic)),
                new PathField("Color",PathFieldType_ID.Color_1),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                (input.fieldsValues.Object_1 as Graphic).color = input.fieldsValues.Color_1;


            },SUCompatibility_ID.Canvas )},


            //ui generics
            {"_SUui1",
            new PathAction("UIGenerics/FocusOnObject",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                SUEventSystemManager.I.FocusOnObject((input.fieldsValues.Object_1 as GameObject));

            },SUCompatibility_ID.Canvas )},


            {"_SUui2",
            new PathAction("UIGenerics/FocusOnObjectOrStateLast",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;


                    SUEventSystemManager.I.FocusOnObjectOrLast((input.fieldsValues.Object_1 as GameObject));

            },SUCompatibility_ID.Canvas )},


            {"_SUui3",
            new PathAction("UIGenerics/EnableClickConstraint",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                var cg = obj.GetComponent<CanvasGroup>();

                    if (cg == null)
                        cg = obj.AddComponent<CanvasGroup>();

                    cg.interactable = true;
                    cg.blocksRaycasts = true;
                    cg.ignoreParentGroups = true;

                    var stateTf = obj.GetObjectStateTransfom();

                    cg = stateTf.GetComponent<CanvasGroup>();

                    if (cg == null)
                        cg = stateTf.gameObject.AddComponent<CanvasGroup>();


                    cg.interactable = false;
                    cg.blocksRaycasts = true;
                    cg.ignoreParentGroups = true;

            },SUCompatibility_ID.Canvas )},


             {"_SUui4",
            new PathAction("UIGenerics/DisableClickConstraint",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                var cgg = obj.GetComponent<CanvasGroup>();

                if (cgg == null)
                    return;

                cgg.ignoreParentGroups = false;

                var stateTf = obj.GetObjectStateTransfom();

                cgg = stateTf.GetComponent<CanvasGroup>();

                if (cgg == null)
                    return;

                cgg.interactable = true;
                cgg.blocksRaycasts = true;

            },SUCompatibility_ID.Canvas )},


            //audio

            {"_SUau1",
            new PathAction("Audio/PlayClip",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
                new PathField("AudioClip",PathFieldType_ID.Object_2,typeof(AudioClip)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;
                if(input.fieldsValues.Object_2 == null)
                return;

                SurferHelper.PlaySound((input.fieldsValues.Object_2 as AudioClip),
                (input.fieldsValues.Object_1 as GameObject));


            })},

            {"_SUau2",
            new PathAction("Audio/PlaySource",
            new List<PathField>()
            {
                new PathField("AudioSource",PathFieldType_ID.Object_1,typeof(AudioSource)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                (input.fieldsValues.Object_1 as AudioSource).Play();

            })},


            //toggle

            {"_SUto1",
            new PathAction("Toggle/True",
            new List<PathField>()
            {
                new PathField("Toggle",PathFieldType_ID.Object_1,typeof(Toggle)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                (input.fieldsValues.Object_1 as Toggle).isOn = true;

            },SUCompatibility_ID.Canvas )},


             {"_SUto2",
            new PathAction("Toggle/False",
            new List<PathField>()
            {
                new PathField("Toggle",PathFieldType_ID.Object_1,typeof(Toggle)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                (input.fieldsValues.Object_1 as Toggle).isOn = false;

            },SUCompatibility_ID.Canvas )},

             {"_SUto3",
            new PathAction("Toggle/ToggleValue",
            new List<PathField>()
            {
                new PathField("Toggle",PathFieldType_ID.Object_1,typeof(Toggle)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                (input.fieldsValues.Object_1 as Toggle).isOn =
                !(input.fieldsValues.Object_1 as Toggle).isOn;

            },SUCompatibility_ID.Canvas )},


            //textmeshpro

             {"_SUte1",
            new PathAction("Text/Empty",
            new List<PathField>()
            {
                new PathField("Tmp",PathFieldType_ID.Object_1,typeof(TextMeshProUGUI)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                (input.fieldsValues.Object_1 as TextMeshProUGUI).text = string.Empty;

            },SUCompatibility_ID.Canvas )},

             {"_SUte2",
            new PathAction("Text/SetText",
            new List<PathField>()
            {
                new PathField("Tmp",PathFieldType_ID.Object_1,typeof(TextMeshProUGUI)),
                new PathField("Value",PathFieldType_ID.String_1)
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                (input.fieldsValues.Object_1 as TextMeshProUGUI).text =
                input.fieldsValues.String_1;

            },SUCompatibility_ID.Canvas )},

            
            //inputfield

             {"_SUin1",
            new PathAction("InputField/Empty",
            new List<PathField>()
            {
                new PathField("InputField",PathFieldType_ID.Object_1,typeof(TMP_InputField)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                (input.fieldsValues.Object_1 as TMP_InputField).text = string.Empty;

            },SUCompatibility_ID.Canvas )},

             {"_SUin2",
            new PathAction("InputField/SetText",
            new List<PathField>()
            {
                new PathField("InputField",PathFieldType_ID.Object_1,typeof(TMP_InputField)),
                new PathField("Value",PathFieldType_ID.String_1)
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                (input.fieldsValues.Object_1 as TMP_InputField).text =
                input.fieldsValues.String_1;

            },SUCompatibility_ID.Canvas )},

            //animations

             {"_SUan1",
            new PathAction("Animations/StopCanvasGroup",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.CGroupPrefix + obj.transform.GetInstanceID());


            },SUCompatibility_ID.Canvas )},



             {"_SUan2",
            new PathAction("Animations/StopColor",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.ColorPrefix + obj.transform.GetInstanceID());


            },SUCompatibility_ID.Canvas )},


             {"_SUan3",
            new PathAction("Animations/StopJump",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.JumpPrefix + obj.transform.GetInstanceID());


            },SUCompatibility_ID.Canvas )},



             {"_SUan4",
            new PathAction("Animations/StopPosition",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.PositionPrefix + obj.transform.GetInstanceID());


            },SUCompatibility_ID.Canvas )},


             {"_SUan5",
            new PathAction("Animations/StopPunch",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.PunchPrefix + obj.transform.GetInstanceID());


            },SUCompatibility_ID.Canvas )},


             {"_SUan6",
            new PathAction("Animations/StopRectSize",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.RectSizePrefix + obj.transform.GetInstanceID());


            },SUCompatibility_ID.Canvas )},


             {"_SUan7",
            new PathAction("Animations/StopRotation",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.RotationPrefix + obj.transform.GetInstanceID());


            },SUCompatibility_ID.Canvas )},


             {"_SUan8",
            new PathAction("Animations/StopScale",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.ScalePrefix + obj.transform.GetInstanceID());


            },SUCompatibility_ID.Canvas )},


             {"_SUan9",
            new PathAction("Animations/StopShake",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.ShakePrefix + obj.transform.GetInstanceID());


            },SUCompatibility_ID.Canvas )},


             {"_SUan10",
            new PathAction("Animations/StopCharTweener",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.CharTweenPrefix + obj.transform.GetInstanceID());


            },SUCompatibility_ID.Canvas )},


            //transform

             {"_SUtr1",
            new PathAction("Transform/SetLocalPosition",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
                new PathField("Position",PathFieldType_ID.Vector3_1,typeof(Vector3)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.PositionPrefix + obj.transform.GetInstanceID());
                    obj.transform.localPosition = (Vector3)input.fieldsValues.Vector3_1;

            })},


             {kSetAnchoredPosition,
            new PathAction("Transform/SetAnchoredPosition",
            new List<PathField>()
            {
                new PathField("RectTransform",PathFieldType_ID.Object_1,typeof(RectTransform)),
                new PathField("Position",PathFieldType_ID.Vector3_1,typeof(Vector3)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var recT = input.fieldsValues.Object_1 as RectTransform;


                DOTween.Kill(SUAnimationData.PositionPrefix + recT.transform.GetInstanceID());

                recT.anchoredPosition = (Vector3)input.fieldsValues.Vector3_1;

            })},


             {"_SUtr3",
            new PathAction("Transform/SetLocalEuler",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
                new PathField("Rotation",PathFieldType_ID.Vector3_1,typeof(Vector3)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.RotationPrefix + obj.transform.GetInstanceID());
                obj.transform.localEulerAngles = (Vector3)input.fieldsValues.Vector3_1;

            })},


             {"_SUtr4",
            new PathAction("Transform/SetLocalScale",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
                new PathField("Scale",PathFieldType_ID.Vector3_1,typeof(Vector3)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                DOTween.Kill(SUAnimationData.ScalePrefix + obj.transform.GetInstanceID());
                obj.transform.localScale = (Vector3)input.fieldsValues.Vector3_1;
            })},


             {"_SUtr5",
            new PathAction("Transform/SetSizeDelta",
            new List<PathField>()
            {
                new PathField("RectTransform",PathFieldType_ID.Object_1,typeof(RectTransform)),
                new PathField("Size",PathFieldType_ID.Vector2_1,typeof(Vector2)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var recT = input.fieldsValues.Object_1 as RectTransform;


                DOTween.Kill(SUAnimationData.RectSizePrefix + recT.transform.GetInstanceID());

                recT.sizeDelta = (Vector2)input.fieldsValues.Vector2_1;

            })},


             {"_SUtr6",
            new PathAction("Transform/JustMoveIn",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                obj.transform.localPosition = Vector3.zero;


            })},


             {"_SUtr7",
            new PathAction("Transform/JustMoveOut",
            new List<PathField>()
            {
                new PathField("GameObject",PathFieldType_ID.Object_1,typeof(GameObject)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as GameObject;

                obj.transform.localPosition = new Vector3(0,-10_000,0);

            })},


            //state

             {"_SUs1",
            new PathAction("State/ResetMyStateHistoryFocus",
            (input)=>
            {
                var eleData = input.gameObj.GetObjectStateElementData();

                if (eleData == null)
                    return;

                SUEventSystemManager.I.ResetStateHistoryFocus(eleData.PlayerID,eleData.StateName);

            },SUCompatibility_ID.Canvas )},


            //image
              {"_SUim1",
            new PathAction("Image/SetSprite",
            new List<PathField>()
            {
                new PathField("Image",PathFieldType_ID.Object_1,typeof(Image)),
                new PathField("Sprite",PathFieldType_ID.Object_2,typeof(Sprite)),
            },
            (input)=>
            {
                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as Image;

                obj.sprite = input.fieldsValues.Object_2 as Sprite;

            },SUCompatibility_ID.Canvas )},


            //dropdown
              {"_SUdr1",
            new PathAction("Dropdown/SelectOption",
            new List<PathField>()
            {
                new PathField("Dropdown",PathFieldType_ID.Object_1,typeof(TMP_Dropdown)),
                new PathField("Index",PathFieldType_ID.Int_1),
            },
            (input)=>
            {

                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as TMP_Dropdown;

                obj.value =  input.fieldsValues.Int_1;

            },SUCompatibility_ID.Canvas )},


            //slider
              {"_SUsl1",
            new PathAction("Slider/SetValue",
            new List<PathField>()
            {
                new PathField("Slider",PathFieldType_ID.Object_1,typeof(Slider)),
                new PathField("Value",PathFieldType_ID.Float_1),
            },
            (input)=>
            {

                if(input.fieldsValues.Object_1 == null)
                return;

                var obj = input.fieldsValues.Object_1 as Slider;

                obj.value =  input.fieldsValues.Float_1;

            },SUCompatibility_ID.Canvas )},


            //application
              {"_SUap1",
            new PathAction("Application/OpenURL",
            new List<PathField>()
            {
                new PathField("Url",PathFieldType_ID.String_1),
            },
            (input)=>
            {

                Application.OpenURL(input.fieldsValues.String_1);

            })},


            //cursor
             {"_SUcu1",
            new PathAction("Cursor/SetDefaultIcon",
            (input)=>
            {

                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);


            },SUCompatibility_ID.Canvas )},


             {"_SUcu2",
            new PathAction("Cursor/SetIcon",
            new List<PathField>()
            {
                new PathField("Texture",PathFieldType_ID.Object_1,typeof(Texture2D)),
            },
            (input)=>
            {

                if(input.fieldsValues.Object_1 == null)
                return;

                var tex = input.fieldsValues.Object_1 as Texture2D;

                Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);

            },SUCompatibility_ID.Canvas )},


             {"_SUcu3",
            new PathAction("Cursor/SetIconCentered",
            new List<PathField>()
            {
                new PathField("Texture",PathFieldType_ID.Object_1,typeof(Texture2D)),
            },
            (input)=>
            {

                if(input.fieldsValues.Object_1 == null)
                return;

                var tex = input.fieldsValues.Object_1 as Texture2D;

                Cursor.SetCursor(tex, new Vector2(tex.width/2f,tex.height/2f), CursorMode.Auto);

            },SUCompatibility_ID.Canvas )},



            //canvas
             {"_SUca1",
            new PathAction("Canvas/BringToFront",
            new List<PathField>()
            {
                new PathField("Canvas",PathFieldType_ID.Object_1,typeof(Canvas)),
            },
            (input)=>
            {

                if(input.fieldsValues.Object_1 == null)
                return;

                var canvas = input.fieldsValues.Object_1 as Canvas;
                var canvases = GameObject.FindObjectsOfType<Canvas>();

                int highestOrder = -40000;

                for(int i=0;i<canvases.Length;i++)
                {
                    highestOrder = Mathf.Max(highestOrder,canvases[i].sortingOrder);
                }

                highestOrder += 1;

                canvas.overrideSorting = true;
                canvas.sortingOrder = highestOrder;

            })},


             {"_SUca2",
            new PathAction("Canvas/SendToBack",
            new List<PathField>()
            {
                new PathField("Canvas",PathFieldType_ID.Object_1,typeof(Canvas)),
            },
            (input)=>
            {

                if(input.fieldsValues.Object_1 == null)
                return;

                var canvas = input.fieldsValues.Object_1 as Canvas;
                var allCanvases = GameObject.FindObjectsOfType<Canvas>();

                int lowestOrder = 40000;

                for (int i = 0; i < allCanvases.Length; i++)
                {
                    lowestOrder = Mathf.Min(lowestOrder, allCanvases[i].sortingOrder);
                }

                lowestOrder -= 1;

                canvas.overrideSorting = true;
                canvas.sortingOrder = lowestOrder;


            })},



            //playerprefs
             {"_SUpl1",
            new PathAction("PlayerPrefs/DeleteAll",
            (input)=>
            {

                PlayerPrefs.DeleteAll();
            })},

             {"_SUpl2",
            new PathAction("PlayerPrefs/DeleteKey",
            new List<PathField>()
            {
                new PathField("Key",PathFieldType_ID.String_1),
            },
            (input)=>
            {

                PlayerPrefs.DeleteKey(input.fieldsValues.String_1);
            })},


             {"_SUpl3",
            new PathAction("PlayerPrefs/SetFloat",
            new List<PathField>()
            {
                new PathField("Key",PathFieldType_ID.String_1),
                new PathField("Value",PathFieldType_ID.Float_1),
            },
            (input)=>
            {

                PlayerPrefs.SetFloat(input.fieldsValues.String_1,input.fieldsValues.Float_1);
            })},


             {"_SUpl4",
            new PathAction("PlayerPrefs/SetInt",
            new List<PathField>()
            {
                new PathField("Key",PathFieldType_ID.String_1),
                new PathField("Value",PathFieldType_ID.Int_1),
            },
            (input)=>
            {

                PlayerPrefs.SetInt(input.fieldsValues.String_1,input.fieldsValues.Int_1);
            })},


            {"_SUpl5",
            new PathAction("PlayerPrefs/SetString",
            new List<PathField>()
            {
                new PathField("Key",PathFieldType_ID.String_1),
                new PathField("Value",PathFieldType_ID.String_2),
            },
            (input)=>
            {

                PlayerPrefs.SetString(input.fieldsValues.String_1,input.fieldsValues.String_2);
            })},



            //animator

             {"_SUant1",
            new PathAction("Animator/PlayState",
            new List<PathField>()
            {
                new PathField("Animator",PathFieldType_ID.Object_1,typeof(Animator)),
                new PathField("State",PathFieldType_ID.String_1),
            },
            (input)=>
            {

                if(input.fieldsValues.Object_1 == null)
                return;

                var animator = input.fieldsValues.Object_1 as Animator;

                animator.Play(input.fieldsValues.String_1);

            })},


             {"_SUant2",
            new PathAction("Animator/Stop",
            new List<PathField>()
            {
                new PathField("Animator",PathFieldType_ID.Object_1,typeof(Animator)),
            },
            (input)=>
            {

                if(input.fieldsValues.Object_1 == null)
                return;

                var animator = input.fieldsValues.Object_1 as Animator;

                animator.StopPlayback();

            })},


            //tween animations

            {"_SUatw1",
            new PathAction("Animations/Position",
            new List<PathField>()
            {
                new PathField("Position","_position"),
            },
            (input)=>
            {

                input.reactionData.PositionAnim.Play(input.gameObj);

            },SUCompatibility_ID.Canvas )},


             {"_SUatw2",
            new PathAction("Animations/AnchoredPosition",
            new List<PathField>()
            {
                new PathField("AnchoredPosition","_anchoredPosition"),
            },
            (input)=>
            {

                input.reactionData.AnchoredPositionAnim.Play(input.gameObj);

            },SUCompatibility_ID.Canvas )},


             {"_SUatw3",
            new PathAction("Animations/Rotation",
            new List<PathField>()
            {
                new PathField("Rotation","_rotation"),
            },
            (input)=>
            {

                input.reactionData.RotationAnim.Play(input.gameObj);

            },SUCompatibility_ID.Canvas )},


             {"_SUatw4",
            new PathAction("Animations/Scale",
            new List<PathField>()
            {
                new PathField("Scale","_scale"),
            },
            (input)=>
            {

                input.reactionData.ScaleAnim.Play(input.gameObj);

            },SUCompatibility_ID.Canvas )},


             {"_SUatw5",
            new PathAction("Animations/RectSize",
            new List<PathField>()
            {
                new PathField("RectSize","_rectSize"),
            },
            (input)=>
            {

                input.reactionData.RectSizeAnim.Play(input.gameObj);

            },SUCompatibility_ID.Canvas )},


             {"_SUatw6",
            new PathAction("Animations/Color",
            new List<PathField>()
            {
                new PathField("Color","_color"),
            },
            (input)=>
            {

                input.reactionData.ColorAnim.Play(input.gameObj);

            },SUCompatibility_ID.Canvas )},


             {"_SUatw7",
            new PathAction("Animations/Shake",
            new List<PathField>()
            {
                new PathField("Shake","_shake"),
            },
            (input)=>
            {

                input.reactionData.ShakeAnim.Play(input.gameObj);

            },SUCompatibility_ID.Canvas )},


             {"_SUatw8",
            new PathAction("Animations/Jump",
            new List<PathField>()
            {
                new PathField("Jump","_jump"),
            },
            (input)=>
            {

                input.reactionData.JumpAnim.Play(input.gameObj);

            },SUCompatibility_ID.Canvas )},


             {"_SUatw9",
            new PathAction("Animations/Punch",
            new List<PathField>()
            {
                new PathField("Punch","_punch"),
            },
            (input)=>
            {

                input.reactionData.PunchAnim.Play(input.gameObj);

            },SUCompatibility_ID.Canvas )},


             {"_SUatw10",
            new PathAction("Animations/CGroup",
            new List<PathField>()
            {
                new PathField("CanvasGroup","_canvasGroup"),
            },
            (input)=>
            {

                input.reactionData.CanvasGroupAnim.Play(input.gameObj);

            },SUCompatibility_ID.Canvas )},


             {"_SUatw11",
            new PathAction("Animations/CharTween",
            new List<PathField>()
            {
                new PathField("CharTween","_charTweener"),
            },
            (input)=>
            {

                input.reactionData.CharTweenerAnim.Play(input.gameObj);

            },SUCompatibility_ID.Canvas )},


            //surfer actions
            
            {kOpenState,
            new PathAction("Surfer/OpenState",
            new List<PathField>()
            {
                new PathField("State","_stateData"),
                new PathField("Version",PathFieldType_ID.Int_1),
                new PathField("PlayerID",PathFieldType_ID.Int_2),
                new PathField("Delay",PathFieldType_ID.Float_1),
                new PathField("Play Sound",PathFieldType_ID.Object_1,typeof(AudioClip))

            },
            (input)=>
            {


                SurferManager.I.OpenPlayerState(GetPlayerID(input),
                input.reactionData.StateData.Name,
                input.fieldsValues.Int_1,
                input.fieldsValues.Float_1);


                SurferHelper.PlaySound((input.fieldsValues.Object_1 as AudioClip),
                input.gameObj,
                input.fieldsValues.Float_1);

            })},


            {"_SUsu2",
            new PathAction("Surfer/OpenPrefabState",
            new List<PathField>()
            {
                new PathField("Parent",PathFieldType_ID.Enum_1,typeof(SUActionData.SUPrefabParent_ID)),
                new PathField("Prefab",PathFieldType_ID.Object_2,typeof(GameObject)),
                new PathField("Version",PathFieldType_ID.Int_1),
                new PathField("Delay",PathFieldType_ID.Float_1),
                new PathField("Play Sound",PathFieldType_ID.Object_1,typeof(AudioClip))
            },
            (input)=>
            {

                var prefab = input.fieldsValues.Object_2 as GameObject;

                if(prefab == null)
                return;

                var delay = input.fieldsValues.Float_1;
                var version = input.fieldsValues.Int_1;
                var parentMode = (SUPrefabParent_ID)input.fieldsValues.Enum_1;

                if(parentMode == SUPrefabParent_ID.Scene)
                    SurferManager.I.OpenPrefabState(prefab,null, version, delay);
                else if(parentMode == SUPrefabParent_ID.RootCanvas)
                    SurferManager.I.OpenPrefabState(prefab,input.gameObj.GetComponentInParent<Canvas>().rootCanvas.transform, version, delay);
                else if (parentMode == SUPrefabParent_ID.ThisState)
                    SurferManager.I.OpenPrefabState(prefab, input.gameObj.GetObjectStateTransfom(), version, delay);
                else if (parentMode == SUPrefabParent_ID.ThisStateParent)
                    SurferManager.I.OpenPrefabState(prefab, input.gameObj.GetObjectStateTransfom().parent.gameObject.GetObjectStateTransfom(), version, delay);



                SurferHelper.PlaySound((input.fieldsValues.Object_1 as AudioClip),
                input.gameObj,
                input.fieldsValues.Float_1);

            },SUCompatibility_ID.Canvas )},



            {"_SUsu3",
            new PathAction("Surfer/CloseState",
            new List<PathField>()
            {
                new PathField("State","_stateData"),
                new PathField("Version",PathFieldType_ID.Int_1),
                new PathField("PlayerID",PathFieldType_ID.Int_2),
                new PathField("Delay",PathFieldType_ID.Float_1),
                new PathField("Play Sound",PathFieldType_ID.Object_1,typeof(AudioClip))

            },
            (input)=>
            {

                SurferManager.I.ClosePlayerState(GetPlayerID(input),
                input.reactionData.StateData.Name,
                input.fieldsValues.Int_1,
                input.fieldsValues.Float_1);


                SurferHelper.PlaySound((input.fieldsValues.Object_1 as AudioClip),
                input.gameObj,
                input.fieldsValues.Float_1);

            })},



            {"_SUsu4",
            new PathAction("Surfer/ToggleState",
            new List<PathField>()
            {
                new PathField("State","_stateData"),
                new PathField("Version",PathFieldType_ID.Int_1),
                new PathField("PlayerID",PathFieldType_ID.Int_2),
                new PathField("Delay",PathFieldType_ID.Float_1),
                new PathField("Play Sound",PathFieldType_ID.Object_1,typeof(AudioClip))

            },
            (input)=>
            {

                SurferManager.I.TogglePlayerState(GetPlayerID(input),
                input.reactionData.StateData.Name,
                input.fieldsValues.Int_1,
                input.fieldsValues.Float_1);


                SurferHelper.PlaySound((input.fieldsValues.Object_1 as AudioClip),
                input.gameObj,
                input.fieldsValues.Float_1);

            })},



            {"_SUsu5",
            new PathAction("Surfer/LoadScene",
            new List<PathField>()
            {
                new PathField("Scene","_sceneData"),
                new PathField("Additive",PathFieldType_ID.Bool_1),
                new PathField("Overlay State","_stateData"),
                new PathField("Version",PathFieldType_ID.Int_1),
                new PathField("PlayerID",PathFieldType_ID.Int_2),
                new PathField("Delay",PathFieldType_ID.Float_1),
                new PathField("Play Sound",PathFieldType_ID.Object_1,typeof(AudioClip))

            },
            (input)=>
            {

                SurferManager.I.OpenPlayerState(GetPlayerID(input),
                input.reactionData.StateData.Name,
                input.fieldsValues.Int_1,
                input.fieldsValues.Float_1);

                SurferManager.I.LoadScene(input.reactionData.SceneData.Name,
                input.fieldsValues.Float_1,
                input.fieldsValues.Bool_1);


                SurferHelper.PlaySound((input.fieldsValues.Object_1 as AudioClip),
                input.gameObj,
                input.fieldsValues.Float_1);

            })},



            {"_SUsu6",
            new PathAction("Surfer/LoadSceneAsync",
            new List<PathField>()
            {
                new PathField("Scene","_sceneData"),
                new PathField("Additive",PathFieldType_ID.Bool_1),
                new PathField("AutoActivation",PathFieldType_ID.Bool_2),
                new PathField("Overlay State","_stateData"),
                new PathField("Version",PathFieldType_ID.Int_1),
                new PathField("PlayerID",PathFieldType_ID.Int_2),
                new PathField("Delay",PathFieldType_ID.Float_1),
                new PathField("Play Sound",PathFieldType_ID.Object_1,typeof(AudioClip))

            },
            (input)=>
            {


                SurferManager.I.OpenPlayerState(GetPlayerID(input),
                input.reactionData.StateData.Name,
                input.fieldsValues.Int_1,
                input.fieldsValues.Float_1);

                SurferManager.I.LoadSceneAsync(input.reactionData.SceneData.Name,
                input.fieldsValues.Float_1,
                input.fieldsValues.Bool_1,
                input.fieldsValues.Bool_2);


                SurferHelper.PlaySound((input.fieldsValues.Object_1 as AudioClip),
                input.gameObj,
                input.fieldsValues.Float_1);

            },SUCompatibility_ID.Both)},


            {"_SUsu7",
            new PathAction("Surfer/UnloadSceneAsync",
            new List<PathField>()
            {
                new PathField("Scene","_sceneData"),
                new PathField("Overlay State","_stateData"),
                new PathField("Version",PathFieldType_ID.Int_1),
                new PathField("PlayerID",PathFieldType_ID.Int_2),
                new PathField("Delay",PathFieldType_ID.Float_1),
                new PathField("Play Sound",PathFieldType_ID.Object_1,typeof(AudioClip))

            },
            (input)=>
            {

                SurferManager.I.OpenPlayerState(GetPlayerID(input),
                input.reactionData.StateData.Name,
                input.fieldsValues.Int_1,
                input.fieldsValues.Float_1);

                SurferManager.I.UnloadSceneAsync(input.reactionData.SceneData.Name,
                input.fieldsValues.Float_1);


                SurferHelper.PlaySound((input.fieldsValues.Object_1 as AudioClip),
                input.gameObj,
                input.fieldsValues.Float_1);

            })},


            {"_SUsu8",
            new PathAction("Surfer/SetActiveScene",
            new List<PathField>()
            {
                new PathField("Scene","_sceneData"),
                new PathField("Overlay State","_stateData"),
                new PathField("Version",PathFieldType_ID.Int_1),
                new PathField("PlayerID",PathFieldType_ID.Int_2),
                new PathField("Delay",PathFieldType_ID.Float_1),
                new PathField("Play Sound",PathFieldType_ID.Object_1,typeof(AudioClip))

            },
            (input)=>
            {

                SurferManager.I.OpenPlayerState(GetPlayerID(input),
                input.reactionData.StateData.Name,
                input.fieldsValues.Int_1,
                input.fieldsValues.Float_1);

                SurferManager.I.SetActiveScene(input.reactionData.SceneData.Name,
                input.fieldsValues.Float_1);


                SurferHelper.PlaySound((input.fieldsValues.Object_1 as AudioClip),
                input.gameObj,
                input.fieldsValues.Float_1);

            })},


            {"_SUsu9",
            new PathAction("Surfer/CloseMyState",
            new List<PathField>()
            {
                new PathField("Delay",PathFieldType_ID.Float_1),
                new PathField("Play Sound",PathFieldType_ID.Object_1,typeof(AudioClip))

            },
            (input)=>
            {
                if (input.elementData.IsToolkit)
                {

#if UNITY_2021_2_OR_NEWER

                    SurferManager.I.ClosePlayerState(input.visualElement.GetVElePlayerID(), input.visualElement.GetVEleStateName(), 0,input.fieldsValues.Float_1);

#endif
                }
                else
                {
                    SurferManager.I.ClosePlayerState(input.gameObj.GetObjectStatePlayerID(true), input.gameObj.GetObjectStateName(true), 0,input.fieldsValues.Float_1);
                }

                SurferHelper.PlaySound((input.fieldsValues.Object_1 as AudioClip),
                input.gameObj,
                input.fieldsValues.Float_1);

            })},



            {"_SUsu10",
            new PathAction("Surfer/SendCustomEvent",
            new List<PathField>()
            {
                new PathField("CustomEvent","_customEData"),
                new PathField("Delay",PathFieldType_ID.Float_1),
                new PathField("Play Sound",PathFieldType_ID.Object_1,typeof(AudioClip))

            },
            (input)=>
            {

                SurferManager.I.SendCustomEvent(input.reactionData.CustomEData.Name,input.fieldsValues.Float_1,null);

                SurferHelper.PlaySound((input.fieldsValues.Object_1 as AudioClip),
                input.gameObj,
                input.fieldsValues.Float_1);

            })},

#endregion


#region UIToolkit Only

#if UNITY_2021_2_OR_NEWER
            
            //visual element
            {"_SUtkve1",
            new PathAction("VisualElement/Set Opacity",
            new List<PathField>()
            {
                new PathField("Who","_query"),
                new PathField("Opacity",PathFieldType_ID.Float_1),
            },
            (input)=>
            {
                input.GetQueryElements((element)=>
                {
                    element.style.opacity = input.fieldsValues.Float_1;
                });
            },SUCompatibility_ID.UIToolkit )},

             {"_SUtkve2",
            new PathAction("VisualElement/Set Display",
            new List<PathField>()
            {
                new PathField("Who","_query"),
                new PathField("Display",PathFieldType_ID.Enum_1,typeof(DisplayStyle)),
            },
            (input)=>
            {

                input.GetQueryElements((element)=>
                {
                    element.style.display = (DisplayStyle)input.fieldsValues.Enum_1;
                });

            },SUCompatibility_ID.UIToolkit )},

             {"_SUtkve3",
            new PathAction("VisualElement/Set Visibility",
            new List<PathField>()
            {
                new PathField("Who","_query"),
                new PathField("Visibility",PathFieldType_ID.Enum_1,typeof(Visibility)),
            },
            (input)=>
            {
                input.GetQueryElements((element)=>
                {
                    element.style.visibility= (Visibility)input.fieldsValues.Enum_1;
                });

            },SUCompatibility_ID.UIToolkit )},


             {"_SUtkve4",
            new PathAction("VisualElement/Set Enabled",
            new List<PathField>()
            {
                new PathField("Who","_query"),
                new PathField("Enabled",PathFieldType_ID.Bool_1),
            },
            (input)=>
            {

                input.GetQueryElements((element)=>
                {
                    element.SetEnabled(input.fieldsValues.Bool_1);
                });

            },SUCompatibility_ID.UIToolkit )},

             {"_SUtkve5",
            new PathAction("VisualElement/Set Direction",
            new List<PathField>()
            {
                new PathField("Who","_query"),
                new PathField("Direction",PathFieldType_ID.Enum_1,typeof(FlexDirection)),
            },
            (input)=>
            {

                input.GetQueryElements((element)=>
                {
                    element.style.flexDirection = (FlexDirection)input.fieldsValues.Enum_1;
                });

            },SUCompatibility_ID.UIToolkit )},

             {"_SUtkve6",
            new PathAction("VisualElement/Align Items",
            new List<PathField>()
            {
                new PathField("Who","_query"),
                new PathField("Alignment",PathFieldType_ID.Enum_1,typeof(Align)),
            },
            (input)=>
            {
                input.GetQueryElements((element)=>
                {
                    element.style.alignItems = (Align)input.fieldsValues.Enum_1;
                });
            },SUCompatibility_ID.UIToolkit )},

             {"_SUtkve7",
            new PathAction("VisualElement/Justify Content",
            new List<PathField>()
            {
                new PathField("Who","_query"),
                new PathField("Justify",PathFieldType_ID.Enum_1,typeof(Justify)),
            },
            (input)=>
            {
                input.GetQueryElements((element)=>
                {
                    element.style.justifyContent = (Justify)input.fieldsValues.Enum_1;
                });
            },SUCompatibility_ID.UIToolkit )},

             {"_SUtkve8",
            new PathAction("VisualElement/Set Background Color",
            new List<PathField>()
            {
                new PathField("Who","_query"),
                new PathField("Color",PathFieldType_ID.Color_1),
            },
            (input)=>
            {
                input.GetQueryElements((element)=>
                {
                    element.style.backgroundColor = input.fieldsValues.Color_1;
                });
            },SUCompatibility_ID.UIToolkit )},

            {"_SUtkve9",
            new PathAction("VisualElement/Set Background Sprite",
            new List<PathField>()
            {
                new PathField("Who","_query"),
                new PathField("Sprite",PathFieldType_ID.Object_1,typeof(Sprite))
            },
            (input)=>
            {
                input.GetQueryElements((element)=>
                {
                    element.style.backgroundImage = Background.FromSprite(input.fieldsValues.Object_1 as Sprite);
                });
            },SUCompatibility_ID.UIToolkit )},

            {"_SUtkve10",
            new PathAction("VisualElement/Focus On Element",
            new List<PathField>()
            {
                new PathField("Who","_query"),
            },
            (input)=>
            {
                input.GetQueryElements((element)=>
                {
                    element.Focus();
                });
            },SUCompatibility_ID.UIToolkit )},

            //ui document
            {"_SUtkdoc1",
            new PathAction("UI Document/BringToFront",
            new List<PathField>()
            {
                new PathField("Document",PathFieldType_ID.Object_1,typeof(UIDocument))
            },
            (input)=>
            {
                var doc = input.fieldsValues.Object_1 as UIDocument;

                if (doc == null)
                return;

                var allDoc = GameObject.FindObjectsOfType<UIDocument>();
                float maxOrder = default;

                for(int i=0;i<allDoc.Length;i++)
                {
                    maxOrder = Mathf.Max(maxOrder,allDoc[i].sortingOrder);
                }

                doc.sortingOrder = maxOrder+1;

            },SUCompatibility_ID.UIToolkit )},

            {"_SUtkdoc2",
            new PathAction("UI Document/SendToBack",
            new List<PathField>()
            {
                new PathField("Document",PathFieldType_ID.Object_1,typeof(UIDocument))
            },
            (input)=>
            {
                var doc = input.fieldsValues.Object_1 as UIDocument;

                if (doc == null)
                return;

                var allDoc = GameObject.FindObjectsOfType<UIDocument>();
                float minOrder = default;

                for(int i=0;i<allDoc.Length;i++)
                {
                    minOrder = Mathf.Min(minOrder,allDoc[i].sortingOrder);
                }

                doc.sortingOrder = minOrder-1;

            },SUCompatibility_ID.UIToolkit )},

            {"_SUtkanim1",
            new PathAction("Animations/Position",
            new List<PathField>()
            {
                new PathField("Position","_tkPosition"),
                new PathField("Who","_query"),
            },
            (input)=>
            {
                
                input.GetQueryElements((element)=>
                {
                    input.reactionData.TkPosition.Play(input.gameObj,element);
                });

            },SUCompatibility_ID.UIToolkit )},

            {"_SUtkanim2",
            new PathAction("Animations/Scale",
            new List<PathField>()
            {
                new PathField("Scale","_tkScale"),
                new PathField("Who","_query"),
            },
            (input)=>
            {

                input.GetQueryElements((element)=>
                {
                    input.reactionData.TkScale.Play(input.gameObj,element);
                });

            },SUCompatibility_ID.UIToolkit )},

            {"_SUtkanim3",
            new PathAction("Animations/Rotation",
            new List<PathField>()
            {
                new PathField("Rotation","_tkRotation"),
                new PathField("Who","_query"),
            },
            (input)=>
            {

                input.GetQueryElements((element)=>
                {
                    input.reactionData.TkRotation.Play(input.gameObj,element);
                });

            },SUCompatibility_ID.UIToolkit )},

            
            {"_SUtkanim4",
            new PathAction("Animations/Background Color",
            new List<PathField>()
            {
                new PathField("Color","_tkColor"),
                new PathField("Who","_query"),
            },
            (input)=>
            {

                input.GetQueryElements((element)=>
                {
                    input.reactionData.TkColor.Play(input.gameObj,element);
                });

            },SUCompatibility_ID.UIToolkit )},

             {"_SUtkanim5",
            new PathAction("Animations/Size",
            new List<PathField>()
            {
                new PathField("Size","_tkSize"),
                new PathField("Who","_query"),
            },
            (input)=>
            {

                input.GetQueryElements((element)=>
                {
                    input.reactionData.TkSize.Play(input.gameObj,element);
                });

            },SUCompatibility_ID.UIToolkit )},
#endif

#endregion


        };

        static int GetPlayerID(FuncInput input)
        {

            var playerID = input.fieldsValues.Int_2;

            if(playerID == SurferHelper.kNestedPlayerID)
            {
                playerID = input.elementData.PlayerID;
            }

            return playerID;
        }


        /// <summary>
        /// Get all the names/paths of all the reactions both Custom and Default. Used for the inspector
        /// </summary>
        /// <returns>Names/paths list</returns>
        public static string[] GetAllNames()
        {
            return All.Select(x=>x.Value.Path).OrderBy(x=>x).Prepend(SurferHelper.Unset).ToArray();
        }

        /// <summary>
        /// Get all the Canvas names/paths of all the reactions both Custom and Default. Used for the inspector
        /// </summary>
        /// <returns>Names/paths list</returns>
        public static string[] GetAllCanvasNames()
        {
            return All.Where(x=>x.Value.Compatibility.IsCanvasCompatible()).Select(x=>x.Value.Path).OrderBy(x=>x).Prepend(SurferHelper.Unset).ToArray();
        }

        /// <summary>
        /// Get all the UIToolkit names/paths of all the reactions both Custom and Default. Used for the inspector
        /// </summary>
        /// <returns>Names/paths list</returns>
        public static string[] GetAllUIToolkitNames()
        {
            return All.Where(x=>x.Value.Compatibility.IsUIToolkitCompatible()).Select(x=>x.Value.Path).OrderBy(x=>x).Prepend(SurferHelper.Unset).ToArray();
        }

        /// <summary>
        /// Get the name/path of a specific reaction. Used for the inspector
        /// </summary>
        /// <param name="key">Reaction key to retrieve the name/path</param>
        /// <returns>Reaction name/path</returns>
        public static string GetName(string key)
        {
            if(All.TryGetValue(key,out PathAction value))
                return value.Path;

            return string.Empty;
        }

        /// <summary>
        /// Get the key of a specific reaction. Used for the inspector
        /// </summary>
        /// <param name="path">Reaction name/path to retrieve the key</param>
        /// <returns>Reaction key</returns>
        public static string GetKey(string path, SUCompatibility_ID compatibilityID = SUCompatibility_ID.Both)
        {
            foreach(KeyValuePair<string, PathAction> pair in All)
            {
                if(!pair.Value.Compatibility.IsCompatibleWith(compatibilityID))
                    continue;

                if(pair.Value.Path.Equals(path))
                return pair.Key;
            }
            return string.Empty;
        }


    

    }


}

