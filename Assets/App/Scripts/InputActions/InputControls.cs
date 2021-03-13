// GENERATED AUTOMATICALLY FROM 'Assets/App/Scripts/InputActions/InputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Move"",
            ""id"": ""705b6947-6672-4027-8db6-b21e673195f8"",
            ""actions"": [
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""275e4caa-4e6e-42eb-9cfb-d445ec6bc106"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftUp"",
                    ""type"": ""Button"",
                    ""id"": ""b9a3c0fd-b960-4297-849c-15423841ff6e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""40797911-7ae9-4832-8c37-5ec5c6fe6d0c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightUp"",
                    ""type"": ""Button"",
                    ""id"": ""3d1f278d-9495-492d-928f-9d756fd0268d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""1cc8b00c-bad6-4ea2-8b89-c6238de87414"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightDown"",
                    ""type"": ""Button"",
                    ""id"": ""895ad5dc-b516-491f-9e30-1804316bbeec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""e29a7456-0644-404f-abab-4fc259e9f846"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftDown"",
                    ""type"": ""Button"",
                    ""id"": ""5e7a352d-5c7c-4976-b4b9-1a4c5680b976"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DirSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""2d497917-aade-4352-b8cd-55ad359603ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""76b9e3a5-6e57-4893-978f-8856aed47548"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3f5c8d7-f3ec-4055-96be-baefca661b19"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e165e3e-8050-49d0-aa3c-3f6b990e7e99"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82fbe863-5b28-450a-8a1c-7faf5d752256"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f7f789b-3bc8-45a2-824f-90020f518586"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c3a753f-4989-4bb9-b98e-56f3942b3a2e"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed635b3e-7078-4fdd-882c-c44ea1fba128"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0052049c-0ca9-454a-be73-e8f6241ed081"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fefb2395-8137-455a-9ffa-a73d6beb638b"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Move
        m_Move = asset.FindActionMap("Move", throwIfNotFound: true);
        m_Move_Left = m_Move.FindAction("Left", throwIfNotFound: true);
        m_Move_LeftUp = m_Move.FindAction("LeftUp", throwIfNotFound: true);
        m_Move_Up = m_Move.FindAction("Up", throwIfNotFound: true);
        m_Move_RightUp = m_Move.FindAction("RightUp", throwIfNotFound: true);
        m_Move_Right = m_Move.FindAction("Right", throwIfNotFound: true);
        m_Move_RightDown = m_Move.FindAction("RightDown", throwIfNotFound: true);
        m_Move_Down = m_Move.FindAction("Down", throwIfNotFound: true);
        m_Move_LeftDown = m_Move.FindAction("LeftDown", throwIfNotFound: true);
        m_Move_DirSwitch = m_Move.FindAction("DirSwitch", throwIfNotFound: true);
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

    // Move
    private readonly InputActionMap m_Move;
    private IMoveActions m_MoveActionsCallbackInterface;
    private readonly InputAction m_Move_Left;
    private readonly InputAction m_Move_LeftUp;
    private readonly InputAction m_Move_Up;
    private readonly InputAction m_Move_RightUp;
    private readonly InputAction m_Move_Right;
    private readonly InputAction m_Move_RightDown;
    private readonly InputAction m_Move_Down;
    private readonly InputAction m_Move_LeftDown;
    private readonly InputAction m_Move_DirSwitch;
    public struct MoveActions
    {
        private @InputControls m_Wrapper;
        public MoveActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Left => m_Wrapper.m_Move_Left;
        public InputAction @LeftUp => m_Wrapper.m_Move_LeftUp;
        public InputAction @Up => m_Wrapper.m_Move_Up;
        public InputAction @RightUp => m_Wrapper.m_Move_RightUp;
        public InputAction @Right => m_Wrapper.m_Move_Right;
        public InputAction @RightDown => m_Wrapper.m_Move_RightDown;
        public InputAction @Down => m_Wrapper.m_Move_Down;
        public InputAction @LeftDown => m_Wrapper.m_Move_LeftDown;
        public InputAction @DirSwitch => m_Wrapper.m_Move_DirSwitch;
        public InputActionMap Get() { return m_Wrapper.m_Move; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MoveActions set) { return set.Get(); }
        public void SetCallbacks(IMoveActions instance)
        {
            if (m_Wrapper.m_MoveActionsCallbackInterface != null)
            {
                @Left.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnLeft;
                @LeftUp.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnLeftUp;
                @LeftUp.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnLeftUp;
                @LeftUp.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnLeftUp;
                @Up.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnUp;
                @RightUp.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnRightUp;
                @RightUp.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnRightUp;
                @RightUp.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnRightUp;
                @Right.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnRight;
                @RightDown.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnRightDown;
                @RightDown.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnRightDown;
                @RightDown.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnRightDown;
                @Down.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnDown;
                @LeftDown.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnLeftDown;
                @LeftDown.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnLeftDown;
                @LeftDown.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnLeftDown;
                @DirSwitch.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnDirSwitch;
                @DirSwitch.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnDirSwitch;
                @DirSwitch.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnDirSwitch;
            }
            m_Wrapper.m_MoveActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @LeftUp.started += instance.OnLeftUp;
                @LeftUp.performed += instance.OnLeftUp;
                @LeftUp.canceled += instance.OnLeftUp;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @RightUp.started += instance.OnRightUp;
                @RightUp.performed += instance.OnRightUp;
                @RightUp.canceled += instance.OnRightUp;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @RightDown.started += instance.OnRightDown;
                @RightDown.performed += instance.OnRightDown;
                @RightDown.canceled += instance.OnRightDown;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @LeftDown.started += instance.OnLeftDown;
                @LeftDown.performed += instance.OnLeftDown;
                @LeftDown.canceled += instance.OnLeftDown;
                @DirSwitch.started += instance.OnDirSwitch;
                @DirSwitch.performed += instance.OnDirSwitch;
                @DirSwitch.canceled += instance.OnDirSwitch;
            }
        }
    }
    public MoveActions @Move => new MoveActions(this);
    public interface IMoveActions
    {
        void OnLeft(InputAction.CallbackContext context);
        void OnLeftUp(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnRightUp(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnRightDown(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeftDown(InputAction.CallbackContext context);
        void OnDirSwitch(InputAction.CallbackContext context);
    }
}
