using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace UseFoodComponent.Personal
{
    [CreateAssetMenu(fileName = "FoodProperty-Sprite", menuName = "FoodProperty-Sprite(dict)")]
    public class FoodPropertiesMap : ScriptableObject
    {
        [SerializeField] private FoodPropertiesEntry[] _foodProperties;

        private Dictionary<string, Sprite> _dic;

        public Dictionary<string, Sprite> GetDictionary()
        {
            InitializeDictionary();
            return _dic;
        }

        [Serializable]
        private class FoodPropertiesEntry
        {
            public string Property;
            public Sprite Sprite;
        }
        private void InitializeDictionary()
        {
            _dic = new Dictionary<string, Sprite>(_foodProperties.Length);
            foreach (var item in _foodProperties)
            {
                _dic.Add(item.Property, item.Sprite);
            }
            Debug.Log("create _dic");

        }
    }
}


