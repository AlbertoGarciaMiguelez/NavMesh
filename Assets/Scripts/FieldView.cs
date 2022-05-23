using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;
    public Transform house;
    public Transform player;
    AgentState state;
    UnityEngine.AI.NavMeshAgent agent;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        house = GameObject.FindGameObjectWithTag("Respawn").transform;
        state= AgentState.Stop;
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)){
                    canSeePlayer = true;
                }
                else{
                    canSeePlayer = false;
                }
            }
            else{
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
            canSeePlayer = false;
        if(canSeePlayer){
            SetState(AgentState.Chasing);
            Debug.Log("ver");
        }else{
            Debug.Log("No ver");
            if(transform.position.x==house.position.x && transform.position.z==house.position.z){
                SetState(AgentState.Stop);
            }else{
                SetState(AgentState.Returning);
            }
        }
    }
    void SetState(AgentState newState){
        if(newState != state){
            state=newState;
            switch(state){
                case AgentState.Stop:
                    Debug.Log(AgentState.Stop);
                    break;
                case AgentState.Chasing:
                    agent.destination = player.position;
                    Debug.Log(AgentState.Chasing);
                    break;
                case AgentState.Returning:
                    agent.destination = house.position;
                    Debug.Log(AgentState.Returning);
                    break;
            }
        }
    }
    public enum AgentState{
        Stop,
        Chasing,
        Returning
    }
}
