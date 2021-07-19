using System.Collections;
using UnityEngine;

public class FoodOnClick: MonoBehaviour
{
    private Player player;
    private Mover mover;

    private Vector3 startPosition;
    private Quaternion startRotation;

    public float coroutineStep=0.05f;
    public float newStopDistance=2;

    public static bool isCoroutineActive;
    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        mover = GameObject.FindObjectOfType<Mover>();

        startPosition = player.transform.position;
        startRotation = player.transform.rotation;
    }

    public  IEnumerator Final(Food food)
    {
        isCoroutineActive = true;
        Mover.needOneTimeStop = false;


        yield return StartCoroutine(Move(food));        
        
        //анимация

        // сравнение
        if (FoodComparer.Compare(food) && mover.FindDistance() <= newStopDistance)
        {
            FoodSpawner.Delete();
            FoodSpawner.Spawn();
        }


        

        isCoroutineActive = false;
        Mover.oneTimeStop = false;
        Mover.needOneTimeStop = true;
        yield return StartCoroutine(Undo());
    }
    public IEnumerator Rotate(Food food)
    {
        var startRotation = player.transform.rotation;
        player.transform.LookAt(food.transform);
        var finalRotation = player.transform.rotation;
        finalRotation = new Quaternion(startRotation.x, finalRotation.y, startRotation.z, startRotation.w);
        float progress = 0;
        while (progress < 1)
        {
            player.transform.rotation = Quaternion.Lerp(startRotation, finalRotation, progress);
            yield return new WaitForFixedUpdate();
            progress += coroutineStep;
        }
    }
    public IEnumerator Move(Food food)
    {
        yield return StartCoroutine(Rotate(food));

        float allWay = mover.FindDistance() - newStopDistance;
        float coveredDistance = 0;

        var foodPos = food.transform.position;
        var basePlayerPos = player.transform.position;
       

        while (mover.FindDistance()>=newStopDistance)
        {

            yield return new WaitForFixedUpdate();

            var progress = (coveredDistance / allWay);
            var posX = Vector3.Lerp(basePlayerPos, foodPos, progress).x;
            player.transform.position = new Vector3(posX, player.transform.position.y, player.transform.position.z);

            if (Mover.oneTimeStop==true)
                mover.Move();
            coveredDistance += (mover.Speed * Time.deltaTime);
        }
    }

    //возвращает игрока на место
    public IEnumerator Undo()
    {
        yield return new WaitForFixedUpdate();
    }
}
