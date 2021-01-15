using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;


public class UnitMovement : NetworkBehaviour
{
    [SerializeReference]
    private NavMeshAgent UnitAgent = null;

   

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

   

}
