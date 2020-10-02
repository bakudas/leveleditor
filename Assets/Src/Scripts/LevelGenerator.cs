using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Serialization;


namespace Src.Scripts
{

    public class LevelGenerator : MonoBehaviour
    {
       [Header("Basic Level Setup")]
        [SerializeField]
        public string mapJson;
        
        [Header("Entities")]
        [FormerlySerializedAs("Player")] public GameObject _player;
        //TODO: INIMIGOS QUE VAI SER FODA
        [FormerlySerializedAs("Bomb")] public GameObject _bomb;
        [FormerlySerializedAs("Basic Floor")] public GameObject _basicFloor;

        private Vector3 positions;
        private float positionsX;
        private float positionsY;
        private float positionsZ;
        
        private JObject mapa;
        
        private void Start()
        {
           
           // get do tipo do tile
           //tiletype = (JArray) mapa["tileTypes"];
           
           StartCoroutine(CriaLevel(mapJson));
           
        }

        IEnumerator CriaLevel(string json)
        {
           var mapa = JObject.Parse(json);
           var tiletype = (JArray)mapa["tileTypes"];
           
           for (int i = 0; i < tiletype.Count; i++)

           {
              positions.x = (float) mapa["positions"][i]["x"];
              positions.y = (float) mapa["positions"][i]["y"];
              positions.z = (float) mapa["positions"][i]["z"];
              
              Instantiate(_basicFloor, positions, Quaternion.identity);
              
              yield return new WaitForSeconds(.1f);
           }
        }
    }

    // FORMATO ESPERADO
    /*{
        "tileTypes":[
           4,
           4,
           4
        ],
        "positions":[
           {
              "x":3.5,
              "y":-1.0,
              "z":-13.5
           },
           {
              "x":3.5,
              "y":-1.0,
              "z":-12.5
           },
           {
              "x":3.5,
              "y":-1.0,
              "z":-11.5
           }
        ],
       "rotations": [
        {
            "x": 0.0,
            "y": -1.0,
            "z": 0.0,
            "w": 0.0
        },
        {
            "x": 0.0,
            "y": 0.0,
            "z": 0.0,
            "w": 1.0
        },
        {
            "x": 0.0,
            "y": 1.0,
            "z": 0.0,
            "w": -4.371138828673793e-8
        }
     }*/
    
}