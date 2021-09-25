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
    private bool calledFallingFood;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = GetComponent<Transform>().position;
        banana = GameObject.Find("Banana");
        cherry = GameObject.Find("Cherry");
        melon = GameObject.Find("melon");
        foodStore = new Dictionary<int, GameObject>();
        foodStore.Add(1, banana);
        foodStore.Add(2, cherry);
        foodStore.Add(3, melon);
        calledFallingFood = false;
    }

    // Update is called once per frame
    void Update()
    {   if (!calledFallingFood)
        {
            StartCoroutine(FallingFood());
            calledFallingFood = true;
        }
        // if state
        float xForce = Random.Range(-40f, 40f);
        float yForce = 0f;
        float zForce = 10f;

        Vector3 force = new Vector3(xForce, yForce, zForce);
        GetComponent<Rigidbody>().velocity = force;
    }

    private IEnumerator FallingFood()
    {
        for (int i = 0; i < 10; i++)
        {
            int randInt = Random.Range(1,3);
            Quaternion spawnRotation = Quaternion.Euler(0,0,0);
            Instantiate(foodStore[randInt], GetComponent<Transform>().position, spawnRotation);
            Debug.Log("Something should be falling");
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }
}
