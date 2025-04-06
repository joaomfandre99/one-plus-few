using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator anim = default;
    [SerializeField] private string nextSceneName = default;
    [SerializeField] private bool hasNextScene = true;
    [SerializeField] private GameObject canvas = default;

    public void OpenDoor()
    {
        anim.SetBool("isOpen", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasNextScene)
        {
            SceneManager.LoadScene(nextSceneName);
        }

        if (other.CompareTag("Player") && !hasNextScene && canvas != null)
        {
            canvas.SetActive(true);
        }
    }
}
