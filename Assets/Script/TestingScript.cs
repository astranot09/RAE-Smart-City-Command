using UnityEngine;

public class TestingScript : MonoBehaviour
{
    [SerializeField] int index;

    public void ChangeCam()
    {
        VCamManager.instance.ChangeCamera(index);
    }
}
