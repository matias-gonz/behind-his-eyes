//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/characters/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""CharacterControls"",
            ""id"": ""f3ef29bc-115f-4fc4-b451-5c91e2d67b12"",
            ""actions"": [
                {
                    ""name"": ""MovementForward"",
                    ""type"": ""Button"",
                    ""id"": ""8eac6e80-0b13-4494-96e2-549494fb8eae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""23ed7c07-522e-4e55-b586-3ed56f079597"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MovementLeft"",
                    ""type"": ""Button"",
                    ""id"": ""d4d147d4-8046-400e-a86d-be22f2cce6a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MovementRight"",
                    ""type"": ""Button"",
                    ""id"": ""70a5e756-340b-4acd-bcf8-2aebc3843289"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""1dbd4f05-aab3-4157-96b4-e0ace1744a0c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MovementBackward"",
                    ""type"": ""Button"",
                    ""id"": ""d9e641eb-bbaa-4853-94a3-29ec61c3b823"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CrouchProne"",
                    ""type"": ""Button"",
                    ""id"": ""eb3da6e1-b966-4ad9-873d-dae42697aca4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RifleAim"",
                    ""type"": ""Button"",
                    ""id"": ""8688a6f5-59a6-4155-92ff-f0e70b57a2d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RifleFire"",
                    ""type"": ""Button"",
                    ""id"": ""f3e7daea-3d56-4f1d-898f-1822819067cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""af8110e8-07d2-4131-a59f-c0d258ab6802"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""86460dc5-4365-42dc-b9c3-bf9e795cf1eb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56652802-d1ec-4faf-8251-c0521a5c4876"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59d38439-af8f-408a-808d-1e4b26d4c059"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf019fc3-b512-4001-a52a-b4514d17eede"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""480daf77-5d8f-42f7-bd00-9b2bab5debbd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e8ea674-3f75-4c80-889a-d48ed6e24aac"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementBackward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""979fb2b5-1dc2-4fc6-bb47-45b8fc64dcbb"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CrouchProne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d8fb2c2-474f-4775-923e-816cd1a9289b"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CrouchProne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad3a0415-281f-4f0d-b6d2-6a06d3eb5f95"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RifleAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b038d4b4-f955-41c9-a047-1906f6cff815"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RifleFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ced3ec58-7089-48f8-b78d-ff7127e5823e"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterControls
        m_CharacterControls = asset.FindActionMap("CharacterControls", throwIfNotFound: true);
        m_CharacterControls_MovementForward = m_CharacterControls.FindAction("MovementForward", throwIfNotFound: true);
        m_CharacterControls_Run = m_CharacterControls.FindAction("Run", throwIfNotFound: true);
        m_CharacterControls_MovementLeft = m_CharacterControls.FindAction("MovementLeft", throwIfNotFound: true);
        m_CharacterControls_MovementRight = m_CharacterControls.FindAction("MovementRight", throwIfNotFound: true);
        m_CharacterControls_Jump = m_CharacterControls.FindAction("Jump", throwIfNotFound: true);
        m_CharacterControls_MovementBackward = m_CharacterControls.FindAction("MovementBackward", throwIfNotFound: true);
        m_CharacterControls_CrouchProne = m_CharacterControls.FindAction("CrouchProne", throwIfNotFound: true);
        m_CharacterControls_RifleAim = m_CharacterControls.FindAction("RifleAim", throwIfNotFound: true);
        m_CharacterControls_RifleFire = m_CharacterControls.FindAction("RifleFire", throwIfNotFound: true);
        m_CharacterControls_Look = m_CharacterControls.FindAction("Look", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // CharacterControls
    private readonly InputActionMap m_CharacterControls;
    private List<ICharacterControlsActions> m_CharacterControlsActionsCallbackInterfaces = new List<ICharacterControlsActions>();
    private readonly InputAction m_CharacterControls_MovementForward;
    private readonly InputAction m_CharacterControls_Run;
    private readonly InputAction m_CharacterControls_MovementLeft;
    private readonly InputAction m_CharacterControls_MovementRight;
    private readonly InputAction m_CharacterControls_Jump;
    private readonly InputAction m_CharacterControls_MovementBackward;
    private readonly InputAction m_CharacterControls_CrouchProne;
    private readonly InputAction m_CharacterControls_RifleAim;
    private readonly InputAction m_CharacterControls_RifleFire;
    private readonly InputAction m_CharacterControls_Look;
    public struct CharacterControlsActions
    {
        private @PlayerInput m_Wrapper;
        public CharacterControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovementForward => m_Wrapper.m_CharacterControls_MovementForward;
        public InputAction @Run => m_Wrapper.m_CharacterControls_Run;
        public InputAction @MovementLeft => m_Wrapper.m_CharacterControls_MovementLeft;
        public InputAction @MovementRight => m_Wrapper.m_CharacterControls_MovementRight;
        public InputAction @Jump => m_Wrapper.m_CharacterControls_Jump;
        public InputAction @MovementBackward => m_Wrapper.m_CharacterControls_MovementBackward;
        public InputAction @CrouchProne => m_Wrapper.m_CharacterControls_CrouchProne;
        public InputAction @RifleAim => m_Wrapper.m_CharacterControls_RifleAim;
        public InputAction @RifleFire => m_Wrapper.m_CharacterControls_RifleFire;
        public InputAction @Look => m_Wrapper.m_CharacterControls_Look;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControlsActions set) { return set.Get(); }
        public void AddCallbacks(ICharacterControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_CharacterControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CharacterControlsActionsCallbackInterfaces.Add(instance);
            @MovementForward.started += instance.OnMovementForward;
            @MovementForward.performed += instance.OnMovementForward;
            @MovementForward.canceled += instance.OnMovementForward;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @MovementLeft.started += instance.OnMovementLeft;
            @MovementLeft.performed += instance.OnMovementLeft;
            @MovementLeft.canceled += instance.OnMovementLeft;
            @MovementRight.started += instance.OnMovementRight;
            @MovementRight.performed += instance.OnMovementRight;
            @MovementRight.canceled += instance.OnMovementRight;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @MovementBackward.started += instance.OnMovementBackward;
            @MovementBackward.performed += instance.OnMovementBackward;
            @MovementBackward.canceled += instance.OnMovementBackward;
            @CrouchProne.started += instance.OnCrouchProne;
            @CrouchProne.performed += instance.OnCrouchProne;
            @CrouchProne.canceled += instance.OnCrouchProne;
            @RifleAim.started += instance.OnRifleAim;
            @RifleAim.performed += instance.OnRifleAim;
            @RifleAim.canceled += instance.OnRifleAim;
            @RifleFire.started += instance.OnRifleFire;
            @RifleFire.performed += instance.OnRifleFire;
            @RifleFire.canceled += instance.OnRifleFire;
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
        }

        private void UnregisterCallbacks(ICharacterControlsActions instance)
        {
            @MovementForward.started -= instance.OnMovementForward;
            @MovementForward.performed -= instance.OnMovementForward;
            @MovementForward.canceled -= instance.OnMovementForward;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @MovementLeft.started -= instance.OnMovementLeft;
            @MovementLeft.performed -= instance.OnMovementLeft;
            @MovementLeft.canceled -= instance.OnMovementLeft;
            @MovementRight.started -= instance.OnMovementRight;
            @MovementRight.performed -= instance.OnMovementRight;
            @MovementRight.canceled -= instance.OnMovementRight;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @MovementBackward.started -= instance.OnMovementBackward;
            @MovementBackward.performed -= instance.OnMovementBackward;
            @MovementBackward.canceled -= instance.OnMovementBackward;
            @CrouchProne.started -= instance.OnCrouchProne;
            @CrouchProne.performed -= instance.OnCrouchProne;
            @CrouchProne.canceled -= instance.OnCrouchProne;
            @RifleAim.started -= instance.OnRifleAim;
            @RifleAim.performed -= instance.OnRifleAim;
            @RifleAim.canceled -= instance.OnRifleAim;
            @RifleFire.started -= instance.OnRifleFire;
            @RifleFire.performed -= instance.OnRifleFire;
            @RifleFire.canceled -= instance.OnRifleFire;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
        }

        public void RemoveCallbacks(ICharacterControlsActions instance)
        {
            if (m_Wrapper.m_CharacterControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICharacterControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_CharacterControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CharacterControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CharacterControlsActions @CharacterControls => new CharacterControlsActions(this);
    public interface ICharacterControlsActions
    {
        void OnMovementForward(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnMovementLeft(InputAction.CallbackContext context);
        void OnMovementRight(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMovementBackward(InputAction.CallbackContext context);
        void OnCrouchProne(InputAction.CallbackContext context);
        void OnRifleAim(InputAction.CallbackContext context);
        void OnRifleFire(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
}
