using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private CutsceneManager cutsceneManager;
    [SerializeField] private PausedManager pausedManager;
    public void OnPlayerClick(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            if(cutsceneManager != null)
            {
                cutsceneManager.OnNextCutscene();
            }
            
        }
    }
    public void OnPlayerPaused(InputAction.CallbackContext ctx)
    {
        Debug.Log("Paused1");
        if (ctx.canceled && pausedManager != null)
        {
            Debug.Log("Paused");
            pausedManager.OpenPausePanel();
        }
    }
}
