using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class CubeAgent : Agent
{
    public Transform coin;
    public Transform target;
    public Transform boundary;
    private Rigidbody agentRb;
    private Rigidbody targetRb;
    private Rigidbody coinRb; // Declare coin's Rigidbody

    public override void Initialize()
    {
        agentRb = GetComponent<Rigidbody>();
        targetRb = target.GetComponent<Rigidbody>();
        coinRb = coin.GetComponent<Rigidbody>(); // Get coin's Rigidbody
    }

    public override void OnEpisodeBegin()
    {
        // Reset agent position and velocity
        transform.localPosition = new Vector3(2.5f, 0.5f, -0.5f);
        agentRb.velocity = Vector3.zero;

        // Instantiate coin randomly (if needed)
        int randomIndex = Random.Range(0,4);
        if (randomIndex <= 1)
        {
            Instantiate(coin, new Vector3(2.5f, 4.5f, 16f), Quaternion.identity);
            coinRb.velocity = new Vector3(2.5f, 0f, -8f);
            Debug.Log("spawn");
        }

        // Change target's velocity along the negative z-axis each episode
        targetRb.velocity = new Vector3(2.5f, 0f, -8f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Observe agent's position
        sensor.AddObservation(transform.localPosition);
    }

    public float jumpForce = 150f;

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Get jump action from agent
        float jumpAction = actionBuffers.DiscreteActions[0];

        // Jump if action is 1
        if (jumpAction == 1f)
        {
            agentRb.AddForce(Vector3.up * jumpForce);
            SetReward(-0.5f);
        }

        // Calculate distance to target
        float distanceToTarget = Vector3.Distance(transform.localPosition, target.localPosition);

        // Check if agent has successfully reached the target
        if (distanceToTarget < 1.42f && transform.localPosition.y > target.localPosition.y)
        {
            SetReward(2.0f); // Reward for successfully reaching the target
            EndEpisode();
        }

        if (GetComponent<Collider>().gameObject == target.gameObject)
            {
                SetReward(-1f); // Penalty for colliding with the target
                EndEpisode();
            }
    }
    
    public override void Heuristic(in ActionBuffers actionsOut)
        {
            var discreteActionsOut = actionsOut.DiscreteActions;
            discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0; // 1 for jump action when space key is pressed
        }
}