using UnityEngine;

public class Defence : MonoBehaviour
{
    #region Fields

    private bool _shield;

    #endregion

    #region Properties

    public bool Shield { get => _shield; set => _shield = value; }

    #endregion

    protected virtual void Awake()
    {
        _shield = false;
    }
}
