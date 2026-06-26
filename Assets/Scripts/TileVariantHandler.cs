using InfiniteRunner;
using UnityEngine;

public class TileVariantHandler : MonoBehaviour
{
    [SerializeField] private GameObject _tileVariant1;
    [SerializeField] private GameObject _tileVariant2;
    [SerializeField] private float _targetScoreToTileChange;

    private void Start() 
    {
        _tileVariant1.gameObject.SetActive(true);
        _tileVariant2.gameObject.SetActive(false);
    }

    public void CheckTileHandler() {
        if (GameManager.Instance.Score > _targetScoreToTileChange) {
            _tileVariant1.gameObject.SetActive(false);
            _tileVariant2.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Just in case mau di reference in gameplay initializer, bila di reference, hapus start function diatas
    /// </summary>
    private void Initialize() {
        _tileVariant1.gameObject.SetActive(true);
        _tileVariant2.gameObject.SetActive(false);
    }
}
