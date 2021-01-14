using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.EventSystems;

public class UnitsSpawner : NetworkBehaviour,IPointerClickHandler
{
    [SerializeField] private GameObject UnitPrefab = null;
    [SerializeField] private Transform UnitPos = null;

  


    #region Server
    [Command]
    private void CmdUnitSpawn()
    {
        GameObject UnitInstance = GameObject.Instantiate(UnitPrefab, UnitPos.transform.position, UnitPos.transform.rotation);

        NetworkServer.Spawn(UnitInstance, connectionToClient);
    }

    #endregion

    #region Client

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }
        if (!hasAuthority) { return; }

        CmdUnitSpawn();
    }

    #endregion
}
