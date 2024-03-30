using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Pathfinding : MonoBehaviour
{

    [Tooltip("True if the enemy knows where the player is")][SerializeField] private bool awareOfThePlayer;

    [Space]
    [Header("Detection")]
    [Tooltip("The radius of which the enemy can detect the player")][SerializeField] private float detectionRadius;
    [Tooltip("The layers of which the enemy can detect things on")][SerializeField] private LayerMask layerMask;
    [Tooltip("The layers to ignore when checking if the player is in line of sight")][SerializeField] private LayerMask layerMask2;

    [Space]
    [Header("Pathfinding")]
    [Tooltip("The player transform. This variable automatically gets a value whenever the player is detected")][SerializeField] private Transform player;
    [Tooltip("The reference to the navmesh agent")][SerializeField] private NavMeshAgent agent;
    [Tooltip("The time between eavh attempt to find the player and calculate a new path")][SerializeField] private float pathfindinginterval = 0.5f;

    [Space]
    [Header("Testing")]
    [Tooltip("")] [SerializeField] private Collider[] detectedPlayers;

    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        InvokeRepeating("CheckForPlayer", 0.5f, pathfindinginterval);
        
    }
    private void CheckForPlayer()
    {
        Debug.Log("test");
        Debug.Log(transform.position);

        detectedPlayers = Physics.OverlapSphere(transform.position, detectionRadius, layerMask);
        if (detectedPlayers[0] != null)
        {
            player = detectedPlayers[0].transform;
            Debug.Log("Found player");

            if (Physics.Raycast(transform.position, player.position - transform.position, out RaycastHit hitInfo, Vector3.Distance(player.position, transform.position) + 10, layerMask2))
            {
                if (/*hitInfo.transform.gameObject.layer == .... om saken raycasten tr�ffade har layern Player --- (1 << hitInfo.transform.gameObject.layer) == layerMask.value*/ hitInfo.transform == player)
                {
                    //s�tt det tr�ffade objectet som v�ran target och sedan kalla p� metoden nedan
                    player = hitInfo.transform;
                    PathfindToPlayer();
                }
            }
        }
        else
        {
            Debug.Log("Player Not found");
        }

        //if (Physics.SphereCast(transform.position, detectionRadius, Vector3.forward, out RaycastHit hitInfo, 0.01f, layerMask, QueryTriggerInteraction.Ignore))
        //{
        //    Debug.Log("Found player");
        //    player = hitInfo.transform;
            
        //    if (Physics.Raycast(transform.position, player.position - transform.position, out hitInfo, Vector3.Distance(player.position, transform.position) + 10, layerMask2))
        //    {
        //        if (/*hitInfo.transform.gameObject.layer == .... om saken raycasten tr�ffade har layern Player --- (1 << hitInfo.transform.gameObject.layer) == layerMask.value*/ hitInfo.transform == player)
        //        {
        //            //s�tt det tr�ffade objectet som v�ran target och sedan kalla p� metoden nedan
        //            player = hitInfo.transform;
        //            PathfindToPlayer();
        //        }
        //    }
        //}
        //else
        //{
        //    Debug.Log("Player Not found");
        //}
        
        //else if (awareOfThePlayer)
        //{
        //    if (/*destination reache*/ true)
        //    {
        //        //Om vi n�r v�ran destination d�r vi s�g spelaren men kan inte se hen nu s� v�ntar vi i x sekunder sedan �terv�nder vi till v�ran orginella plats
        //    }
        //}
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.LookAt(new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z));
    }

    private void PathfindToPlayer()
    {
        agent.destination = player.position;
    }
    private void OnDrawGizmosSelected()
    {
        // Set the color of the SphereCast visualization
        Gizmos.color = Color.red;

        // Perform the SphereCast
        if (Physics.SphereCast(transform.position, detectionRadius, Vector3.forward, out RaycastHit hitInfo, 0.01f, layerMask))
        {
            // Draw a line from the current position to the point of contact
            Gizmos.DrawLine(transform.position, hitInfo.point);

            // Draw a small sphere at the point of contact
            Gizmos.DrawSphere(hitInfo.point, 0.1f);
        }
        else
        {
            // If no collision, just draw a SphereCast from the current position
            Gizmos.DrawWireSphere(transform.position + Vector3.forward * 0.01f, detectionRadius);
        }
    }
}
