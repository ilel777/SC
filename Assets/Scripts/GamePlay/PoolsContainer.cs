using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsContainer
{
    #region Fields

    static PoolsContainer _instance;
    BoltPool _bolts;

    #endregion

    #region Accessors

    public static BoltPool Bolts { get => _instance._bolts; }

    #endregion

    #region Methods

    public static void Initialize()
    {
        if (_instance != null) return;
        _instance = new PoolsContainer();
    }

    #endregion

    #region Constructors

    PoolsContainer()
    {
        _bolts = new BoltPool(100);
    }

    #endregion
}
