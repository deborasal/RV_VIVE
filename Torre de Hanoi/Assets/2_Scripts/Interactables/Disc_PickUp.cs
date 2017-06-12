using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc_PickUp : Pickup_Touch {

    private Vector3 initialPos;

    protected override void Start()
    {
        base.Start();
        initialPos = transform.position;
    }

    public override void OnManipulationEnded(VRInteraction caller)
    {
        base.OnManipulationEnded(caller);
        //Verificar se está na barra: Cair se sim, voltar à posição inicial se não
    }
}
