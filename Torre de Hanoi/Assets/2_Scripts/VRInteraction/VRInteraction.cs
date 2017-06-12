﻿using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class VRInteraction : MonoBehaviour {

    protected List<Interactable> interactablesInRange = new List<Interactable>();

    [SerializeField] protected Transform interactionPoint;
    public LayerMask interactMask;

    public bool interactionEnabled { get; protected set; }

    protected Interactable currSelectedInteractable { get; private set; }
    protected Interactable currManipulatedInteractable { get; private set; }
    protected Interactable interactable { get { return currManipulatedInteractable ?? currSelectedInteractable; } }

    private Action<Interactable> OnSelect;
    private Action<Interactable> OnDeselect;
    private Action<Interactable> OnManipulationStart;
    private Action<Interactable> OnManipulationEnd;

    public abstract void SelectInteractableFromRange();
    public abstract void OnTriggerPress(VRWand_Controller wand);
    public abstract void OnTriggerRelease(VRWand_Controller wand);
    public abstract void OnGripPress(VRWand_Controller wand);
    public abstract void OnGripRelease(VRWand_Controller wand);

    protected virtual void Start()
    {
        EnableInteration();
        SetCollisionRestrictions();
    }

    protected virtual void LateUpdate()
    {
        if (interactionEnabled)
        {
            SelectInteractableFromRange();
        }
    }

    protected virtual void DisableInteration()
    {
        interactionEnabled = false;
    }

    protected virtual void EnableInteration()
    {
        interactionEnabled = true;
    }

    private void SetCollisionRestrictions()
    {
        int objectLayer = gameObject.layer;

        for (int i = 0; i < 32; i++)
        {
            Physics.IgnoreLayerCollision(objectLayer, i, true);
            if (((1 << i) & interactMask) > 0)
            {
                Physics.IgnoreLayerCollision(objectLayer, i, false);
            }
        }
    }

    #region Callbacks
    public void OnSelectAddListener(Action<Interactable> action)
    {
        OnSelect += action;
    }

    public void OnSelectRemoveListener(Action<Interactable> action)
    {
        OnSelect -= action;
    }

    public void OnDeselectAddListener(Action<Interactable> action)
    {
        OnDeselect += action;
    }

    public void OnDeselectRemoveListener(Action<Interactable> action)
    {
        OnDeselect -= action;
    }

    public void OnManipulationStartAddListener(Action<Interactable> action)
    {
        OnManipulationStart += action;
    }

    public void OnManipulationStartRemoveListener(Action<Interactable> action)
    {
        OnManipulationStart -= action;
    }

    public void OnManipulationEndListener(Action<Interactable> action)
    {
        OnManipulationEnd += action;
    }

    public void OnManipulationEndRemoveListener(Action<Interactable> action)
    {
        OnManipulationEnd -= action;
    }
    #endregion

    #region ObjectSelection
    public void SetSelectedInteractable(Interactable interactable)
    {
        if (interactable != null)
        {
            if (currSelectedInteractable != null)
            {
                if (currSelectedInteractable == interactable)
                {
                    return;
                }
                DeselectInteractable(currSelectedInteractable);
            }
            SelectInteractable(interactable);
        }
        else if (currSelectedInteractable != null)
        {
            DeselectInteractable(currSelectedInteractable);
        }
    }

    private bool SelectInteractable(Interactable interactable)
    {
        if (interactable == null)
        {
            Debug.LogError("The interactable you're trying to select is null. Try using SetSelectedInteractable() instead.");
            return false;
        }

        currSelectedInteractable = interactable;
        interactable.OnSelected(this);
        
        if (OnSelect != null)
        {
            OnSelect(interactable);
        }

        return true;
    }

    private bool DeselectInteractable(Interactable interactable)
    {
        if (interactable == null)
        {
            Debug.LogError("You must send a interactable to be deselected.");
            return false;
        }

        if (interactable != currSelectedInteractable)
        {
            Debug.LogError("The interactable you're trying to deselect hasn't been selected. Current selected interactable is: " +
                currSelectedInteractable == null ? "null" : currSelectedInteractable.name);
            return false;
        }

        currSelectedInteractable = null;

        interactable.OnDeselected(this);

        if (OnDeselect != null)
        {
            OnDeselect(interactable);
        }

        return true;
    }
    #endregion

    #region Object Manipulation
    public bool CanManipulate(Interactable interactable)
    {
        if (interactable == null || currManipulatedInteractable != null)
        {
            return false;
        }

        SelectInteractableFromRange();
        return interactable == currSelectedInteractable && interactable.CanBeManipulated(interactionPoint);
    }

    public void SetManipulatedInteractable(Interactable interactable)
    {
        if (interactable != null)
        {
            if (currManipulatedInteractable != null)
            {
                if (currManipulatedInteractable == interactable)
                {
                    return;
                }
                ReleaseManipulatedInteractable(currManipulatedInteractable);
            }
            ManipulateInteractable(interactable);
        }
        else if (currManipulatedInteractable != null)
        {
            ReleaseManipulatedInteractable(currManipulatedInteractable);
        }
    }

    private bool ManipulateInteractable(Interactable interactable)
    {
        if (!CanManipulate(interactable))
        {
            return false;
        }

        currManipulatedInteractable = interactable;
        DisableInteration();

        interactable.OnManipulationStarted(this);

        if (OnManipulationStart != null)
        {
            OnManipulationStart(interactable);
        }

        return true;
    }

    private bool ReleaseManipulatedInteractable(Interactable interactable)
    {
        if (interactable == null || currManipulatedInteractable != interactable)
        {
            Debug.LogError("The interactable you're trying to stop manipulation is not being manipulated. Current manipulated interactable is: " +
                currManipulatedInteractable == null ? "null" : currManipulatedInteractable.name);
            return false;
        }

        EnableInteration();

        currManipulatedInteractable = null;

        interactable.OnManipulationEnded(this);

        if (OnManipulationEnd != null)
        {
            OnManipulationEnd(interactable);
        }

        return true;
    }
    #endregion

}
