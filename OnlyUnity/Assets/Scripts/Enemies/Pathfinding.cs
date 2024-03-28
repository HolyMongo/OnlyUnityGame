using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Pathfinding : MonoBehaviour
{

    [Tooltip("true if the enemy knows where the player is")][SerializeField] private bool awareOfThePlayer;

    [Space]
    [Header("Detection")]
    [Tooltip("The radius of which the enemy can detect the player")][SerializeField] private float detectionRadius;
    [Tooltip("The layers of which the enemy can detect things on")][SerializeField] private LayerMask layerMask;
    [Tooltip("The layers to ignore when checking if the player is in line of sight")][SerializeField] private LayerMask layerMask2;

    [Space]
    [Header("Pathfinding")]
    [Tooltip("the player transform. This variable automatically gets a value whenever the player is detected")][SerializeField] private Transform player;
    [Tooltip("the reference to the navmesh agent")][SerializeField] private NavMeshAgent agent;
    [Tooltip("The time between eavh attempt to find the player and calculate a new path")][SerializeField] private float pathfindinginterval = 0.5f;

    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        InvokeRepeating("CheckForPlayer", 0.5f, pathfindinginterval);
    }
    private void CheckForPlayer()
    {
        Physics.CheckSphere(transform.position, detectionRadius, layerMask);
        if (Physics.CheckSphere(transform.position, detectionRadius, layerMask))
        {
            if (Physics.Raycast(transform.position, player.position - transform.position, out RaycastHit hitInfo, Vector3.Distance(player.position, transform.position) + 10, layerMask2))
            {
                if (/*hitInfo.transform.gameObject.layer == .... om saken raycasten träffade har layern Player*/true)
                {
                    //sätt det träffade objectet som våran target och sedan kalla på metoden nedan
                    PathfindToPlayer();
                }
            }
        }
        else if (awareOfThePlayer)
        {
            if (/*destination reache*/ true)
            {
                //Om vi når våran destination där vi såg spelaren men kan inte se hen nu så väntar vi i x sekunder sedan återvänder vi till våran orginella plats
            }
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
}
