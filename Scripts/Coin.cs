using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed = 60f;
    [SerializeField] float moveSpeed = 12f;

    private Transform player;
    private bool isMagnetized = false;

    void Update()
    {
        // چرخش سکه
        transform.Rotate(1, turnSpeed * Time.deltaTime, 1);

        if (isMagnetized && player != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );

            // وقتی به بازیکن رسید
            if (Vector3.Distance(transform.position, player.position) < 0.3f)
            {
                GameManager.inst.IncrementScore();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magnet"))
        {
            player = other.transform.root;   // خود Player
            isMagnetized = true;
        }
    }
}