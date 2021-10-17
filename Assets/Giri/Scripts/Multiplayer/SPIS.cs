using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using TMPro;
using UnityEngine.UI;

// Steam Player Information Script
public class SPIS : MonoBehaviour
{
    [SerializeField]
    private TMP_Text userNameText;
    [SerializeField]
    private RawImage userProfilePhoto;

    protected Callback<AvatarImageLoaded_t> avatarImageLoaded;

    private void OnEnable()
    {
        avatarImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnAvatarImageLoaded);
    }

    void Start()
    {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            userNameText.text = name;
            int ret = SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID());
            userProfilePhoto.texture = GetSteamImageAsTexture(ret);
        }
    }

    void OnAvatarImageLoaded(AvatarImageLoaded_t callback)
    {
        userProfilePhoto.texture = GetSteamImageAsTexture(callback.m_iImage);
    }

    private Texture2D GetSteamImageAsTexture(int iImage)
    {
        Texture2D texture = null;

        bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);

        if (isValid)
        {
            byte[] image = new byte[width * height * 4];

            isValid = SteamUtils.GetImageRGBA(iImage, image, (int)(width * height * 4));

            if (isValid)
            {
                texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
                texture.LoadRawTextureData(image);
                texture.Apply();
            }
        }

        return texture;
    }
}
