// GENERATED AUTOMATICALLY FROM 'Assets/Giri/Input Action/AllControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @AllControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @AllControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""AllControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c3a302d4-348b-4f13-80fa-020a44c14add"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""2d752020-2a02-440a-973c-a1f723ccb13d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""07b8b798-0b94-4ad5-a594-26146c475ee3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""68689537-5670-43f0-b4aa-3d2d2b3061d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""75b8cf44-5863-46ba-834c-4db411a11360"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9779e288-0520-4fc1-bd6c-0f0366eda668"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""XboxController"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""998e2844-9493-4889-b1be-f4ec72570bd3"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1802f8a9-eed2-4e2b-a724-c64b30e6047d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""094d71cf-0f04-4129-b6a0-6016b86c2b56"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5df1212f-9fc5-4645-8404-5f2e39c2bef4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""769eceb5-816a-4d6d-ab20-b92390163bb7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b5d13e46-d911-43e9-8928-356447b8866b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone,ScaleVector2(x=300,y=300)"",
                    ""groups"": ""XboxController"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed986b4a-4681-4224-acc7-06142e732b43"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=15,y=15)"",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c1f03e8-f96b-4d0c-ae25-cb96b53c1514"",
                    ""path"": ""<XInputController>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5fbc95b8-1837-4162-af3f-ceec7bcdadee"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce8ecc74-193e-4d54-8f25-62d4463154ac"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62926882-cd17-43b7-b92b-2546cdda606e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""EarthBending"",
            ""id"": ""045eb542-b7e3-4df7-90a5-a9d023d3521a"",
            ""actions"": [
                {
                    ""name"": ""SeismicSense"",
                    ""type"": ""Button"",
                    ""id"": ""0b9ff865-a0f1-4e04-934d-b1e433f77be2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenWall"",
                    ""type"": ""Button"",
                    ""id"": ""ac65061c-37b9-4f04-8aae-d7677d19f926"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""202bd3a3-1d2f-4c39-8353-2dbee34e47e5"",
                    ""path"": ""<XInputController>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""SeismicSense"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""030067f0-a048-456e-9365-bc186786c74d"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""SeismicSense"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4ba4ded-e27a-4458-a07a-5bcbb0a4534e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""OpenWall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""FireBending"",
            ""id"": ""d21a1cbf-b5ad-4d5e-a125-2f9c7685dcf2"",
            ""actions"": [
                {
                    ""name"": ""FireTorch"",
                    ""type"": ""Button"",
                    ""id"": ""1290060d-d38b-4bfb-bbed-25d2113e4585"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""714572d7-f5aa-4b0f-bf5d-9222bfefe574"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""FireTorch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6638743-cc28-4608-aeac-2fb155e8a82b"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""FireTorch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ControlHints"",
            ""id"": ""249f4619-6afb-4d89-92e0-921c57575c21"",
            ""actions"": [
                {
                    ""name"": ""CheckKM"",
                    ""type"": ""Value"",
                    ""id"": ""a2e85fd8-aac5-4168-aae8-29ddfb55f085"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CheckXC"",
                    ""type"": ""Value"",
                    ""id"": ""c7a6cbde-4aa0-4c19-91f2-3c7a4ff90ab8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7d27590c-ee72-46c3-a95a-ae44b42a53c1"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""CheckKM"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9b6ad0c-34d3-441f-a0b5-469bfb23084a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""CheckKM"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3ca0083-c296-46c8-bf17-75185a23e5a9"",
                    ""path"": ""<XInputController>/button*"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckXC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a13706f1-7d35-4ade-a7ed-733485be5710"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckXC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05f4055e-b20a-4e7e-a8a8-1144ae3f557f"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckXC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""178d7303-3cd1-47c2-aa0c-9532220b5a98"",
                    ""path"": ""<XInputController>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckXC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""350ab58b-559f-4d91-9936-5c8d9bc7bcb6"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckXC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c87c7a97-860a-457e-ad43-c171b744659e"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckXC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06a0ce8a-1571-4927-bcd4-87167d0c5d58"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckXC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21a4b3ad-5644-4956-8a4a-2c4f9d55288d"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckXC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88ced3a2-d4c9-4242-811f-5fceee39ee02"",
                    ""path"": ""<XInputController>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckXC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4994717c-88f8-4cbf-bf15-e4bf72c2564b"",
                    ""path"": ""<XInputController>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckXC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49d7c9f4-7012-48b2-9ebb-c6d91c8ad519"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckXC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d231cc05-8f11-46ee-8b6e-3f00d1309037"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckXC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""c2d3626b-055f-4be8-a7c7-0acb788185c1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3e786d1f-cfc8-4e2e-b738-7c738c18296f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""a98c15c1-5bc4-4f40-9ff2-0b51a3a2fba1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RoomLobby"",
                    ""type"": ""Button"",
                    ""id"": ""7f749efd-2cc6-417d-95e8-3588e43d7b94"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Paused"",
                    ""type"": ""Button"",
                    ""id"": ""d1f4036f-2fe4-43a8-8a50-a6396e2a1e31"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8ea427f1-b927-471c-913f-4d09cde509b4"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ecc99fe-10f0-4a6e-9bdb-2fceb39ca089"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b65cd62-24d6-44b2-8d09-a6b7568f4537"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""Paused"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91850578-7813-4416-8534-e739da03c254"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Paused"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d1ea8b6-613a-4d1d-a3fa-ad09fe6491e5"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Paused"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4153dfb2-a9ff-4163-ba8f-27aba552752a"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""RoomLobby"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39ec9b4b-9510-47c3-9628-5eef2a4033c8"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""RoomLobby"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XboxController"",
            ""bindingGroup"": ""XboxController"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        // EarthBending
        m_EarthBending = asset.FindActionMap("EarthBending", throwIfNotFound: true);
        m_EarthBending_SeismicSense = m_EarthBending.FindAction("SeismicSense", throwIfNotFound: true);
        m_EarthBending_OpenWall = m_EarthBending.FindAction("OpenWall", throwIfNotFound: true);
        // FireBending
        m_FireBending = asset.FindActionMap("FireBending", throwIfNotFound: true);
        m_FireBending_FireTorch = m_FireBending.FindAction("FireTorch", throwIfNotFound: true);
        // ControlHints
        m_ControlHints = asset.FindActionMap("ControlHints", throwIfNotFound: true);
        m_ControlHints_CheckKM = m_ControlHints.FindAction("CheckKM", throwIfNotFound: true);
        m_ControlHints_CheckXC = m_ControlHints.FindAction("CheckXC", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Move = m_UI.FindAction("Move", throwIfNotFound: true);
        m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
        m_UI_RoomLobby = m_UI.FindAction("RoomLobby", throwIfNotFound: true);
        m_UI_Paused = m_UI.FindAction("Paused", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Jump;
    public struct PlayerActions
    {
        private @AllControls m_Wrapper;
        public PlayerActions(@AllControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Run.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // EarthBending
    private readonly InputActionMap m_EarthBending;
    private IEarthBendingActions m_EarthBendingActionsCallbackInterface;
    private readonly InputAction m_EarthBending_SeismicSense;
    private readonly InputAction m_EarthBending_OpenWall;
    public struct EarthBendingActions
    {
        private @AllControls m_Wrapper;
        public EarthBendingActions(@AllControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SeismicSense => m_Wrapper.m_EarthBending_SeismicSense;
        public InputAction @OpenWall => m_Wrapper.m_EarthBending_OpenWall;
        public InputActionMap Get() { return m_Wrapper.m_EarthBending; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EarthBendingActions set) { return set.Get(); }
        public void SetCallbacks(IEarthBendingActions instance)
        {
            if (m_Wrapper.m_EarthBendingActionsCallbackInterface != null)
            {
                @SeismicSense.started -= m_Wrapper.m_EarthBendingActionsCallbackInterface.OnSeismicSense;
                @SeismicSense.performed -= m_Wrapper.m_EarthBendingActionsCallbackInterface.OnSeismicSense;
                @SeismicSense.canceled -= m_Wrapper.m_EarthBendingActionsCallbackInterface.OnSeismicSense;
                @OpenWall.started -= m_Wrapper.m_EarthBendingActionsCallbackInterface.OnOpenWall;
                @OpenWall.performed -= m_Wrapper.m_EarthBendingActionsCallbackInterface.OnOpenWall;
                @OpenWall.canceled -= m_Wrapper.m_EarthBendingActionsCallbackInterface.OnOpenWall;
            }
            m_Wrapper.m_EarthBendingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SeismicSense.started += instance.OnSeismicSense;
                @SeismicSense.performed += instance.OnSeismicSense;
                @SeismicSense.canceled += instance.OnSeismicSense;
                @OpenWall.started += instance.OnOpenWall;
                @OpenWall.performed += instance.OnOpenWall;
                @OpenWall.canceled += instance.OnOpenWall;
            }
        }
    }
    public EarthBendingActions @EarthBending => new EarthBendingActions(this);

    // FireBending
    private readonly InputActionMap m_FireBending;
    private IFireBendingActions m_FireBendingActionsCallbackInterface;
    private readonly InputAction m_FireBending_FireTorch;
    public struct FireBendingActions
    {
        private @AllControls m_Wrapper;
        public FireBendingActions(@AllControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @FireTorch => m_Wrapper.m_FireBending_FireTorch;
        public InputActionMap Get() { return m_Wrapper.m_FireBending; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FireBendingActions set) { return set.Get(); }
        public void SetCallbacks(IFireBendingActions instance)
        {
            if (m_Wrapper.m_FireBendingActionsCallbackInterface != null)
            {
                @FireTorch.started -= m_Wrapper.m_FireBendingActionsCallbackInterface.OnFireTorch;
                @FireTorch.performed -= m_Wrapper.m_FireBendingActionsCallbackInterface.OnFireTorch;
                @FireTorch.canceled -= m_Wrapper.m_FireBendingActionsCallbackInterface.OnFireTorch;
            }
            m_Wrapper.m_FireBendingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @FireTorch.started += instance.OnFireTorch;
                @FireTorch.performed += instance.OnFireTorch;
                @FireTorch.canceled += instance.OnFireTorch;
            }
        }
    }
    public FireBendingActions @FireBending => new FireBendingActions(this);

    // ControlHints
    private readonly InputActionMap m_ControlHints;
    private IControlHintsActions m_ControlHintsActionsCallbackInterface;
    private readonly InputAction m_ControlHints_CheckKM;
    private readonly InputAction m_ControlHints_CheckXC;
    public struct ControlHintsActions
    {
        private @AllControls m_Wrapper;
        public ControlHintsActions(@AllControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CheckKM => m_Wrapper.m_ControlHints_CheckKM;
        public InputAction @CheckXC => m_Wrapper.m_ControlHints_CheckXC;
        public InputActionMap Get() { return m_Wrapper.m_ControlHints; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlHintsActions set) { return set.Get(); }
        public void SetCallbacks(IControlHintsActions instance)
        {
            if (m_Wrapper.m_ControlHintsActionsCallbackInterface != null)
            {
                @CheckKM.started -= m_Wrapper.m_ControlHintsActionsCallbackInterface.OnCheckKM;
                @CheckKM.performed -= m_Wrapper.m_ControlHintsActionsCallbackInterface.OnCheckKM;
                @CheckKM.canceled -= m_Wrapper.m_ControlHintsActionsCallbackInterface.OnCheckKM;
                @CheckXC.started -= m_Wrapper.m_ControlHintsActionsCallbackInterface.OnCheckXC;
                @CheckXC.performed -= m_Wrapper.m_ControlHintsActionsCallbackInterface.OnCheckXC;
                @CheckXC.canceled -= m_Wrapper.m_ControlHintsActionsCallbackInterface.OnCheckXC;
            }
            m_Wrapper.m_ControlHintsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CheckKM.started += instance.OnCheckKM;
                @CheckKM.performed += instance.OnCheckKM;
                @CheckKM.canceled += instance.OnCheckKM;
                @CheckXC.started += instance.OnCheckXC;
                @CheckXC.performed += instance.OnCheckXC;
                @CheckXC.canceled += instance.OnCheckXC;
            }
        }
    }
    public ControlHintsActions @ControlHints => new ControlHintsActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Move;
    private readonly InputAction m_UI_Click;
    private readonly InputAction m_UI_RoomLobby;
    private readonly InputAction m_UI_Paused;
    public struct UIActions
    {
        private @AllControls m_Wrapper;
        public UIActions(@AllControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_UI_Move;
        public InputAction @Click => m_Wrapper.m_UI_Click;
        public InputAction @RoomLobby => m_Wrapper.m_UI_RoomLobby;
        public InputAction @Paused => m_Wrapper.m_UI_Paused;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Click.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @RoomLobby.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRoomLobby;
                @RoomLobby.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRoomLobby;
                @RoomLobby.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRoomLobby;
                @Paused.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPaused;
                @Paused.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPaused;
                @Paused.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPaused;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @RoomLobby.started += instance.OnRoomLobby;
                @RoomLobby.performed += instance.OnRoomLobby;
                @RoomLobby.canceled += instance.OnRoomLobby;
                @Paused.started += instance.OnPaused;
                @Paused.performed += instance.OnPaused;
                @Paused.canceled += instance.OnPaused;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_XboxControllerSchemeIndex = -1;
    public InputControlScheme XboxControllerScheme
    {
        get
        {
            if (m_XboxControllerSchemeIndex == -1) m_XboxControllerSchemeIndex = asset.FindControlSchemeIndex("XboxController");
            return asset.controlSchemes[m_XboxControllerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IEarthBendingActions
    {
        void OnSeismicSense(InputAction.CallbackContext context);
        void OnOpenWall(InputAction.CallbackContext context);
    }
    public interface IFireBendingActions
    {
        void OnFireTorch(InputAction.CallbackContext context);
    }
    public interface IControlHintsActions
    {
        void OnCheckKM(InputAction.CallbackContext context);
        void OnCheckXC(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnRoomLobby(InputAction.CallbackContext context);
        void OnPaused(InputAction.CallbackContext context);
    }
}
