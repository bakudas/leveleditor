using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.Vaca_Roxa.Level_Editor.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CustomEditor(typeof(LevelConstructor))]
public class CreateLevelInspector : Editor
{
    private Vector3 _mousePosition;
    private LevelConstructor _level;
    private bool _createdObjects = false;

    public Vector3 GetMousePosition()
    {
        return _mousePosition;
    }

    private void StepPhysics()
    {
        Physics.autoSimulation = false;
        
        if (Physics.autoSimulation)
            return; // do nothing if the automatic simulation is enabled

        float timer = 0f;
        timer += Time.deltaTime;

        // Catch up with the game time.
        // Advance the physics simulation in portions of Time.fixedDeltaTime
        // Note that generally, we don't want to pass variable delta to Simulate as that leads to unstable results.
        while (timer >= Time.fixedDeltaTime)
        {
            timer -= Time.fixedDeltaTime;
            Physics.Simulate(Time.fixedDeltaTime);
        }
    }
 
    [MenuItem("Tools/Scene Physics")]
    private static void OpenWindow()
    {
        
    }
    
    private void OnSceneGUI()
    {
        if (_level == null)
        {
            _level = (LevelConstructor) target;
        }
        
        _mousePosition = Event.current.mousePosition;
        Ray ray = HandleUtility.GUIPointToWorldRay(_mousePosition);
        _mousePosition = ray.GetPoint(5f);

        if (_createdObjects)
        {
            Event e = Event.current;
            if (e.isMouse)
            {
                if (e.type == EventType.MouseUp)
                {
                    if (e.button == 0  && e.alt)
                    {
                        _level.CreateObject(GetMousePosition());
                        EditorUtility.SetDirty(_level);
                        EditorUtility.SetDirty(_level.gameObject);
                    }
                }
                
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (GUILayout.Button("Liga a Física o/"))
        {
            StepPhysics();
        }
        
        if (GUILayout.Button("Cria Level"))
        {
            _level.StartCreateLevel(_level.mapJson);
        }
        
        if (GUILayout.Button("Cria Objeto com clique do Mouse = " + _createdObjects))
        {
            _createdObjects = !_createdObjects;
        }

        try
        {
            foreach (InGameObject gameObject in _level._createdObjects)
            {
                if (GUILayout.Button("Destruir o Objeto @" + gameObject.GetGameObject().transform.position))
                {
                    _level.DestroyGameObject(gameObject);
                }
            }

        }
        catch (Exception e)
        {
            
        }
    }
}
