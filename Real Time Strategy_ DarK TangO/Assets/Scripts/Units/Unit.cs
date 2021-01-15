using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField] private UnityEvent  OnSelect=null;
    [SerializeField] private UnityEvent  OnDeselect =null;

    [SerializeField] private UnitMovement unitMovement = null;
    
    public UnitMovement GetUnitMovement()
    {
        return unitMovement;
    }

    #region Client
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
