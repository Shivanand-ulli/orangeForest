using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Music Controls")]
    [SerializeField] Button musicOn;
    [SerializeField] Button musicOff;

    [Header("SFX Controls")]
    [SerializeField] Button sfxOn;
    [SerializeField] Button sfxOff;

    public GameObject extraSettingPanel;
    private bool extraSettings = false;

    void Start()
    {
        // Assign button listeners
        musicOn?.onClick.AddListener(() => SetMusicMuted(true));
        musicOff?.onClick.AddListener(() => SetMusicMuted(false));
        sfxOn?.onClick.AddListener(() => SetSfxMuted(true));
        sfxOff?.onClick.AddListener(() => SetSfxMuted(false));

        AudioManager.Instance.musicSource.mute = AudioManager.GetMusicState();
        AudioManager.Instance.sfxSource.mute = AudioManager.GetSfxState();

        // Updating button visible states
        UpdateMusicButtonState(AudioManager.GetMusicState());
        UpdateSfxButtonState(AudioManager.GetSfxState());
    }

    // Toggle to show additional settings
    public void SetExtraPanel()
    {
        extraSettings = !extraSettings;
        extraSettingPanel.SetActive(extraSettings);
    }

    // SetMusic States
    private void SetMusicMuted(bool isMuted)
    {
        AudioManager.Instance.ToggleMusic(isMuted);
        UpdateMusicButtonState(isMuted);
    }

    // Update visibility of music buttons
    private void UpdateMusicButtonState(bool isMuted)
    {
        musicOn.gameObject.SetActive(!isMuted);
        musicOff.gameObject.SetActive(isMuted);
    }

    // SetSfx States
    private void SetSfxMuted(bool isMuted)
    {
        AudioManager.Instance.ToggleSfx(isMuted);
        UpdateSfxButtonState(isMuted);
    }

    // Update visibility of sfx buttons
    private void UpdateSfxButtonState(bool isMuted)
    {
        sfxOn.gameObject.SetActive(!isMuted);
        sfxOff.gameObject.SetActive(isMuted);
    }
}
