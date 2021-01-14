using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class UnitMovement : NetworkBehaviour
{
    [SerializeReference]
    private NavMeshAgent UnitAgent = null;

    private Camera MainCamera = null;

    #region Server
    [Command]
    public void CmdUnitMove(Vector3 Pos)
    {
        if(!NavMesh.SamplePosition(Pos,out NavMeshHit hit,1f, NavMesh.AllAreas)){
            return;
        }

        UnitAgent.SetDestination(hit.position);
        
    }
    #endregion

    #region client

    public override void OnStartAuthority()
    {
        MainCamera = Camera.main;
    }

    [ClientCallback]
    void Update()
    {
        if (!hasAuthority)
        {
            return;
        }
        if (!Mouse.current.rightButton.wasPressedThisFrame)
            
        {
            return;
        }

        Ray ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(!Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity))
        {
            return;
        }

        CmdUnitMove(hit.point);
    }


    #endregion

}
