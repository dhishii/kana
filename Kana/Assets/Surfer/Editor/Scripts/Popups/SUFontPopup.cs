using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

namespace Surfer
{
    public class SUFontPopup : SUGUIDBasePopup
    {
        TMP_FontAsset _font = default;
        protected override string ValueError { get; set; } = "Add a Font above";

        public SUFontPopup(System.Action<SUGUIDElementBaseData> OnSave)  : base(OnSave) { }

        public SUFontPopup(SUGUIDFontData dataToEdit,System.Action<SUGUIDElementBaseData> OnSave)  : base(dataToEdit, OnSave) { }
        
        protected override void OnCustomField()
        {
            base.OnCustomField();
            GUILayout.Label("Font : ", EditorStyles.boldLabel);
            _font = EditorGUILayout.ObjectField(_font,typeof(TMP_FontAsset),true) as TMP_FontAsset;
        }

        protected override void OnSave()
        {
            _showNameError = false;
            _showValueError = false;

            if(_font == null)
            {
                _showValueError = true;
            }
            if(!SurferHelper.SO.Themes.IsFontNameAvailable(Name) && _modeID == Mode_ID.Add)
            {
                _showNameError = true;
            }

            if (!_showNameError && !_showValueError)
            {
                base.OnSave();

                var guid = _modeID == Mode_ID.Edit ? _dataToEdit.GUID : System.Guid.NewGuid().ToString();

                OnConfirmData?.Invoke(new SUGUIDFontData(guid, Name, _font));
            }
        }

        public override void OnOpen()
        {
            base.OnOpen();

            if(_modeID == Mode_ID.Add)
            {
                Name = "my font";
                _font = null;
            }
            else
            {
                Name = _dataToEdit.Name;
                _font = (_dataToEdit as SUGUIDFontData).Font;
            }
        }
    }
}

