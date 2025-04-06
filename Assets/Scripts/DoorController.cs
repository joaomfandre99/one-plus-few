using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator anim = default;
    [SerializeField] private SceneAsset nextScene = default;

    public void OpenDoor()
    {
        anim.SetBool("isOpen", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextScene.name);
        }
    }
}
