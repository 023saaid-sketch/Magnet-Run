using UnityEngine;

public class CheckpointGate : MonoBehaviour
{
    public int requiredScore = 20;

    private bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (used)
            return;

        if (!other.CompareTag("Player"))
            return;

        used = true;

        StartCoroutine(GameManager.inst.PassCheckpoint(gameObject, requiredScore));
    }
}