using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public UnityEvent OnCoinCollide = new UnityEvent();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Destroy(gameObject);
            OnCoinCollide?.Invoke();
        }
    }
}
