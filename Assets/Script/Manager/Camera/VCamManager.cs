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
        int allCamera = cinemachineCameras.Count;

        if(index > allCamera - 1)
        {
            return;
        }

        for (int i = 0; i < allCamera; i++)
        {
            if(i == index)
            {
                cinemachineCameras[i].Priority = 5;
            }
            else
            {
                cinemachineCameras[i].Priority = 1;
            }
        }
    }

}
