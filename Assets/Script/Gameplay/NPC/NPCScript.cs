using UnityEngine;

[System.Serializable]
public enum NPCState
{
    Idle,
    Evacuation,
    BackFromEvacuation,
    Dead
}


public class NPCScript : MonoBehaviour
{
    bool inEvacuationArea = false;
    public bool InEvacuationArea => inEvacuationArea;

    private NPCState currentState = NPCState.Idle;

    [Header("Reference")]
    private OutpostManager outpostManager;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("EvacuateArea"))
        {
            inEvacuationArea = true;

        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case NPCState.Idle:
                break;
            case NPCState.Evacuation:
                break;
            case NPCState.BackFromEvacuation:
                break;
        }
    }

    public void SetUp()
    {

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
