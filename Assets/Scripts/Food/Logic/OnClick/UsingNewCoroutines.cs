using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UseFoodComponent.Logic.OnClick
{
    public class UsingNewCoroutines : MonoBehaviour
    {
        private Coroutine _cor;
        private static List<Coroutine> _coroutines = new List<Coroutine>(10);

        public IEnumerator StartCoroutineAndWaitIt(IEnumerator coroutine)
        {
            StartCoroutineAndAddToList(coroutine);
            yield return _cor;
        }
        public void StartCoroutineAndAddToList(IEnumerator coroutine)
        {
            _cor = StartCoroutine(coroutine);
            _coroutines.Add(_cor);
        }

        public void StopAllCoroutinesOfThisClass()
        {
            foreach (var item in _coroutines)
            {
                if (item != null)
                    StopCoroutine(item);
            }
            _coroutines.Clear();
        }
    }
}