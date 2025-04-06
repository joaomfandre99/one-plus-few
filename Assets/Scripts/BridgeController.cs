using UnityEngine;

public class BridgeController : MonoBehaviour
{
    [SerializeField] private Animator anim = default;
    [SerializeField] private GameObject[] obstacles = default;

    private int activePlates = 0;

    public void PlateActivated()
    {
        activePlates++;
        if (activePlates == 1)
        {
            OpenBridge();
        }
    }

    public void PlateDeactivated()
    {
        activePlates--;
        if (activePlates <= 0)
        {
            CloseBridge();
        }
    }

    private void OpenBridge()
    {
        anim.SetBool("isOn", true);
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }
    }

    private void CloseBridge()
    {
        anim.SetBool("isOn", false);
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(true);
        }
    }
}
