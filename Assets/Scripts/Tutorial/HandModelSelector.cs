using System;
using System.Collections.Generic;
using BNG;
using UnityEngine;

namespace Tutorial {
    public class HandModelSelector : MonoBehaviour {

        /// <summary>
        /// Child index of the hand model to use if nothing stored in playerprefs and LoadHandSelectionFromPrefs true
        /// </summary>
        [Tooltip("Child index of the hand model to use if nothing stored in playerprefs or LoadHandSelectionFromPrefs set to false")]        
        public int DefaultHandsModel = 0;
        
        /// <summary>
        /// This transform holds all of the hand models. Can be used to enabled / disabled various hand options
        /// </summary>
        [Tooltip("This transform holds all of the hand models. Can be used to enabled / disabled various hand options.")]
        public Transform LeftHandGFXHolder;

        /// <summary>
        /// This transform holds all of the hand models. Can be used to enabled / disabled various hand options
        /// </summary>
        [Tooltip("This transform holds all of the hand models. Can be used to enabled / disabled various hand options")]
        public Transform RightHandGFXHolder;
        private int _selectedHandGFX = 0;

        /// <summary>
        /// This is the start point of a line for UI purposes. We may want to move this around if we change models or controllers.        
        /// </summary>
        UIPointer uiPoint;

        List<Transform> leftHandModels = default;
        Transform activatedLeftModel = default;

        List<Transform> rightHandModels = default;
        Transform activatedRightModel = default;

        private void Awake() {
            uiPoint = GetComponentInChildren<UIPointer>();

            CacheHandModels();
            ChangeHandsModel(DefaultHandsModel);
        }

        private void Update()
        {
            if (!rightHandModels[1].gameObject.activeSelf)
            {
                rightHandModels[1].gameObject.transform.position = transform.position;
                leftHandModels[1].gameObject.transform.position = transform.position;
            }
        }

        private void CacheHandModels() {

            leftHandModels = new List<Transform>();
            for(int x = 0; x < LeftHandGFXHolder.childCount; x++) {
                leftHandModels.Add(LeftHandGFXHolder.GetChild(x));
            }

            rightHandModels = new List<Transform>();
            for (int x = 0; x < RightHandGFXHolder.childCount; x++) {
                rightHandModels.Add(RightHandGFXHolder.GetChild(x));
            }
        }

        private void ChangeHandsModel(int childIndex) {

            // Deactivate any previous models
            if(activatedLeftModel != null) {
                activatedLeftModel.gameObject.SetActive(false);
            }
            if (activatedRightModel != null) {
                activatedRightModel.gameObject.SetActive(false);
            }

            // Activate new Model
            

            // Loop back to beginning if we went over
            _selectedHandGFX = childIndex;
            if (_selectedHandGFX > leftHandModels.Count - 1) {
                _selectedHandGFX = 0;
            }

            // Activate New
            activatedLeftModel = leftHandModels[_selectedHandGFX];
            activatedRightModel = rightHandModels[_selectedHandGFX];

            activatedLeftModel.gameObject.SetActive(true);
            activatedRightModel.gameObject.SetActive(true);

            // Update any animators
            HandController leftControl = LeftHandGFXHolder.parent.GetComponent<HandController>();
            HandController rightControl = RightHandGFXHolder.parent.GetComponent<HandController>();

            // Physical hands have their own animator controler
            bool isPhysicalHand = activatedLeftModel.name.ToLower().Contains("physical");
            if(isPhysicalHand) {
                leftControl.HandAnimator = null;
                rightControl.HandAnimator = null;
            }
            else if (leftControl && rightControl) {
                leftControl.HandAnimator = activatedLeftModel.GetComponentInChildren<Animator>(true);
                rightControl.HandAnimator = activatedRightModel.GetComponentInChildren<Animator>(true);
            }

            // Change UI Pointer position depending on if we're using Oculus Hands or Oculus Controller Model
            // This is for the demo. Typically this would be fixed to a bone or transform
            // Oculus Touch Controller is positioned near the front
            if ((activatedLeftModel.transform.name.StartsWith("OculusTouchForQuestAndRift") || activatedLeftModel.transform.name.StartsWith("ControllerReferences")) && uiPoint != null) {
                uiPoint.transform.localPosition = new Vector3(0, 0, 0.0462f);
                uiPoint.transform.localEulerAngles = new Vector3(0, 0f, 0);
            }
            // Hand Model
            else if (_selectedHandGFX != 0 && uiPoint != null) {
                uiPoint.transform.localPosition = new Vector3(0.0392f, 0.0033f, 0.0988f);
                uiPoint.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }

        public void SwapToHands()
        {
            ChangeHandsModel(1);
        }
        public void SwapToControllers()
        {
            ChangeHandsModel(0);
        }
    }
}