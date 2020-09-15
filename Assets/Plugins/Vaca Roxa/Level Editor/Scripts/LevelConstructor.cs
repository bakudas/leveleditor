using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Serialization;


namespace Plugins.Vaca_Roxa.Level_Editor.Scripts
{
   [Serializable]
    public class LevelConstructor : MonoBehaviour
    {
       [Header("Basic Level Setup")]
        [SerializeField]
        public string mapJson;
        public List<InGameObject> _createdObjects;
        
        private List<GameObject> _CreatedObjectsAsGameObjects;
        
        [Header("Entities")]
        [FormerlySerializedAs("Player")] public GameObject _player;
        //TODO: INIMIGOS QUE VAI SER FODA
        [FormerlySerializedAs("Bomb")] public GameObject _bomb;
        [FormerlySerializedAs("Basic Floor")] public GameObject _basicFloor;

        private Vector3 positions;

        private JObject mapa;

        private void Start()
        {
           
           // get do tipo do tile
           //tiletype = (JArray) mapa["tileTypes"];
           
           //StartCoroutine(CriaLevel(mapJson));
           
        }

        public void StartCreateLevel(string map)
        {
           if (_createdObjects == null)
           {
              _createdObjects = new List<InGameObject>();
           }

           if (_CreatedObjectsAsGameObjects == null)
           {
              _CreatedObjectsAsGameObjects = new List<GameObject>();
           }

           var mapa = JObject.Parse(map);
           var tiletype = (JArray)mapa["tileTypes"];
           
           for (int i = 0; i < tiletype.Count; i++)

           {
              positions.x = (float) mapa["positions"][i]["x"];
              positions.y = (float) mapa["positions"][i]["y"];
              positions.z = (float) mapa["positions"][i]["z"];
              
              GameObject go = (GameObject)Instantiate(_basicFloor, positions, Quaternion.identity);
              
              _createdObjects.Add( new InGameObject(go));
              _CreatedObjectsAsGameObjects.Add(go);
           }
           
        }

        /*private IEnumerator CriaLevel(string json)
        {
           var mapa = JObject.Parse(json);
           var tiletype = (JArray)mapa["tileTypes"];
           
           for (int i = 0; i < tiletype.Count; i++)

           {
              positions.x = (float) mapa["positions"][i]["x"];
              positions.y = (float) mapa["positions"][i]["y"];
              positions.z = (float) mapa["positions"][i]["z"];
              
              Instantiate(_basicFloor, positions, Quaternion.identity);
              
              //yield return new WaitForSeconds(.1f);
           }
           
        }*/

        public void CreateObject(Vector3 position)
        {
           if (_createdObjects == null)
           {
              _createdObjects = new List<InGameObject>();
           }

           if (_CreatedObjectsAsGameObjects == null)
           {
              _CreatedObjectsAsGameObjects = new List<GameObject>();
           }

           GameObject go = (GameObject) Instantiate(_basicFloor, position, Quaternion.Euler(0,0,0 ));
           
           _createdObjects.Add( new InGameObject(go));
           _CreatedObjectsAsGameObjects.Add(go);

        }

        public void DestroyGameObject(InGameObject objectToDestroy)
        {
           _CreatedObjectsAsGameObjects.Remove(objectToDestroy.GetGameObject());
           _createdObjects.Remove(objectToDestroy);
           DestroyImmediate(objectToDestroy.GetGameObject());
        }
       
    }
    
    [SerializeField]
    public class InGameObject
    {
       public GameObject _GameObject;

       public InGameObject(GameObject _obj)
       {
          _GameObject = _obj;
       }

       public GameObject GetGameObject()
       {
          return _GameObject;
       }

    }
}