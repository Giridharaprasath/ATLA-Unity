// GENERATED AUTOMATICALLY FROM 'Assets/Giri - Old/Input/SampleControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @SampleControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @SampleControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""SampleControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""4bd6e822-57f6-4add-b414-32166aeed465"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""d2599ceb-ad8f-497e-b75b-5609700a0777"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""65cd55b9-dd2e-4ab1-892b-825f40f2272f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""aaa67381-c9d8-49d4-b88a-99763bf1587d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""74e42cb6-0bbe-46f9-9b8b-0d1cce0097a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""f0b72f5b-0b68-4970-818e-b56df4570677"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LobbyList"",
                    ""type"": ""Button"",
                    ""id"": ""0f26aaa6-241d-47b1-a413-3319c1d68a2b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0f33fbd6-af02-4a00-8c05-fe7c40300f5f"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""57b5e278-823e-4970-a24c-4bca3cf0410a"",
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
                    ""id"": ""d38f413b-0579-4255-8e20-c404df62bede"",
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
                    ""id"": ""94da3e25-5722-4139-97ab-77ea31ef1070"",
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
                    ""id"": ""38a7275e-4c0a-4cbf-b4ef-7ab9503d884d"",
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
                    ""id"": ""d423a549-5b73-42a1-8a51-f7e6bc6f1fdf"",
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
                    ""id"": ""f217f183-1840-4609-aa4a-61d525a78023"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dfbf204d-2cff-4e04-9d01-fe236381ccfa"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94dfdc1c-2768-44bc-a8fe-365718f57c45"",
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
                    ""id"": ""2e143b85-3f43-4029-834c-fa16a22529a6"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone,ScaleVector2(x=300,y=300)"",
                    ""groups"": ""Controller"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""093c078b-8184-4451-8e73-4637dcd548f9"",
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
                    ""id"": ""63d03576-676b-4f57-b142-53ed1f338779"",
                    ""path"": ""<XInputController>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""006bccb6-df85-45be-a61c-7559031619aa"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e234a6d2-bc6a-40d9-b37b-a82205d629a8"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce9820b1-a9e0-45c1-8268-492ffc51e7c5"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""LobbyList"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95bbac55-576d-43e4-a5e2-08b014b2c14a"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""LobbyList"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""EarthBending"",
            ""id"": ""91f580bb-ca59-4c57-9890-5569cb8011b9"",
            ""actions"": [
                {
                    ""name"": ""SeismicSense"",
                    ""type"": ""Button"",
                    ""id"": ""425fdfa4-83f9-4658-aa57-468b809250bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e1f37c6c-a378-4ce4-a3ca-bf44ce5ab69f"",
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
                    ""id"": ""2c83344a-8832-4f25-979f-43c410ec1083"",
                    ""path"": ""<XInputController>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""SeismicSense"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""FireBending"",
            ""id"": ""312988d6-8df3-4474-918d-d563337d2626"",
            ""actions"": [
                {
                    ""name"": ""TorchLight"",
                    ""type"": ""Button"",
                    ""id"": ""d10393ee-e9f3-482b-91cf-d562eeba3cce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2943da69-c9d3-4309-9b7e-81688278346e"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""TorchLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b7acc18-73ff-4410-bf6d-3bc1a8d6c07a"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""TorchLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ControlsHint"",
            ""id"": ""035c1132-f29d-468d-b534-9e6ac5212b79"",
            ""actions"": [
                {
                    ""name"": ""CheckKM"",
                    ""type"": ""Value"",
                    ""id"": ""02857256-1c4d-463f-9dd9-93e6667fe884"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CheckJ"",
                    ""type"": ""Value"",
                    ""id"": ""8179c9a1-a586-41d7-946b-7a6faafac53a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2dd1ff0b-3299-408a-a371-df98e178e2de"",
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
                    ""id"": ""88dd4f16-d9f7-4c53-a09f-50a161c17436"",
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
                    ""id"": ""c3c7871d-7b56-450b-864f-7b6c383de61a"",
                    ""path"": ""<XInputController>/button*"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckJ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79a34f92-5d7a-4f79-aa9c-c6e65a2ea967"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckJ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4369465-f355-472c-a277-32ce187a6378"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckJ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c555aa4-33bc-4fde-853e-60258932853f"",
                    ""path"": ""<XInputController>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckJ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2fc7647-8aee-4d22-bdba-5139c93a94fb"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckJ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13af0279-fbd8-4ed5-b7b4-96a1a4d6c1ef"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckJ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60d2aa7b-a986-403b-a98c-8035e94048a1"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckJ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43d4ac92-53ea-466f-8adc-b9a99d8774a3"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckJ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e308ccbe-e377-41c1-ba92-afe11252ef11"",
                    ""path"": ""<XInputController>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckJ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08de5e0f-d9af-48dc-bdc4-08f66f1fc58b"",
                    ""path"": ""<XInputController>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckJ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4458e1e5-b1fd-4760-a423-175362fb12da"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckJ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ab2d631-d3ed-4659-8a7a-b560db768145"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""CheckJ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""0f178243-a072-47a2-8a98-2e71299abbcd"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""47824039-4f40-4292-a2ba-edfca5af364e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""db7edb33-919b-49a2-b09e-5822c710f190"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""89a6899b-65f6-4fc1-a10e-05ac37eb6f6a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0fb71aa4-1bfc-4381-a4f1-1f679ed361df"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""79452e67-c63c-48d7-94eb-a3aeee8f94da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5122408d-16db-41f8-9cf0-18887ea6bbcb"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0668394-2391-4c81-8921-afdb9a8074a8"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""089e7324-1f64-404f-807c-7e0a6d79211b"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5dbb869-759a-478a-8de4-43de54a9ecb9"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7ba7480-9be0-4a75-a8d8-3e66470a00a5"",
                    ""path"": ""<XInputController>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrow KeyBoard "",
                    ""id"": ""988e4cb2-bf9e-459d-ba9f-40b6bd3d8fa5"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f8da102f-42af-40ac-b228-cc4c6bb0e4bc"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""006b03fd-aa37-42e8-8f0b-777b66c9e283"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c62c9cf2-9dc3-4133-a9b5-26af5bfc6551"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6941289e-7481-44a0-937d-ed4693973c65"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0d80922a-4d23-438f-8a87-7639ce21443a"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ded83118-b302-48ba-8942-16ccafd78f6b"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Click"",
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
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<XboxOneGampadiOS>"",
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
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
        m_Player_LobbyList = m_Player.FindAction("LobbyList", throwIfNotFound: true);
        // EarthBending
        m_EarthBending = asset.FindActionMap("EarthBending", throwIfNotFound: true);
        m_EarthBending_SeismicSense = m_EarthBending.FindAction("SeismicSense", throwIfNotFound: true);
        // FireBending
        m_FireBending = asset.FindActionMap("FireBending", throwIfNotFound: true);
        m_FireBending_TorchLight = m_FireBending.FindAction("TorchLight", throwIfNotFound: true);
        // ControlsHint
        m_ControlsHint = asset.FindActionMap("ControlsHint", throwIfNotFound: true);
        m_ControlsHint_CheckKM = m_ControlsHint.FindAction("CheckKM", throwIfNotFound: true);
        m_ControlsHint_CheckJ = m_ControlsHint.FindAction("CheckJ", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Select = m_UI.FindAction("Select", throwIfNotFound: true);
        m_UI_Back = m_UI.FindAction("Back", throwIfNotFound: true);
        m_UI_Navigate = m_UI.FindAction("Navigate", throwIfNotFound: true);
        m_UI_Move = m_UI.FindAction("Move", throwIfNotFound: true);
        m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Pause;
    private readonly InputAction m_Player_LobbyList;
    public struct PlayerActions
    {
        private @SampleControls m_Wrapper;
        public PlayerActions(@SampleControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputAction @LobbyList => m_Wrapper.m_Player_LobbyList;
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
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Run.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Pause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @LobbyList.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLobbyList;
                @LobbyList.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLobbyList;
                @LobbyList.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLobbyList;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @LobbyList.started += instance.OnLobbyList;
                @LobbyList.performed += instance.OnLobbyList;
                @LobbyList.canceled += instance.OnLobbyList;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // EarthBending
    private readonly InputActionMap m_EarthBending;
    private IEarthBendingActions m_EarthBendingActionsCallbackInterface;
    private readonly InputAction m_EarthBending_SeismicSense;
    public struct EarthBendingActions
    {
        private @SampleControls m_Wrapper;
        public EarthBendingActions(@SampleControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SeismicSense => m_Wrapper.m_EarthBending_SeismicSense;
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
            }
            m_Wrapper.m_EarthBendingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SeismicSense.started += instance.OnSeismicSense;
                @SeismicSense.performed += instance.OnSeismicSense;
                @SeismicSense.canceled += instance.OnSeismicSense;
            }
        }
    }
    public EarthBendingActions @EarthBending => new EarthBendingActions(this);

    // FireBending
    private readonly InputActionMap m_FireBending;
    private IFireBendingActions m_FireBendingActionsCallbackInterface;
    private readonly InputAction m_FireBending_TorchLight;
    public struct FireBendingActions
    {
        private @SampleControls m_Wrapper;
        public FireBendingActions(@SampleControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TorchLight => m_Wrapper.m_FireBending_TorchLight;
        public InputActionMap Get() { return m_Wrapper.m_FireBending; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FireBendingActions set) { return set.Get(); }
        public void SetCallbacks(IFireBendingActions instance)
        {
            if (m_Wrapper.m_FireBendingActionsCallbackInterface != null)
            {
                @TorchLight.started -= m_Wrapper.m_FireBendingActionsCallbackInterface.OnTorchLight;
                @TorchLight.performed -= m_Wrapper.m_FireBendingActionsCallbackInterface.OnTorchLight;
                @TorchLight.canceled -= m_Wrapper.m_FireBendingActionsCallbackInterface.OnTorchLight;
            }
            m_Wrapper.m_FireBendingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TorchLight.started += instance.OnTorchLight;
                @TorchLight.performed += instance.OnTorchLight;
                @TorchLight.canceled += instance.OnTorchLight;
            }
        }
    }
    public FireBendingActions @FireBending => new FireBendingActions(this);

    // ControlsHint
    private readonly InputActionMap m_ControlsHint;
    private IControlsHintActions m_ControlsHintActionsCallbackInterface;
    private readonly InputAction m_ControlsHint_CheckKM;
    private readonly InputAction m_ControlsHint_CheckJ;
    public struct ControlsHintActions
    {
        private @SampleControls m_Wrapper;
        public ControlsHintActions(@SampleControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CheckKM => m_Wrapper.m_ControlsHint_CheckKM;
        public InputAction @CheckJ => m_Wrapper.m_ControlsHint_CheckJ;
        public InputActionMap Get() { return m_Wrapper.m_ControlsHint; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControlsHintActions set) { return set.Get(); }
        public void SetCallbacks(IControlsHintActions instance)
        {
            if (m_Wrapper.m_ControlsHintActionsCallbackInterface != null)
            {
                @CheckKM.started -= m_Wrapper.m_ControlsHintActionsCallbackInterface.OnCheckKM;
                @CheckKM.performed -= m_Wrapper.m_ControlsHintActionsCallbackInterface.OnCheckKM;
                @CheckKM.canceled -= m_Wrapper.m_ControlsHintActionsCallbackInterface.OnCheckKM;
                @CheckJ.started -= m_Wrapper.m_ControlsHintActionsCallbackInterface.OnCheckJ;
                @CheckJ.performed -= m_Wrapper.m_ControlsHintActionsCallbackInterface.OnCheckJ;
                @CheckJ.canceled -= m_Wrapper.m_ControlsHintActionsCallbackInterface.OnCheckJ;
            }
            m_Wrapper.m_ControlsHintActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CheckKM.started += instance.OnCheckKM;
                @CheckKM.performed += instance.OnCheckKM;
                @CheckKM.canceled += instance.OnCheckKM;
                @CheckJ.started += instance.OnCheckJ;
                @CheckJ.performed += instance.OnCheckJ;
                @CheckJ.canceled += instance.OnCheckJ;
            }
        }
    }
    public ControlsHintActions @ControlsHint => new ControlsHintActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Select;
    private readonly InputAction m_UI_Back;
    private readonly InputAction m_UI_Navigate;
    private readonly InputAction m_UI_Move;
    private readonly InputAction m_UI_Click;
    public struct UIActions
    {
        private @SampleControls m_Wrapper;
        public UIActions(@SampleControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_UI_Select;
        public InputAction @Back => m_Wrapper.m_UI_Back;
        public InputAction @Navigate => m_Wrapper.m_UI_Navigate;
        public InputAction @Move => m_Wrapper.m_UI_Move;
        public InputAction @Click => m_Wrapper.m_UI_Click;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Back.started -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                @Navigate.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Move.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Click.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
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
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnLobbyList(InputAction.CallbackContext context);
    }
    public interface IEarthBendingActions
    {
        void OnSeismicSense(InputAction.CallbackContext context);
    }
    public interface IFireBendingActions
    {
        void OnTorchLight(InputAction.CallbackContext context);
    }
    public interface IControlsHintActions
    {
        void OnCheckKM(InputAction.CallbackContext context);
        void OnCheckJ(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnNavigate(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
    }
}
