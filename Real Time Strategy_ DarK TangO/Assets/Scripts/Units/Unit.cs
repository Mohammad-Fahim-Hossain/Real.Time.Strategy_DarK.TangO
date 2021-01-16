using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class Unit : NetworkBehaviour
{
    [SerializeField] private UnityEvent OnSelect = null;
    [SerializeField] private UnityEvent OnDeselect = null;

    [SerializeField] private UnitMovement unitMovement = null;

    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDespawned;

    public static event Action<Unit> AuthorityOnUnitSpawned;
    public static event Action<Unit> AuthorityOnUnitDespawned;

    #region Server
    public override void OnStartServer()
    {
        ServerOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnUnitDespawned?.Invoke(this);
    }
    #endregion


    #region Client

    public override void OnStartClient()
    {
        if (!isClientOnly || !hasAuthority) { return; }

        AuthorityOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopClient()
    {
        if (!isClientOnly || !hasAuthority) { return; }
        AuthorityOnUnitDespawned?.Invoke(this);
    }

    public UnitMovement GetUnitMovement()
    {
        return unitMovement;
    }

   
    [Client]
    public void Select()
    {
        if (!hasAuthority) { return; }
        OnSelect?.Invoke();
    }

    [Client]
    public void Deselect()
    {
        if (!hasAuthority) { return; }
        OnDeselect?.Invoke();
    }
    #endregion

}
