using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RTSPlayer : NetworkBehaviour
{
    [SerializeField ]private List<Unit> MyUnits = new List<Unit>() ;

    #region server
    public override void OnStartServer()
    {
        Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned += ServerHandleUnitDespawned;
    }

    public override void OnStopServer()
    {
        Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned -= ServerHandleUnitDespawned;
    }

    public void ServerHandleUnitSpawned(Unit unit)
    {
        if (unit.connectionToClient.connectionId != this.connectionToClient.connectionId) { return; }

        MyUnits.Add(unit);
    }

    public void ServerHandleUnitDespawned(Unit unit)
    {
        if (unit.connectionToClient.connectionId != this.connectionToClient.connectionId) { return; }

        MyUnits.Remove(unit);

    }
    #endregion


    #region Client
    public override void OnStartClient()
    {
        if (!isClientOnly) { return; }

        Unit.AuthorityOnUnitSpawned += AuthorityHandleUnitSpawned;
        Unit.AuthorityOnUnitDespawned += AuthorityHandleUnitDespawned;
    }

    public override void OnStopClient()
    {
        if (!isClientOnly) { return; }
        Unit.AuthorityOnUnitSpawned -= AuthorityHandleUnitSpawned;
        Unit.AuthorityOnUnitDespawned -= AuthorityHandleUnitDespawned;
    }

    public void AuthorityHandleUnitSpawned(Unit unit)
    {
        if (!hasAuthority) { return; }

        MyUnits.Add(unit);
    }

    public void AuthorityHandleUnitDespawned(Unit unit)
    {
        if (!hasAuthority) { return; }

        MyUnits.Remove(unit);

    }




    #endregion
}
