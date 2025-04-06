using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject trigger = default;
    [SerializeField] private int minAmountRequired = 1;
    [SerializeField] private TextMeshPro text = default;
    [SerializeField] private Animator anim = default;

    private int count = 0;
    private bool wasActive = false;

    private void Start()
    {
        SetText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            count++;
        } else if (other.CompareTag("Player"))
        {
            count += 2;
        }
        InteractWithTrigger();
        SetText();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            count--;
        }
        else if (other.CompareTag("Player"))
        {
            count -= 2;
        }
        InteractWithTrigger();
        SetText();
    }

    private void InteractWithTrigger()
    {
        bool isActive = count >= minAmountRequired;

        if (isActive && !wasActive)
        {
            wasActive = true;
            anim.SetBool("isCompleted", true);
            trigger.GetComponent<BridgeController>().PlateActivated();
        }
        else if (!isActive && wasActive)
        {
            wasActive = false;
            anim.SetBool("isCompleted", false);
            trigger.GetComponent<BridgeController>().PlateDeactivated();
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
