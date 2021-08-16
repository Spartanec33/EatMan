using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace UseFoodComponent.Personal
{
    [CreateAssetMenu(fileName = "FoodProperty-Sprite", menuName = "FoodProperty-Sprite(dict)")]
    public class FoodPropertiesMap : ScriptableObject
    {
        private Dictionary<string, Image> _dic;
        [SerializeField] private FoodPropertiesEntry[] _foodProperties;

        [Serializable]
        private class FoodPropertiesEntry
        {
            public string Property;
            public Image Image;
        }
        private void Start()
        {
            _dic = new Dictionary<string, Image>(_foodProperties.Length);
            foreach (var item in _foodProperties)
            {
                _dic.Add(item.Property, item.Image);
            }
        }
    }
}


