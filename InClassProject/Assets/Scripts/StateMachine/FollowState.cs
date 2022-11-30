using UnityEngine;

/// <summary>
/// The state in which the camera follows the target as long as it is in sight
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
public class FollowState : State
{
    GameObject player;

    public delegate void SurveyDelegate(bool doSee);
    public static SurveyDelegate SeeChange;

    /// <summary>
    /// Get a reference to the Player game object (yes, there's a lot of debate on performance of the various Find...() methods
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SeeChange?.Invoke(true);
        
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
            SeeChange?.Invoke(false);
            
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
}
