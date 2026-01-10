using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public float detectRange = 20f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    float lastAttackTime;
    bool isAttacking;

    Transform player;
    NavMeshAgent agent;
    Animator animator;

    Vector3 lastPlayerPos;
    bool hasSeenPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        Debug.Log($"Distance: {distance}, isAttacking: {isAttacking}, Cooldown ready: {Time.time - lastAttackTime >= attackCooldown}");

        if (distance <= detectRange)
        {
            if (distance <= attackRange)
            {
                agent.isStopped = true;

                // Look at player
                Vector3 direction = player.position - transform.position;
                direction.y = 0;
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
                }

                if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
                {
                    StartAttack();
                }
            }
            else if (!isAttacking) // Chỉ di chuyển nếu không đang tấn công
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }

            // Update animation
            animator.SetBool("isRunning", !agent.isStopped && agent.velocity.magnitude > 0.1f);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("isRunning", false);
        }
    }

    void StartAttack()
    {
        Debug.Log("Starting Attack Sequence");
        isAttacking = true;
        lastAttackTime = Time.time;
        animator.SetTrigger("Attack");
        animator.SetBool("isRunning", false);

        // Tự động reset attack state sau 1 giây (fallback nếu animation event fail)
        Invoke("ForceEndAttack", 1f);
    }

    void ForceEndAttack()
    {
        if (isAttacking)
        {
            Debug.LogWarning("Force ending attack - animation event might have failed");
            EndAttack();
        }
    }

    public void EndAttack()
    {
        Debug.Log("Attack ended normally");
        isAttacking = false;
        CancelInvoke("ForceEndAttack");
    }
}