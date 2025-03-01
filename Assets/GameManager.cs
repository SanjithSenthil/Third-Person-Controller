using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
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
        scoreText.text = $"Score: {score}";
    }
}
