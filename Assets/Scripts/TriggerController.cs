using TMPro;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [SerializeField] private GameObject trigger = default;
    [SerializeField] private int minAmountRequired = 1;
    [SerializeField] private TextMeshPro text = default;
    [SerializeField] private Animator[] anim = default;
    [SerializeField] private bool isPressurePlate = default;

    private int count = 0;

    private void Start()
    {
        SetText();
    }

    private void Update()
    {
        if (!isPressurePlate)
        {
            count = GameManager.Instance.minionCount;
            SetText();
            if (count == minAmountRequired)
            {
                InteractWithTrigger();
            }
        }
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
        SetText();
    }

    private void InteractWithTrigger()
    {
        if (count >= minAmountRequired)
        {
            foreach (var item in anim)
            {
                item.SetBool("isCompleted", true);
            }
            trigger.GetComponent<DoorController>().OpenDoor();

            if (isPressurePlate)
            {
                GetComponent<Collider>().enabled = false;
            }
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
