// MoveTo.cs
    using UnityEngine;
    using UnityEngine.AI;
    
    public class MoveTo : MonoBehaviour {
       
      public Transform house;
      public Transform player;
      NavMeshAgent agent;
      
      void Start () {
         agent = GetComponent<NavMeshAgent>();
         player = GameObject.FindGameObjectWithTag("Player").transform;
         house = GameObject.FindGameObjectWithTag("Respawn").transform;
      }
         void Update()
      {
         RaycastHit hit;
         Vector3 directionToTarget = (player.position-transform.position).normalized;
         if(Physics.Raycast(transform.position, directionToTarget,out hit, 20f)){
            agent.destination = player.position;
            Debug.Log("Hola");
         }else{
            agent.destination = house.position;
            Debug.Log("Hola2");
         }
      }
   }
