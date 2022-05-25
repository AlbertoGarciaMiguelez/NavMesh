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
    public Transform posicion2;
    public Transform player;
    public Transform posicion1;
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
        posicion2 = GameObject.FindGameObjectWithTag("Punto2").transform;
        posicion1 = GameObject.FindGameObjectWithTag("Punto1").transform;
        state= AgentState.Chasing;
        StartCoroutine(FOVRoutine());
    }
    void Update(){
        FieldOfViewCheck();
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait2 = new WaitForSeconds(2.5f);
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            if(state==AgentState.StopPosicion1 || state==AgentState.StopPosicion2){
                yield return wait2;
                Acciones();
            }else{
                Acciones();   
            }
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
        }
    }
    private void Acciones(){
        if(canSeePlayer){
            Debug.Log("ver");
        }else{
            Debug.Log("No ver");
            switch(state){
                case AgentState.Chasing:
                    SetState(AgentState.Returning);
                    break;
                case AgentState.StopPosicion1:
                    SetState(AgentState.Returning);
                    break;
                case AgentState.StopPosicion2:
                    SetState(AgentState.Centinela);
                    break;
            }
            if(transform.position.x==posicion1.position.x && transform.position.z==posicion1.position.z){
                SetState(AgentState.StopPosicion1);
            }
            else if(transform.position.x==posicion2.position.x && transform.position.z==posicion2.position.z){
                SetState(AgentState.StopPosicion2);
            }else if(state==AgentState.StopPosicion1 && !(transform.position.x==posicion1.position.x && transform.position.z==posicion1.position.z) && !(transform.position.x==posicion2.position.x && transform.position.z==posicion2.position.z)){
                SetState(AgentState.Returning);
                Debug.Log("Esto no pasa");
            }
        }
    }
    void SetState(AgentState newState){
        if(newState != state){
            state=newState;
            switch(state){
                case AgentState.StopPosicion1:
                    Debug.Log(AgentState.StopPosicion1);
                    break;
                case AgentState.StopPosicion2:
                    Debug.Log(AgentState.StopPosicion2);
                    break;
                case AgentState.Chasing:
                    agent.destination = player.position;
                    Debug.Log(AgentState.Chasing);
                    break;
                case AgentState.Returning:
                    agent.destination = posicion2.position;
                    Debug.Log(AgentState.Returning);
                    break;
                case AgentState.Centinela:
                    agent.destination = posicion1.position;
                    Debug.Log("Llegando");
                    Debug.Log(AgentState.Centinela);
                break;
            }
        }
    }
    public enum AgentState{
        StopPosicion1,
        StopPosicion2,
        Chasing,
        Returning,
        Centinela
    }
}
