using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FoodFinal 
{
    private  static Player player=GameObject.FindObjectOfType<Player>();
    private static float coroutineStep=0.05f;
    public static bool isCoroutineActive;



    /// <summary>
    /// 
    /// хуевое название
    /// </summary>
    /// <param name="food"></param>
    /// <returns></returns>
    public static IEnumerator Final(Food food)
    {
        isCoroutineActive = true;

        //поворот
        var startRotation = player.transform.rotation;
        player.transform.LookAt(food.transform);
        var finalRotation = player.transform.rotation;
        float progress=0;
        while (progress<1)
        {
            player.transform.rotation = Quaternion.Lerp(startRotation, finalRotation, progress);
            yield return new WaitForSeconds(coroutineStep);
            progress += coroutineStep;
        }
        //движение

        //анимация

        // сравнение
        if (FoodComparer.Compare(food) && Mover.isMovable == false)
        {
            FoodSpawner.Delete();
            FoodSpawner.Spawn();
        }
        isCoroutineActive = false;
    }
}
