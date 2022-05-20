    using UnityEngine;
    using UnityEngine.AI;
    
    public class MoveTo2 : MonoBehaviour {
       
      public Transform ojos;
      public Transform house;
      public Transform player;
      public float rangoVision = 10f;
      NavMeshAgent agent;
      
      void Start () {
         agent = GetComponent<NavMeshAgent>();
         player = GameObject.FindGameObjectWithTag("Player").transform;
         house = GameObject.FindGameObjectWithTag("Respawn").transform;
      }
         void Update()
      {
        RaycastHit hit;
         if(Physics.Raycast(ojos.position, ojos.forward, out hit, rangoVision) && hit.collider.CompareTag("Player")){
            agent.destination = player.position;
            Debug.Log("Hola");
         }else{
            agent.destination = house.position;
            Debug.Log("Hola2");
         }
      }
   }
