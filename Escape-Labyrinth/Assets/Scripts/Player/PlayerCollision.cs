using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
     void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collided with something: " + col.gameObject.name);
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Collided with Enemy");
            Destroy(this.gameObject);
            //Genie.instance.HerbsGameActivated();
        }
    }
}
