using System;
using UnityEngine;

public class PointToCollect : MonoBehaviour
{
    private AudioManager audioManager;
    public static int PointToCollectLayer => LayerMask.NameToLayer("PointToCollect");
    public static event Action PointToCollectTriggered;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        PointToCollectTriggered?.Invoke();
    }

    public void Collect(ref int number)
    {
        number++;
        audioManager.PlaySound("PickUpCheese");
        Destroy(gameObject);
    }
}