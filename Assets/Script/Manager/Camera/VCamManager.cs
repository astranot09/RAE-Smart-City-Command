using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class VCamManager : MonoBehaviour
{
    public static VCamManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private List<CinemachineCamera> cinemachineCameras = new List<CinemachineCamera>();

    public void ChangeCamera(int index)
    {

    }

}
