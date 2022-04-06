// GENERATED AUTOMATICALLY FROM 'Assets/playerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""playerInputActions"",
    ""maps"": [
        {
            ""name"": ""player"",
            ""id"": ""8a1b533a-c465-43bf-9330-afe97030fce4"",
            ""actions"": [
                {
                    ""name"": ""movement"",
                    ""type"": ""Value"",
                    ""id"": ""a82d5fc7-2416-4d2d-a831-8b37411ac41b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""mousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""d6e1ca19-24ee-42ad-a5d8-379a8a0c6af5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""posses"",
                    ""type"": ""Button"",
                    ""id"": ""7570776a-a4c0-4741-be08-ea0b53259dad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""65550841-a089-4d1a-aced-8d5c62c75ecd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""398f4064-ad96-4f0b-bf1c-be13e256c36e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""99690ec5-985c-4aaf-8cfc-d68a03eae593"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""afdab36c-ec8f-44c2-afac-7aae5b571cee"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bd548c7c-157c-4646-8d7e-ad7228eb432d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3193f754-5e44-4a55-88dd-51de910acef8"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""mousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c49985b-6cbd-44d6-82d6-7e3e0c10e68b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""posses"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // player
        m_player = asset.FindActionMap("player", throwIfNotFound: true);
        m_player_movement = m_player.FindAction("movement", throwIfNotFound: true);
        m_player_mousePosition = m_player.FindAction("mousePosition", throwIfNotFound: true);
        m_player_posses = m_player.FindAction("posses", throwIfNotFound: true);
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

    // player
    private readonly InputActionMap m_player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_player_movement;
    private readonly InputAction m_player_mousePosition;
    private readonly InputAction m_player_posses;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @movement => m_Wrapper.m_player_movement;
        public InputAction @mousePosition => m_Wrapper.m_player_mousePosition;
        public InputAction @posses => m_Wrapper.m_player_posses;
        public InputActionMap Get() { return m_Wrapper.m_player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @mousePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @mousePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @mousePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @posses.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPosses;
                @posses.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPosses;
                @posses.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPosses;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @movement.started += instance.OnMovement;
                @movement.performed += instance.OnMovement;
                @movement.canceled += instance.OnMovement;
                @mousePosition.started += instance.OnMousePosition;
                @mousePosition.performed += instance.OnMousePosition;
                @mousePosition.canceled += instance.OnMousePosition;
                @posses.started += instance.OnPosses;
                @posses.performed += instance.OnPosses;
                @posses.canceled += instance.OnPosses;
            }
        }
    }
    public PlayerActions @player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnPosses(InputAction.CallbackContext context);
    }
}
