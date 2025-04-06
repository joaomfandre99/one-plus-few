using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator = default;

    public void OpenDoor()
    {
        doorAnimator.Play("DoorOpen", 0, 0.0f);
    }
}
