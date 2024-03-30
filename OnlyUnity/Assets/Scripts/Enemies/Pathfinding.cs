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
    [Tooltip("The distance it needs between the agent and the target to consider it as reached")][SerializeField] private float endTargetmargin = 1f;
    [Tooltip("The position in worldSpace that the agent will return to when it can't find the player anymore")][SerializeField] private Vector3 startPosition;

    [Space]
    [Header("Testing")]
    [Tooltip("")] [SerializeField] private Collider[] detectedPlayers;

    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        InvokeRepeating("CheckForPlayer", 0.5f, pathfindinginterval);
        startPosition = transform.position;
        
    }
    private void CheckForPlayer()
    {
        detectedPlayers = null;
        detectedPlayers = Physics.OverlapSphere(transform.position, detectionRadius, layerMask);
        if (detectedPlayers.Length > 0)
        {
            player = detectedPlayers[0].transform;
            Debug.Log("Found player");

            if (Physics.Raycast(transform.position, player.position - transform.position, out RaycastHit hitInfo, Vector3.Distance(player.position, transform.position) + 10, layerMask2))
            {
                Debug.Log("Hit Target1");
                if (/*hitInfo.transform.gameObject.layer == .... om saken raycasten träffade har layern Player --- (1 << hitInfo.transform.gameObject.layer) == layerMask.value*/ hitInfo.transform == player)
                {
                    Debug.Log("Target is player");
                    //sätt det träffade objectet som våran target och sedan kalla på metoden nedan
                    player = hitInfo.transform;
                    awareOfThePlayer = true;
                    PathfindToPlayer();
                }
                else
                {
                    if (agent.remainingDistance <= endTargetmargin)
                    {
                        awareOfThePlayer = false;
                        agent.destination = startPosition;
                    }
                }
            }
            else if (awareOfThePlayer)
            {
                if (agent.remainingDistance <= endTargetmargin)
                {
                    awareOfThePlayer = false;
                    agent.destination = startPosition;
                }
            }
            else
            {
                if (agent.remainingDistance <= endTargetmargin)
                {
                    agent.destination = startPosition;
                }
            }
        }
        else if (awareOfThePlayer)
        {
            if (Physics.Raycast(transform.position, player.position - transform.position, out RaycastHit hitInfo, Vector3.Distance(player.position, transform.position) + 10, layerMask2))
            {
                Debug.Log("Hit Target2");
                if (/*hitInfo.transform.gameObject.layer == .... om saken raycasten träffade har layern Player --- (1 << hitInfo.transform.gameObject.layer) == layerMask.value*/ hitInfo.transform == player)
                {
                    Debug.Log("Found player but putside sphere");
                    PathfindToPlayer();
                }
                else if (agent.remainingDistance <= endTargetmargin)
                {
                        awareOfThePlayer = false;
                        agent.destination = startPosition;
                        //Om vi når våran destination där vi såg spelaren men kan inte se hen nu så väntar vi i x sekunder sedan återvänder vi till våran orginella plats
                }
            }
        }
        else if (!awareOfThePlayer && agent.remainingDistance <= endTargetmargin)
        {
            agent.destination = startPosition;
        }
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
