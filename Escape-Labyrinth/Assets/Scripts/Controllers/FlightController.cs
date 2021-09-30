using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    private Vector3 startPosition;
    Dictionary<int, GameObject> foodStore;
    private GameObject banana;
    private GameObject cherry;
    private GameObject melon;
    private GameObject donut;
    private GameObject hamburger;
    private bool calledFallingFood;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = GetComponent<Transform>().position;
        banana = GameObject.Find("Banana");
        cherry = GameObject.Find("Cherry");
        melon = GameObject.Find("Melon");
        donut = GameObject.Find("Donut");
        hamburger = GameObject.Find("Hamburger");
        foodStore = new Dictionary<int, GameObject>();
        foodStore.Add(1, banana);
        foodStore.Add(2, cherry);
        foodStore.Add(3, melon);
        foodStore.Add(4, donut);
        foodStore.Add(5, hamburger);
        calledFallingFood = false;
    }

    // Update is called once per frame
    void Update()
    {   if (!calledFallingFood)
        {
            StartCoroutine(FallingFood());
            calledFallingFood = true;
        }
        float xForce = Random.Range(-5f, 5f);
        float yForce = 0f;
        float zForce = 4f;

        Vector3 force = new Vector3(xForce, yForce, zForce);
        GetComponent<Rigidbody>().velocity = force;
    }

    private IEnumerator FallingFood()
    {
        for (int i = 0; i < 30; i++)
        {
            int randInt = Random.Range(1,6);
            Quaternion spawnRotation = Quaternion.Euler(0,0,0);
            Vector3 position = GetComponent<Transform>().position;
            if (randInt == 5)
                position.y = 0;
            Instantiate(foodStore[randInt], position, spawnRotation);
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }
}
