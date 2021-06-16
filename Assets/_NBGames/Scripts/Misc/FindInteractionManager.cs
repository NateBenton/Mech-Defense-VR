using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace _NBGames.Scripts.Misc
{
    public class FindInteractionManager : MonoBehaviour
    {
        private XRGrabInteractable _grabInteractable;
        
        private void OnEnable()
        {
            SceneManager.activeSceneChanged += GetInteractionManager;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= GetInteractionManager;
        }

        private void Awake()
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();

            if (_grabInteractable == null)
            {
                Debug.LogError("grabInteractable null on " + gameObject.name);
            }
        }

        private void GetInteractionManager(Scene arg0, Scene arg1)
        {
            _grabInteractable.interactionManager = FindObjectOfType<XRInteractionManager>();
        }
    }
}
