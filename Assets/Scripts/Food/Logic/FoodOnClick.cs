using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOnClick: MonoBehaviour
{
    private Player player;
    private Mover mover;
    public float coroutineStep=0.05f;
    public float newStopDistance=2;
    public static bool isCoroutineActive;
    private void Start()
    {
          player = GameObject.FindObjectOfType<Player>();
          mover = GameObject.FindObjectOfType<Mover>();
    }

    public  IEnumerator Final(Food food)
    {
        isCoroutineActive = true;
        //поворот
        //движение
        yield return StartCoroutine(Move(food));        
        
        //анимация

        // сравнение


        if (FoodComparer.Compare(food) && Mover.isMovable == false)
        {
            FoodSpawner.Delete();
            FoodSpawner.Spawn();
        }
        isCoroutineActive = false;
    }
    public IEnumerator Rotate(Food food)
    {
        var startRotation = player.transform.rotation;
        player.transform.LookAt(food.transform);
        var finalRotation = player.transform.rotation;
        float progress = 0;
        while (progress < 1)
        {
            player.transform.rotation = Quaternion.Lerp(startRotation, finalRotation, progress);
            yield return new WaitForSeconds(coroutineStep);
            progress += coroutineStep;
        }
    }
    public IEnumerator Move(Food food)
    {
        yield return StartCoroutine(Rotate(food));
        while(mover.FindDistance()>newStopDistance)
        {
            Debug.Log(mover.FindDistance());

            yield return new WaitForFixedUpdate();
            mover.Move();
        }
    }
}
