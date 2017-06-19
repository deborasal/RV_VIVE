using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc_PickUp : Pickup_Touch {

    public Transform manager;
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

    public override bool CanBeManipulated(Transform other)
    {
        initialPos = transform.position;
        return base.CanBeManipulated(other) && manager.GetComponent<disk_manage>().TryPop(transform.GetComponent<properties>().GetSize(), initialPos);
    }

    protected override void GetDropped(Vector3 throwVelocity)
    {
        if (!manager.GetComponent<disk_manage>().TryPush(transform.GetComponent<properties>().GetSize(), transform.position))
        {
            transform.position = initialPos;
            manager.GetComponent<disk_manage>().TryPush(transform.GetComponent<properties>().GetSize(), transform.position);
            manager.GetComponent<disk_manage>().BackToInitialPos();
        }

        holder.SetManipulatedInteractable(null);

        rby.useGravity = true;
        rby.isKinematic = false;

        rby.velocity = throwVelocity;
    }
}
