using UnityEngine;
using UnityEngine.AI;

public class TroopController : MonoBehaviour
{
    public Troop troop;
    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(troop.DetectClosestEnemy().transform.position);
    }
}
