using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Surfer
{
    public class SUColorPopup : SUGUIDBasePopup
    {
        Color _color = default;

        public SUColorPopup(System.Action<SUGUIDElementBaseData> OnSave)  : base(OnSave) { }

        public SUColorPopup(SUGUIDColorData dataToEdit,System.Action<SUGUIDElementBaseData> OnSave)  : base(dataToEdit, OnSave) { }

        protected override void OnCustomField()
        {
            base.OnCustomField();
            GUILayout.Label("Color : ", EditorStyles.boldLabel);
            _color = EditorGUILayout.ColorField(_color);
        }

        protected override void OnSave()
        {
            _showNameError = false;
            
            if(!SurferHelper.SO.Themes.IsColorNameAvailable(Name) && _modeID == Mode_ID.Add)
            {
                _showNameError = true;
            }
            else
            {
                base.OnSave();

                var guid = _modeID == Mode_ID.Edit ? _dataToEdit.GUID : System.Guid.NewGuid().ToString();
                OnConfirmData?.Invoke(new SUGUIDColorData(guid,Name,_color));
            }
            
        }

        public override void OnOpen()
        {
            base.OnOpen();

            if(_modeID == Mode_ID.Add)
            {
                Name = "my color";
                _color = Color.black;
            }
            else
            {
                Name = _dataToEdit.Name;
                _color = (_dataToEdit as SUGUIDColorData).Color;
            }
        }

        public override void OnClose()
        {
        }
    }
}
