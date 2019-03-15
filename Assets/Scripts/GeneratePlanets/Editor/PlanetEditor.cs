using System;
using UnityEditor;
using UnityEngine;

namespace GeneratePlanets
{
    [CustomEditor(typeof(Planet))]
    public class PlanetEditor : Editor
    {
        private Planet _planet;
        private Editor _shapeEditor;
        private Editor _colorEditor;
        
        public override void OnInspectorGUI()
        {
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                base.OnInspectorGUI();
                if (check.changed)
                {
                    _planet.GeneratePlanet();
                }
            }

            if (GUILayout.Button("Generate planet"))
            {
                _planet.GeneratePlanet();
            }
            
//            DrawSettingsEditor(_planet.ColorSettings, _planet.OnColorSettingsUpdate, true, ref _colorEditor);
            DrawSettingsEditor(_planet.PlanetSettings, null, true, ref _shapeEditor);
        }

        private void DrawSettingsEditor(UnityEngine.Object settings, Action onSettingsUpdate, bool foldout, ref Editor editor)
        {
            if(settings==null)
                return;

            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdate != null)
                        {
                            onSettingsUpdate();
                        }
                    }
                }
            }
        }

        private void OnEnable()
        {
            _planet = (Planet) target;
        }
    }
}