using UnityEngine;

/// <summary>
/// The state in which the camera follows the target as long as it is in sight
/// </summary>
public class FollowState : StateMachineBehaviour
{
    GameObject player;

    /// <summary>
    /// Get a reference to the Player game object (yes, there's a lot of debate on performance of the various Find...() methods
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");
        animator.GetComponent<Renderer>().material.color = Color.red;
    }

    /// <summary>
    /// If the camera NO LONGER sees the player, move to "LostState"
    /// Otherwise, RotateTowards the player gameobject 
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!SeesEnemy(animator))
        {
            animator.SetInteger("CameraState", 2);
        }
        else
        {
            Vector3 targetDirection = player.transform.position - animator.transform.position;
            targetDirection.y = 0; // disregard positional difference on y-axis
            Vector3 viewDirection = Vector3.RotateTowards(animator.transform.forward, targetDirection, 1f, 0f);
            animator.transform.rotation = Quaternion.LookRotation(viewDirection);
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
