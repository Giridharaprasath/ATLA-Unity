using UnityEngine;
using Steamworks;
using TMPro;
using UnityEngine.UI;

public class PlayerSteamInfo : MonoBehaviour
{
    [SerializeField]
    private TMP_Text playerNameText;
    [SerializeField]
    private RawImage playerProfilePhoto;

    protected Callback<AvatarImageLoaded_t> avatarImageLoaded;

    private void OnEnable()
    {
        avatarImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnAvatarImageLoaded);
    }

    private void Start()
    {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            playerNameText.text = name;
            int ret = SteamFriends.GetLargeFriendAvatar(SteamUser.GetSteamID());
            playerProfilePhoto.texture = GetSteamImageAsTexture(ret);
        }
    }

    private void OnAvatarImageLoaded(AvatarImageLoaded_t callback)
    {
        playerProfilePhoto.texture = GetSteamImageAsTexture(callback.m_iImage);
    }

    Texture2D GetSteamImageAsTexture(int iImage)
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
