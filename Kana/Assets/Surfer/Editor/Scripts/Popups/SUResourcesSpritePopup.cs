using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Surfer
{
    public class SUResourcesSpritePopup : SUGUIDBasePopup
    {
        string _path = default;
        protected override string ValueError { get; set; } = "Add a Path above";

        public SUResourcesSpritePopup(System.Action<SUGUIDElementBaseData> OnSave) : base(OnSave) { }

        public SUResourcesSpritePopup(SUGUIDResourcesSpriteData dataToEdit, System.Action<SUGUIDElementBaseData> OnSave) : base(dataToEdit, OnSave) { }

        protected override void OnCustomField()
        {
            base.OnCustomField();
            GUILayout.Label("Resources Path : ", EditorStyles.boldLabel);
            _path = EditorGUILayout.TextField(_path);
        }

        protected override void OnSave()
        {
            _showNameError = false;
            _showValueError = false;

            if(string.IsNullOrEmpty(_path))
            {
                _showValueError = true;
            }
            if(!SurferHelper.SO.Themes.IsResSpriteNameAvailable(Name) && _modeID == Mode_ID.Add)
            {
                _showNameError = true;
            }

            if (!_showNameError && !_showValueError)
            {   
                base.OnSave();

                var guid = _modeID == Mode_ID.Edit ? _dataToEdit.GUID : System.Guid.NewGuid().ToString();
                OnConfirmData?.Invoke(new SUGUIDResourcesSpriteData(guid,Name,_path));
            }
            
        }

        public override void OnOpen()
        {
            base.OnOpen();

            if(_modeID == Mode_ID.Add)
            {
                Name = "my sprite";
                _path = string.Empty;
            }
            else
            {
                Name = _dataToEdit.Name;
                _path = (_dataToEdit as SUGUIDResourcesSpriteData).Path;
            }
        }
    }
}

