using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.InputSystem;

public class RollerBallAgent : Agent
{
    private Rigidbody body;
    public Transform target;
    [SerializeField] private float forceMultiplier = 10f;

    public override void Initialize()
    {
        body = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
       
        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

       
        transform.localPosition = new Vector3(0f, 0.5f, 0f);

     
        target.localPosition = new Vector3(Random.Range(-3.5f, 3.5f), 0.5f, Random.Range(-3.5f, 3.5f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
       
        sensor.AddObservation(target.localPosition); 
        sensor.AddObservation(transform.localPosition); 
        sensor.AddObservation(body.linearVelocity.x); 
        sensor.AddObservation(body.linearVelocity.z); 
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
      
        var continuousActionVector = actions.ContinuousActions;
        Vector3 force = new Vector3(continuousActionVector[0], 0f, continuousActionVector[1]) * forceMultiplier;
        body.AddForce(force, ForceMode.Force);

        
        if (Vector3.Distance(transform.localPosition, target.localPosition) < 1.4f)
        {
            SetReward(1f); 
            EndEpisode(); 
        }
     
        else if (transform.localPosition.y < -1f)
        {
            EndEpisode(); 
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        
        var keys = Keyboard.current;
        var continuousActionsOut = actionsOut.ContinuousActions;

        if (keys == null) return;

        float x = 0f;
        float z = 0f;

        if (keys.dKey.isPressed) x = 1f;
        if (keys.aKey.isPressed) x = -1f;
        if (keys.wKey.isPressed) z = 1f;
        if (keys.sKey.isPressed) z = -1f;

        continuousActionsOut[0] = x;
        continuousActionsOut[1] = z;
    }
}