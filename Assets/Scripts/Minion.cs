using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    public enum State { Idle, Follow }

    [SerializeField] private Animator anim = default;

    [HideInInspector]
    public NavMeshAgent agent = default;
    public State state = default;
    private Coroutine updateTarget = default;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (state == State.Follow)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                anim.SetBool("isWalking", false);
            }
            else
            {
                anim.SetBool("isWalking", true);
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    public void SetTarget(Transform target, float updateTime = 1f)
    {
        state = State.Follow;
        if (updateTarget != null)
            StopCoroutine(updateTarget);

        WaitForSeconds wait = new WaitForSeconds(updateTime);
        updateTarget = StartCoroutine(UpdateTarget());

        IEnumerator UpdateTarget()
        {
            while (true)
            {
                if (agent.enabled)
                    agent.SetDestination(target.position);
                yield return wait;
            }
        }
    }

    public void SetIdle()
    {
        state = State.Idle;
        if (updateTarget != null)
            StopCoroutine(updateTarget);

        agent.ResetPath();
    }
}
