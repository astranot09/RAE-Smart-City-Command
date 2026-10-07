using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance;

    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private Transform notificationTransform;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void CallNotification(string x)
    {
        GameObject y = Instantiate(notificationPrefab,notificationTransform);
        y.GetComponent<NotificationPrefab>().NotificationSetUp(x);
        Debug.Log(x);
    }
}
