using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public abstract class NVRInteractable : MonoBehaviour
    {
        public NVRInteractionEvent OnHovering;
        public NVRInteractionEvent OnHoveringStart;
        public NVRInteractionEvent OnHoveringEnd;

        protected Collider[] Colliders = new Collider[0];


        protected virtual void Awake()
        {

        }

        public virtual void UpdateColliders()
        {

            Collider[] candidates = GetComponentsInChildren<Collider>();
            List<Collider> result = new List<Collider>();

            foreach (Collider col in candidates) {
                NVRInteractable interactable = col.gameObject.GetComponentInParent<NVRInteractable>();
                if (interactable != this)
                    continue;

                    result.Add(col);
            }
            Colliders = result.ToArray();

            NVRInteractables.Register(this, Colliders);
        }

        protected virtual void Start()
        {
            UpdateColliders();
        }

        protected virtual void Update()
        {

        }

        public virtual void HoverStart(NVRHand hand)
        {
            if (OnHoveringStart != null)
                OnHoveringStart.Invoke(hand);
        }

        public virtual void HoveringUpdate(NVRHand hand, float forTime)
        {
            if (OnHovering != null)
            {
                OnHovering.Invoke(hand);
            }
        }

        public virtual void HoverEnd(NVRHand hand)
        {
            if (OnHoveringEnd != null)
                OnHoveringEnd.Invoke(hand);
        }

        protected virtual void OnDestroy()
        {
            NVRInteractables.Deregister(this);
        }
    }
}
