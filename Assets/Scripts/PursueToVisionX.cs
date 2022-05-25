using UnityEngine;
using UnityEngine.AI;

    public class PursueToVisionX : MonoBehaviour {
    
        public Transform ojos;
        public Transform house;
        public Transform player;
        public float rangoVision = 100f;
        NavMeshAgent agent;
        [Range(0f, 360f)]
        public float visionAngle=30f;

        Vector3 v = Vector3.zero;
        public float distance= 0.0f;
        
        void Start () {
            agent = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            house = GameObject.FindGameObjectWithTag("Respawn").transform;
        }
        void Update()
        {
            RaycastHit hit;
            Vector3 directionToTarget = (player.position-transform.position).normalized;
            if(Physics.Raycast(ojos.position, ojos.forward, out hit, rangoVision)){
            agent.destination = player.position;
            }else{
            agent.destination = house.position;
        }
        void OnDrawGizmos(){
            v = player.position-transform.position;
            distance=v.magnitude;
        }
    }
}