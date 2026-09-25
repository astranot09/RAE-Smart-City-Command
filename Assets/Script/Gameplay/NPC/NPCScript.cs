using System.Collections;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public enum NPCState
{
    Wandering,
    Evacuation,
    BackFromEvacuation,
    Dead
}

public class NPCScript : MonoBehaviour
{
    private bool inEvacuationArea = false;
    public bool InEvacuationArea => inEvacuationArea;

    [SerializeField] private NPCState currentState = NPCState.Wandering;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stopDistance = 0.5f;
    [SerializeField] private Transform evacLoc;

    private Rigidbody2D rb;
    private Vector3 spawnPosition;

    [Header("Wandering")]
    [SerializeField] private float waderingSpeed = 2f;
    [SerializeField] private float minXLoc;
    [SerializeField] private float maxXLoc;
    [SerializeField] private float minWanderDistance = 3f;

    [Header("Reference")]
    private OutpostManager outpostManager;
    private SpriteRenderer spriteRenderer;
    private Coroutine wanderCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Effect fade-in saat awal spawn
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        spawnPosition = transform.position;
        spriteRenderer.DOFade(1f, 0.5f);

        // Langsung jalankan logika wandering sejak awal
        if (currentState == NPCState.Wandering)
        {
            wanderCoroutine = StartCoroutine(WanderingRoutine());
        }
    }

    public void SetUp(OutpostManager manager, Transform evacuationLocation, float minWandering, float maxWandering)
    {
        outpostManager = manager;
        evacLoc = evacuationLocation;
        minXLoc = minWandering;
        maxXLoc = maxWandering;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        switch (currentState)
        {
            case NPCState.Wandering:
                
                break;

            case NPCState.Evacuation:
                if (evacLoc == null) return;
                MoveToTargetX(evacLoc.position.x);
                break;

            case NPCState.BackFromEvacuation:
                // Jika sudah kembali sampai lokasi awal, kembalikan ke Wandering
                if (MoveToTargetX(spawnPosition.x))
                {
                    if (outpostManager != null)
                        outpostManager.NPCBackFromEvacuateArea();

                    ChangeState(NPCState.Wandering);
                }
                break;

            case NPCState.Dead:
                rb.linearVelocity = Vector2.zero;
                break;
        }
    }

    private bool MoveToTargetX(float targetX)
    {
        float distanceX = targetX - transform.position.x;

        if (Mathf.Abs(distanceX) > stopDistance)
        {
            float directionX = Mathf.Sign(distanceX);

            if(currentState == NPCState.Wandering)
            {
                rb.linearVelocity = new Vector2(directionX * waderingSpeed, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(directionX * moveSpeed, rb.linearVelocity.y);
            }

            // Membalikkan arah visual sprite (facing left/right)
            if (directionX != 0)
                spriteRenderer.flipX = directionX < 0;

            return false;
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return true;
        }
    }

    public void ChangeState(NPCState newState)
    {
        if (currentState == newState) return;

        // Hentikan coroutine jalan terus jika berpindah state (misal: Evakuasi/Mati)
        if (wanderCoroutine != null)
        {
            StopCoroutine(wanderCoroutine);
            wanderCoroutine = null;
        }

        currentState = newState;

        // Jalankan kembali coroutine jika masuk ke state Wandering
        if (currentState == NPCState.Wandering)
        {
            wanderCoroutine = StartCoroutine(WanderingRoutine());
        }
    }

    private IEnumerator WanderingRoutine()
    {
        while (currentState == NPCState.Wandering)
        {
            // Ambil titik tujuan acak baru
            float targetX = Random.Range(minXLoc - 1, maxXLoc + 1);
            Mathf.Clamp(targetX, minXLoc, maxXLoc);

            // Terus berjalan frame demi frame hingga sampai ke targetX
            while (!MoveToTargetX(targetX) && currentState == NPCState.Wandering)
            {
                yield return new WaitForFixedUpdate();
            }

            // Setelah sampai target, langsung loop ke atas untuk mencari target baru (tanpa jeda/idle)
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EvacuateArea"))
        {
            if (outpostManager != null)
                outpostManager.NPCEscaped();

            inEvacuationArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EvacuateArea"))
        {
            inEvacuationArea = false;
        }
    }

    public void NPC_Dead()
    {
        ChangeState(NPCState.Dead);
        Destroy(gameObject);
    }
}