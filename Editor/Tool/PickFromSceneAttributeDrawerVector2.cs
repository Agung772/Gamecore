using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Gamecore.Tool
{
    public class PickFromSceneAttributeDrawerVector2 : OdinAttributeDrawer<PickFromSceneAttribute, Vector2>{
        private string label;
        private Vector2 current;
        private GUIStyle buttonStyle;
        private GUIContent labelContent;
        private IfAttributeHelper ifAttributeHelper;
        private object valueCondition;
        private bool hideIfCondition;
        private Color handleColor;

        protected override void Initialize() {
            handleColor  = ValueResolver.Get(Property,Attribute.HandleColor, Color.white).GetValue();
            label = Property.NiceName.ToTitleCase();
            current = ValueEntry.SmartValue;
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;
            SceneView.RepaintAll();
            buttonStyle = new GUIStyle(GUI.skin.button);
            SetupOdinVisibilityAttribute();
        }

        private void OnSceneGUI(SceneView sceneView) {
            try {
                if (Property?.Tree?.UnitySerializedObject?.targetObject == null) return;
                if (!Property.IsReachableFromRoot()) {
                    SceneView.duringSceneGui -= OnSceneGUI;
                }
            }
            catch {
                SceneView.duringSceneGui -= OnSceneGUI;
                return;
            }
            if (!IsVisibleInInspector()) return;
            var _guiColor = Handles.color;
            Handles.color = handleColor;
            var _handlePosition = (Vector2)Handles.FreeMoveHandle(ValueEntry.SmartValue, 0.5f, Vector3.zero, Handles.SphereHandleCap);
            var _label = Attribute.UsePathAsAsLabel ? Property.Path.Replace("$", "") : this.label;
            Handles.color = _guiColor;
            Handles.Label(_handlePosition + Vector2.down, _label, buttonStyle);
            if (current == _handlePosition) return;
            ValueEntry.SmartValue = _handlePosition;
            current = _handlePosition;
            try {
                ValueEntry?.ApplyChanges();
            }
            catch (Exception) {
                SceneView.duringSceneGui -= OnSceneGUI;
            }
        }

        protected override void DrawPropertyLayout(GUIContent content) {
            GUILayout.BeginHorizontal();
            if (Attribute.Label != "") {
                this.label = Attribute.Label;
            }
            else {
                this.label = content != null ? content.text : Property.NiceName;
            }

            var _value = EditorGUILayout.Vector2Field(this.label, ValueEntry.SmartValue);

            if (current != _value) {
                ValueEntry.SmartValue = _value;
                current = _value;
                SceneView.RepaintAll();
                ValueEntry.ApplyChanges();
            }

            if (SirenixEditorGUI.IconButton(EditorIcons.Flag, buttonStyle)) {
                SetPositionToCurrentSceneViewFrame();
            }

            if (SirenixEditorGUI.IconButton(EditorIcons.MagnifyingGlass, buttonStyle)) {
                SetFramePosition(current);
            }

            GUILayout.EndHorizontal();
        }

        private void SetupOdinVisibilityAttribute() {
            // Jika ada lebih dari satu visibility attribute, kita gk bisa tentuin pilih mana (gk ada akses ke odin order) 
            // jadi , kalo perlu dimerge condition yg di property nya
            // tp, dikebanyakan kasus hanya ada Showif/Hideif
            var _condition = "";
            if (TryGetAttribute<ShowIfAttribute>(out var _showIfAttribute)) {
                _condition = _showIfAttribute.Condition;
                valueCondition = _showIfAttribute.Value;
                hideIfCondition = false;
            }

            if (TryGetAttribute<HideIfAttribute>(out var _hideIfAttribute)) {
                _condition = _hideIfAttribute.Condition;
                valueCondition = _hideIfAttribute.Value;
                hideIfCondition = true;
            }

            if (_condition == "") return;
            ifAttributeHelper = new IfAttributeHelper(Property, _condition, true);
        }

        private bool TryGetAttribute<T>(out T attribute) where T : Attribute {
            attribute = Property.Attributes.GetAttribute<T>();
            return attribute != null;
        }

        private bool IsVisibleInInspector() {
            if (ifAttributeHelper == null) return true;
            var _ifValue = ifAttributeHelper.GetValue(valueCondition);
            if (hideIfCondition) return !_ifValue;
            return _ifValue;
        }

        private void SetPositionToCurrentSceneViewFrame() {
            if (SceneView.lastActiveSceneView == null) return;
            if (SceneView.lastActiveSceneView.camera == null) return;
            current = SceneView.lastActiveSceneView.camera.transform.position;
            ValueEntry.SmartValue = current;
            SceneView.RepaintAll();
            ValueEntry.ApplyChanges();
        }

        private void SetFramePosition(Vector2 position) {
            if (SceneView.lastActiveSceneView == null) return;
            SceneView.lastActiveSceneView.Frame(new Bounds(position, Vector3.one * 10), false);
        }

        ~PickFromSceneAttributeDrawerVector2() {
            SceneView.duringSceneGui -= OnSceneGUI;
        }
    }
}