using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Surfer
{
    public class SUFontSizePopup : SUGUIDBasePopup
    {
        int _minValue = 10;
        int _maxValue = 40;
        int _fontSize = default;

        public SUFontSizePopup(System.Action<SUGUIDElementBaseData> OnSave) : base(OnSave) { }
        
        public SUFontSizePopup(SUGUIDFontSizeData dataToEdit,System.Action<SUGUIDElementBaseData> OnSave) : base(dataToEdit, OnSave) { }

        protected override void OnCustomField()
        {
            base.OnCustomField();
            GUILayout.Label("Size : ", EditorStyles.boldLabel);
            _fontSize = EditorGUILayout.IntSlider(_fontSize,_minValue,_maxValue);
        }

        protected override void OnSave()
        {
            _showNameError = false;

            if(!SurferHelper.SO.Themes.IsFontSizeNameAvailable(Name) && _modeID == Mode_ID.Add)
            {
                _showNameError = true;
            }
            else
            {
                base.OnSave();

                var guid = _modeID == Mode_ID.Edit ? _dataToEdit.GUID : System.Guid.NewGuid().ToString();
                OnConfirmData?.Invoke(new SUGUIDFontSizeData(guid,Name,_fontSize));
            }
            
        }

        public override void OnOpen()
        {
            base.OnOpen();

            if(_modeID == Mode_ID.Add)
            {
                Name = "my font size";
                _fontSize = _minValue;
            }
            else
            {
                Name = _dataToEdit.Name;
                _fontSize = (_dataToEdit as SUGUIDFontSizeData).FontSize;
            }
        }

        public override void OnClose()
        {
        }
    }
}

