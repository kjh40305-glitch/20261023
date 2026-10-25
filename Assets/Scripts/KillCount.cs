using TMPro;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private TextMeshProUGUI _killText;

    private void Update()
    {
        if (_player != null)
        {
            _killText.text = $"Kills: {_player.killCount}";
        }
    }
}
