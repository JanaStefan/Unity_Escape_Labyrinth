using UnityEngine; 

public class PlayerRaycast : MonoBehaviour
{
    public float distanceToSee;
    public PlayerManager playerManager;
   //public LayerMask layerMask;
    //public int layerMask2 = 1 << 8;
    
    private RaycastHit _objectThatIHit;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        int layerIndex = 1 << LayerMask.NameToLayer("Interactable");
        
        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.magenta);

        
        if (Physics.Raycast(this.transform.position, this.transform.forward, out _objectThatIHit, distanceToSee, layerIndex) && Input.GetKey("space"))
        {
            //Debug.Log("Hit interactable object");

            playerManager.HandleInteractables(_objectThatIHit.collider.gameObject.tag, _objectThatIHit.collider.gameObject.name);
        }
    }
}