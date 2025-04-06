using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    [SerializeField] private float whistleRadius = 10f;
    [SerializeField] private Transform target = default;
    [SerializeField] private Material highlightMaterial = default;
    [SerializeField] private Material originalMaterial = default;

    private List<Minion> allMinions= new List<Minion>();
    private int controlledMinions = 0;

    private void Start()
    {
        allMinions.AddRange(FindObjectsByType<Minion>(FindObjectsSortMode.None));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FollowWhistle();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            WaitWhistle();
        }

        HighlightMinions();
    }

    private void FollowWhistle()
    {
        foreach (Minion minion in allMinions)
        {
            if (Vector3.Distance(minion.transform.position, transform.position) < whistleRadius && minion.state != Minion.State.Follow)
            {
                minion.SetTarget(target, 0.25f);
                controlledMinions++;
            }
        }
    }
    
    private void WaitWhistle()
    {
        foreach (Minion minion in allMinions)
        {
            if (Vector3.Distance(minion.transform.position, transform.position) < whistleRadius && minion.state != Minion.State.Idle)
            {
                minion.SetIdle();
                controlledMinions--;
            }
        }
    }

    private void HighlightMinions()
    {
        foreach (Minion minion in allMinions)
        {
            if (Vector3.Distance(minion.transform.position, transform.position) < whistleRadius && minion.state == Minion.State.Idle)
            {
                minion.GetComponent<MeshRenderer>().material= highlightMaterial;
            } 
            else
            {
                minion.GetComponent<MeshRenderer>().material = originalMaterial;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, whistleRadius);
    }
}
