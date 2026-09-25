using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using System;

public class VCamManager : MonoBehaviour
{
    public static VCamManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        onCameraChange?.Invoke();
    }

    [SerializeField] private List<CinemachineCamera> cinemachineCameras = new List<CinemachineCamera>();
    [SerializeField] private int index;
    public int Index => index;

    public event Action onCameraChange;


    public void ChangeCamera(int idx)
    {
        int allCamera = cinemachineCameras.Count;

        if(idx > allCamera - 1)
        {
            return;
        }

        for (int i = 0; i < allCamera; i++)
        {
            if(i == idx)
            {
                cinemachineCameras[i].Priority = 5;
            }
            else
            {
                cinemachineCameras[i].Priority = 1;
            }
        }
    }
    public void ChangeToNextCamera()
    {
        int allCamera = cinemachineCameras.Count;

        if (index >= allCamera - 1)
        {
            return;
        }
        
        index++;

        for (int i = 0; i < allCamera; i++)
        {
            if (i == index)
            {
                cinemachineCameras[i].Priority = 5;
            }
            else
            {
                cinemachineCameras[i].Priority = 1;
            }
        }
        onCameraChange?.Invoke();
    }
    public void ChangeToPreviousCamera()
    {
        int allCamera = cinemachineCameras.Count;

        if (index <= 0)
        {
            return;
        }

        index--;

        for (int i = 0; i < allCamera; i++)
        {
            if (i == index)
            {
                cinemachineCameras[i].Priority = 5;
            }
            else
            {
                cinemachineCameras[i].Priority = 1;
            }
        }
        onCameraChange?.Invoke();
    }

}
