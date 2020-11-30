using UnityEngine;

/// <summary>
/// The state in which the camera waits to see if the target reappears. If so, transition to follow state
/// If it does not within a certain amount of time, transition to patrol state
///
/// Change from the in-class execution:
/// This state, like all three, extends State which in its turn extends StateMachineBehaviour.
/// This makes it possible to have the duplicated function 'SeesEnemy' in a central location.
/// So that all three State defining classes have access to is and the function does not appear three times in the code base.
/// So, there now is a chain/tree that describes the relationship between the three classes (the last of which is defined in Unity
/// LostState ---\
/// FollowState --  State - StateMachienBehaviour
/// LostState ---/
/// </summary>
public class LostState : State
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
}
