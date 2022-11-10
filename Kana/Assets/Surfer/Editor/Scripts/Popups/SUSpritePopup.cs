using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Surfer
{
    public class SUSpritePopup : SUGUIDBasePopup
    {
        Sprite _sprite = default;
        protected override string ValueError { get; set; } = "Add a Sprite above";

        public SUSpritePopup(System.Action<SUGUIDElementBaseData> OnSave) : base( OnSave) { }
        public SUSpritePopup(SUGUIDSpriteData dataToEdit, System.Action<SUGUIDElementBaseData> OnSave) : base( dataToEdit, OnSave) { }

        protected override void OnCustomField()
        {
            base.OnCustomField();
            GUILayout.Label("Sprite : ", EditorStyles.boldLabel);
            _sprite = EditorGUILayout.ObjectField(_sprite,typeof(Sprite),true) as Sprite;
        }

        protected override void OnSave()
        {
            _showNameError = false;
            _showValueError = false;

            if(_sprite == null)
            {
                _showValueError = true;
            }
            if(!SurferHelper.SO.Themes.IsSpriteNameAvailable(Name) && _modeID == Mode_ID.Add)
            {
                _showNameError = true;
            }

            if(!_showNameError && !_showValueError)
            {
                base.OnSave();

                var guid = _modeID == Mode_ID.Edit ? _dataToEdit.GUID : System.Guid.NewGuid().ToString();
                OnConfirmData?.Invoke(new SUGUIDSpriteData(guid,Name,_sprite));
            }
            
        }

        public override void OnOpen()
        {
            base.OnOpen();

            if(_modeID == Mode_ID.Add)
            {
                Name = "my sprite";
                _sprite = null;
            }
            else
            {
                Name = _dataToEdit.Name;
                _sprite = (_dataToEdit as SUGUIDSpriteData).Sprite;
            }
        }

    }
}

