using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Extensions;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AgentOverride2d))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Mover : MoverBase
{
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private Transform body;

    private NavMeshAgent agent;

    //get the navmeshagent on gameobject and set the update rotation to false
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }
    
    /// <summary>
    /// set the agent to move and set the destination target to new Vector2
    /// </summary>
    /// <param name="position">Vector2</param>
    public void SetMovementTarget(Vector2 position)
    {
        agent.isStopped = false;
        agent.SetDestination(position);
    }

    /// <summary>
    /// stops current movement
    /// </summary>
    public void CancelMovement()
    {
        agent.isStopped = true;
    }
    
    /// <summary>
    /// get the current radius of gameobject
    /// </summary>
    /// <returns>float</returns>
    public float GetRadius()
    {
        return radius;
    }

    /// <summary>
    /// sets the radius of the shadow gameobject to fit to the collider
    /// </summary>
    private void OnValidate()
    {
        GetComponent<CircleCollider2D>().radius = radius;
        GetComponent<NavMeshAgent>().radius = radius;
        GetComponent<NavMeshAgent>().height = 0;
        if (body != null)
        {
            body.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
        }
    }
}
