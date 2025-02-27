using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;
    private Coin[] coins;

    void Start()
    {
        coins = FindObjectsByType<Coin>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Coin coin in coins) {
            coin.OnCoinCollide.AddListener(IncrementScore);
        }
    }

    private void IncrementScore()
    {
        score++;
        Debug.Log("Score: " + score);
    }
}
