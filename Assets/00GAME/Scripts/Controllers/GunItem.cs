using UnityEngine;
using UnityEngine.UI;

public class GunItem : MonoBehaviour
{
	[SerializeField] GunData _gunData;
    [SerializeField] GameObject _lock;
    [SerializeField] Image _itemIcon;

    void Start()
	{
		_itemIcon.sprite = _gunData.GetSpriteGun();
		if (GameManager.instance.idGun.Contains(" " + _gunData.GetGunID() + " "))
		{
			_gunData.SetUnlocked(true);
		}
		else
		{
			_gunData.SetUnlocked(false);
		}
		UpdateLock();    
	}

	public void OnItemClick()
	{
		AudioManager.instance.PlaySound(AudioManager.instance.UIClips[2],0,false);
		if (_gunData.isUnlocked())
		{
			SpawnController.instance._bullet = _gunData.GetBullet();
            PlayerController.instance.GetComponentInChildren<GunController>().SetGunData(_gunData);
			PlayerPrefs.SetString(CONSTANTS.IDGUNLAST, _gunData.GetGunID());
            Observer.instance.Notify(CONSTANTS.UISTORE_PLAYER, null);
        }
		else
		{
			Observer.instance.Notify(CONSTANTS.UISTORE_NOTI,this);
		}
	}

    public GunData GetData()
    {
        return _gunData;
    }
    public void UpdateLock()
    {
        if (_gunData.isUnlocked())
        {
            _lock.SetActive(false);
            return;
        }
        _lock.SetActive(true);
    }
}
