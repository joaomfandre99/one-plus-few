using TMPro;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject trigger = default;
    [SerializeField] private int minAmountRequired = 1;
    [SerializeField] private TextMeshPro text = default;
    [SerializeField] private Animator anim = default;

    private int count = 0;

    private void Start()
    {
        SetText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion") || other.CompareTag("Player"))
        {
            count++;
        }
        InteractWithTrigger();
        SetText();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Minion") || other.CompareTag("Player"))
        {
            count--;
        }
        SetText();
    }

    private void InteractWithTrigger()
    {
        if (count >= minAmountRequired)
        {
            trigger.GetComponent<DoorController>().OpenDoor();
            GetComponent<Collider>().enabled = false;
            anim.SetBool("isPressed", true);
        }
    }

    private void SetText()
    {
        if (text != null && count <= minAmountRequired)
        {
            text.text = $"{minAmountRequired - count}";
        }
    }
}
