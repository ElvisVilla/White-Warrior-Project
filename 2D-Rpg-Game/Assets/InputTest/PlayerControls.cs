// GENERATED AUTOMATICALLY FROM 'Assets/InputTest/PlayerControls.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerControls : IInputActionCollection
{
    private InputActionAsset asset;
    public PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""MovementMap"",
            ""id"": ""cf541c14-9be9-4b35-b996-fff7d5f55998"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""76910a25-5f2e-4334-ac47-7697e53c8668"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8c358156-8ecf-43f0-af4a-bacdfdf7cbde"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""keyboard"",
                    ""id"": ""d7090400-da29-4e02-81ec-0cacfde14404"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e966dfb0-648b-4529-8044-02c90b771f6b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2e36c292-53ac-4475-a6de-1dd4feb11cc5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""47ea7faa-e5f1-467f-b60a-684266ff2957"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""044e2f6d-8db2-46f9-9e01-0d508371ddc9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""79c58da4-b004-44fe-811a-e5edae5e0304"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""aac7f037-be3a-4079-ac5d-07d56436ffcb"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""61163114-6f1e-4ba4-ba69-f04b5102813a"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5b8253bf-3905-4941-83d5-9e2e155d2ab1"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ca128bad-3645-47a4-b534-98aaf1c1acae"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""91a12a36-c843-4b85-8dac-777e9ed9cf9e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ce3b152-78ca-42f8-8296-f3d336f53afa"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""AbilitiesMap"",
            ""id"": ""222e5545-2755-4f26-89c7-a83a412f7676"",
            ""actions"": [
                {
                    ""name"": ""Ability One"",
                    ""type"": ""Button"",
                    ""id"": ""1b384782-9073-4ade-b3a7-ce849a5eb1a5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Ability Two"",
                    ""type"": ""Button"",
                    ""id"": ""bdaeb841-8858-4ee1-8eb9-99b74a3f579a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Ability Three"",
                    ""type"": ""Button"",
                    ""id"": ""6c0ece09-0015-4d70-b397-547080119936"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Ability Four"",
                    ""type"": ""Button"",
                    ""id"": ""7d04efc4-f43f-4a6b-9b99-abf38e916ae3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""dd312934-ada5-4fa6-9d55-8a2f2489d59f"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability One"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56fc4b22-53aa-4397-875d-23766fdcca33"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability One"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""039e258e-ca9f-4833-a204-f83a77e751aa"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability Two"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""189608bf-2790-47e5-971a-d7e2c0f50109"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability Two"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb665ede-5cd1-4a8a-a9cf-af33ac34dec8"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability Three"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13b22a8e-487d-4698-a02e-1c2dc2d66070"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability Three"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc4a4ed9-fc1d-4954-91ed-8b4b463946c4"",
                    ""path"": ""<XInputController>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability Four"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db77dc7d-3541-4a70-81d8-602479b8ae73"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability Four"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MovementMap
        m_MovementMap = asset.GetActionMap("MovementMap");
        m_MovementMap_Movement = m_MovementMap.GetAction("Movement");
        m_MovementMap_Jump = m_MovementMap.GetAction("Jump");
        // AbilitiesMap
        m_AbilitiesMap = asset.GetActionMap("AbilitiesMap");
        m_AbilitiesMap_AbilityOne = m_AbilitiesMap.GetAction("Ability One");
        m_AbilitiesMap_AbilityTwo = m_AbilitiesMap.GetAction("Ability Two");
        m_AbilitiesMap_AbilityThree = m_AbilitiesMap.GetAction("Ability Three");
        m_AbilitiesMap_AbilityFour = m_AbilitiesMap.GetAction("Ability Four");
    }

    ~PlayerControls()
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

    // MovementMap
    private readonly InputActionMap m_MovementMap;
    private IMovementMapActions m_MovementMapActionsCallbackInterface;
    private readonly InputAction m_MovementMap_Movement;
    private readonly InputAction m_MovementMap_Jump;
    public struct MovementMapActions
    {
        private PlayerControls m_Wrapper;
        public MovementMapActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_MovementMap_Movement;
        public InputAction @Jump => m_Wrapper.m_MovementMap_Jump;
        public InputActionMap Get() { return m_Wrapper.m_MovementMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementMapActions set) { return set.Get(); }
        public void SetCallbacks(IMovementMapActions instance)
        {
            if (m_Wrapper.m_MovementMapActionsCallbackInterface != null)
            {
                Movement.started -= m_Wrapper.m_MovementMapActionsCallbackInterface.OnMovement;
                Movement.performed -= m_Wrapper.m_MovementMapActionsCallbackInterface.OnMovement;
                Movement.canceled -= m_Wrapper.m_MovementMapActionsCallbackInterface.OnMovement;
                Jump.started -= m_Wrapper.m_MovementMapActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_MovementMapActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_MovementMapActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_MovementMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                Movement.started += instance.OnMovement;
                Movement.performed += instance.OnMovement;
                Movement.canceled += instance.OnMovement;
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
            }
        }
    }
    public MovementMapActions @MovementMap => new MovementMapActions(this);

    // AbilitiesMap
    private readonly InputActionMap m_AbilitiesMap;
    private IAbilitiesMapActions m_AbilitiesMapActionsCallbackInterface;
    private readonly InputAction m_AbilitiesMap_AbilityOne;
    private readonly InputAction m_AbilitiesMap_AbilityTwo;
    private readonly InputAction m_AbilitiesMap_AbilityThree;
    private readonly InputAction m_AbilitiesMap_AbilityFour;
    public struct AbilitiesMapActions
    {
        private PlayerControls m_Wrapper;
        public AbilitiesMapActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @AbilityOne => m_Wrapper.m_AbilitiesMap_AbilityOne;
        public InputAction @AbilityTwo => m_Wrapper.m_AbilitiesMap_AbilityTwo;
        public InputAction @AbilityThree => m_Wrapper.m_AbilitiesMap_AbilityThree;
        public InputAction @AbilityFour => m_Wrapper.m_AbilitiesMap_AbilityFour;
        public InputActionMap Get() { return m_Wrapper.m_AbilitiesMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AbilitiesMapActions set) { return set.Get(); }
        public void SetCallbacks(IAbilitiesMapActions instance)
        {
            if (m_Wrapper.m_AbilitiesMapActionsCallbackInterface != null)
            {
                AbilityOne.started -= m_Wrapper.m_AbilitiesMapActionsCallbackInterface.OnAbilityOne;
                AbilityOne.performed -= m_Wrapper.m_AbilitiesMapActionsCallbackInterface.OnAbilityOne;
                AbilityOne.canceled -= m_Wrapper.m_AbilitiesMapActionsCallbackInterface.OnAbilityOne;
                AbilityTwo.started -= m_Wrapper.m_AbilitiesMapActionsCallbackInterface.OnAbilityTwo;
                AbilityTwo.performed -= m_Wrapper.m_AbilitiesMapActionsCallbackInterface.OnAbilityTwo;
                AbilityTwo.canceled -= m_Wrapper.m_AbilitiesMapActionsCallbackInterface.OnAbilityTwo;
                AbilityThree.started -= m_Wrapper.m_AbilitiesMapActionsCallbackInterface.OnAbilityThree;
                AbilityThree.performed -= m_Wrapper.m_AbilitiesMapActionsCallbackInterface.OnAbilityThree;
                AbilityThree.canceled -= m_Wrapper.m_AbilitiesMapActionsCallbackInterface.OnAbilityThree;
                AbilityFour.started -= m_Wrapper.m_AbilitiesMapActionsCallbackInterface.OnAbilityFour;
                AbilityFour.performed -= m_Wrapper.m_AbilitiesMapActionsCallbackInterface.OnAbilityFour;
                AbilityFour.canceled -= m_Wrapper.m_AbilitiesMapActionsCallbackInterface.OnAbilityFour;
            }
            m_Wrapper.m_AbilitiesMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                AbilityOne.started += instance.OnAbilityOne;
                AbilityOne.performed += instance.OnAbilityOne;
                AbilityOne.canceled += instance.OnAbilityOne;
                AbilityTwo.started += instance.OnAbilityTwo;
                AbilityTwo.performed += instance.OnAbilityTwo;
                AbilityTwo.canceled += instance.OnAbilityTwo;
                AbilityThree.started += instance.OnAbilityThree;
                AbilityThree.performed += instance.OnAbilityThree;
                AbilityThree.canceled += instance.OnAbilityThree;
                AbilityFour.started += instance.OnAbilityFour;
                AbilityFour.performed += instance.OnAbilityFour;
                AbilityFour.canceled += instance.OnAbilityFour;
            }
        }
    }
    public AbilitiesMapActions @AbilitiesMap => new AbilitiesMapActions(this);
    public interface IMovementMapActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IAbilitiesMapActions
    {
        void OnAbilityOne(InputAction.CallbackContext context);
        void OnAbilityTwo(InputAction.CallbackContext context);
        void OnAbilityThree(InputAction.CallbackContext context);
        void OnAbilityFour(InputAction.CallbackContext context);
    }
}
