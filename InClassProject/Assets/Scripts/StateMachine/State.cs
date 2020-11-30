using UnityEngine;

/// <summary>
/// The state in which the camera makes its rounds
/// It rotates until it 'sees' the player Game object. In that case, it switches state
/// </summary>
public abstract class State : StateMachineBehaviour
{
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
