using UnityEngine;
using DG.Tweening;

[System.Serializable]
public enum NPCState
{
    Idle,
    Evacuation,
    BackFromEvacuation,
    MoveAround,
    Dead
}


public class NPCScript : MonoBehaviour
{
    bool inEvacuationArea = false;
    public bool InEvacuationArea => inEvacuationArea;

    [SerializeField] private NPCState currentState = NPCState.Idle;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stopDistance = 0.5f; // Jarak toleransi berhenti saat sampai target
    private Rigidbody2D rb;
    [SerializeField] private Transform evacLoc;

    private Vector3 spawnPosition;

    [Header("Reference")]
    private OutpostManager outpostManager;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        spawnPosition = transform.position;
        spriteRenderer.DOFade(1f, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EvacuateArea"))
        {
            outpostManager.NPCEscaped();
            inEvacuationArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EvacuateArea"))
        {
            //outpostManager.NPCLeaveEvacuateArea();
            inEvacuationArea = false;
        }
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        switch (currentState)
        {
            case NPCState.Idle:
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
                break;

            case NPCState.Evacuation:
                if (evacLoc == null) return;

                // Pergerakan menuju Evacuation Location
                MoveToTargetX(evacLoc.position.x);
                break;

            case NPCState.BackFromEvacuation:
                // Pergerakan kembali ke Spawn Position
                MoveToTargetX(spawnPosition.x);
                break;

            case NPCState.Dead:
                rb.linearVelocity = Vector2.zero;
                break;
        }
    }
    private void MoveToTargetX(float targetX)
    {
        // Formula pergerakan: Target - Posisi Sekarang
        float distanceX = targetX - transform.position.x;

        if (Mathf.Abs(distanceX) > stopDistance)
        {
            float directionX = Mathf.Sign(distanceX);
            rb.linearVelocity = new Vector2(directionX * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            // Sampai di tujuan
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

            // Jika sudah selesai kembali dari evakuasi, ubah state kembali ke Idle
            if (currentState == NPCState.BackFromEvacuation)
            {
                outpostManager.NPCBackFromEvacuateArea();
                ChangeState(NPCState.Idle);
            }
        }
    }
    public void SetUp(OutpostManager x, Transform evacLocation)
    {
        outpostManager = x;
        evacLoc = evacLocation;
    }

    public void ChangeState(NPCState state)
    {
        currentState = state;
    }
    public void NPC_Dead()
    {
        Destroy(gameObject);
    }
}
