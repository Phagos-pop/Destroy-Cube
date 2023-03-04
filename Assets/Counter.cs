using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] List<GameObject> numbers;
    [SerializeField] GameObject number;
    [SerializeField] CubeSpawner spawner;

    private GameObject secondGameobject;
    private int deadCount;

    private void Start()
    {
        
        deadCount = 0;
        spawner.DeadCubeEvent += Spawner_DeadCubeEvent;
    }

    private void OnDestroy()
    {
        spawner.DeadCubeEvent -= Spawner_DeadCubeEvent;
    }

    private void Spawner_DeadCubeEvent()
    {
        AddCount();
    }

    private void AddCount()
    {
        deadCount++;
        if (deadCount < 10)
        {
            Transform numberTransform = number.transform;
            Destroy(number);
            number = Instantiate(numbers[deadCount], this.transform);
            number.transform.position = numberTransform.position;
            number.transform.rotation = numberTransform.rotation;
            number.transform.localScale = numberTransform.localScale;
        }
        else
        {
            int firstNumber = deadCount / 10;
            int secondNumber = deadCount % 10;

            Transform numberTransform = number.transform;

            Destroy(number);
            number = Instantiate(numbers[firstNumber], this.transform);
            number.transform.position = new Vector3(0, 0, 8) + new Vector3(0.5f, 0, 0);
            number.transform.rotation = numberTransform.rotation;
            number.transform.localScale = numberTransform.localScale;

            Destroy(secondGameobject);
            secondGameobject = Instantiate(numbers[secondNumber], this.transform);
            secondGameobject.transform.position = new Vector3(0, 0, 8) + new Vector3(-0.5f, 0, 0);
            secondGameobject.transform.rotation = numberTransform.rotation;
            secondGameobject.transform.localScale = numberTransform.localScale;
        }
    }
}
