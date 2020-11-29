using UnityEngine;

/// <summary>
/// The state in which the camera waits to see if the target reappears. If so, transition to follow state
/// If it does not within a certain amount of time, transition to patrol state 
/// </summary>
public class LostState : StateMachineBehaviour
{

    [SerializeField]
    float alertDuration;

    float toPatrolStateTime;

    /// <summary>
    /// Set the time at which the camera will move to patrol state
    /// Make the renderer's color be Orange-like
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        toPatrolStateTime = Time.time + alertDuration;
        animator.GetComponent<Renderer>().material.color = new Color(1f, 0.5f, 0f);
    }

    /// <summary>
    /// If the camera 'sees' the player again, transition to state Follow
    /// Otherwise, if the 'alert time' has passed, transition to Patrol state
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (SeesEnemy(animator))
        {
            animator.SetInteger("CameraState", 1);
        }
        else if (Time.time > toPatrolStateTime)
        {
            animator.SetInteger("CameraState", 0);
        }
    }

    /// <summary>
    /// Cast a ray from camera into the distance (12 units here, just for the heck of it).
    /// 
    /// Returns true if the resulting hit is the player (has the Player component).
    /// Returns false otherwise.
    /// </summary>
    /// <param name="animator">The Animator this State Machine Behaviour belongs to</param>
    /// <returns></returns>
    protected bool SeesEnemy(Animator animator)
    {
        bool result = false;
        RaycastHit hit;
        float viewDistance = 12;

        if (Physics.Raycast(animator.transform.position, animator.transform.forward, out hit, viewDistance))
        {
            if (hit.transform.GetComponent<Player>())
            {
                result = true;
            }
        }
        return result;
    }
}
