using UnityEngine;

/// <summary>
/// The state in which the camera makes its rounds
/// It rotates until it 'sees' the player Game object. In that case, it switches state
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
public class PatrolState : State
{

    [SerializeField]
    float rotationSpeed;

    /// <summary>
    /// When this state becomes active, the Renderer component is told to go Green
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Renderer>().material.color = Color.green;
    }

    /// <summary>
    /// Just rotate.
    /// And... Check if the player is within sight (here done 
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (SeesEnemy(animator))
        {
            animator.SetInteger("CameraState", 1);
        }
    }
}
