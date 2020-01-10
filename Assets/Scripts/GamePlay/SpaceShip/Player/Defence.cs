using UnityEngine;

public class Defence : MonoBehaviour
{
    #region Fields

    private bool _shield;

    #endregion

    #region Properties

    public bool ShieldActivated { get => _shield; }

    #endregion


    #region Methods

    protected virtual void Awake()
    {
        _shield = false;
    }

    public void ActivateShield()
    {
        _shield = true;
    }

    public void DeactivateShield()
    {
        _shield = false;
    }

    #endregion
}
