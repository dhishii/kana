using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Surfer
{
    public class SUGUIDBasePopup : PopupWindowContent
    {
        public enum Mode_ID
        {
            Add,
            Edit
        }

        protected System.Action<SUGUIDElementBaseData> OnConfirmData = default;
        protected string Name { get; set; } = "entry name";
        protected string _title = string.Empty;
        protected string _nameError = "Name already exists";
        protected virtual string ValueError { get; set; } = "Error";
        protected bool _showNameError = default;
        protected bool _showValueError = default;
        protected Mode_ID _modeID = default;
        protected SUGUIDElementBaseData _dataToEdit = default;

        public override Vector2 GetWindowSize()
        {
            return new Vector2(200,190);
        }

        public override void OnGUI(Rect rect)
        {
            _title = _modeID == Mode_ID.Add ? "Add new" : "Edit";

            GUILayout.Label(_title, EditorStyles.boldLabel);
            GUILayout.Space(SurferHelper.lineHeight);
            GUILayout.Label("Name : ", EditorStyles.boldLabel);
            Name = EditorGUILayout.TextField(Name);

            GUILayout.Label(_showNameError ? _nameError : string.Empty, EditorStyles.miniLabel);

            OnCustomField();

            GUILayout.Label(_showValueError ? ValueError : string.Empty, EditorStyles.miniLabel);

            GUILayout.Space(SurferHelper.lineHeight);
            if (GUILayout.Button("SAVE"))
            {
                OnSave();
            }
        }

        protected virtual void OnCustomField() {}
        protected virtual void OnSave() 
        {
            editorWindow.Close();
        }

        public override void OnOpen()
        {
            _showValueError = _showNameError = false;
        }

        public SUGUIDBasePopup(System.Action<SUGUIDElementBaseData> OnSave)
        {
            _modeID = Mode_ID.Add;
            OnConfirmData = OnSave;
        }

        public SUGUIDBasePopup(SUGUIDElementBaseData elementToEdit,System.Action<SUGUIDElementBaseData> OnSave)
        {
            _dataToEdit = elementToEdit;
            _modeID = Mode_ID.Edit;
            OnConfirmData = OnSave;
        }
    }
}

