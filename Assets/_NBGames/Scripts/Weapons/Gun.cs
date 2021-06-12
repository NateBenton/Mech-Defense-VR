using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _NBGames.Scripts.Weapons
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private XRGrabInteractable _grabInteractable;
    
        [Header("Haptic Feedback")]
        [SerializeField] private float _amplitude;
        [SerializeField] private float _duration;

        private void OnEnable()
        {
            _grabInteractable.activated.AddListener(TriggerPulled);
        }

        private void OnDisable()
        {
            _grabInteractable.activated.RemoveListener(TriggerPulled);
        }

        private void TriggerPulled(ActivateEventArgs arg0)
        {
            arg0.interactor.GetComponent<XRBaseController>().SendHapticImpulse(_amplitude, _duration);
        }
    }
}
