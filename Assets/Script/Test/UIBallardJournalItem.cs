using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBallardJournalItem : MonoBehaviour
{
    public void Refresh()
    {
        Hide();

        if (_type == BallardJournallPageType.None)
        {
        }
        else if (_type == BallardJournallPageType.Intro)
        {
            _introGo.SetActive(true);
        }
        else if (_type == BallardJournallPageType.Local)
        {
            _localGo.SetActive(true);
        }
        else if (_type == BallardJournallPageType.Party)
        {
            _partyGo.SetActive(true);
        }
    }
    public void Init(BallardJournalItem item)
    {
        _type = item.Type;
        
        if (_type == BallardJournallPageType.Intro)
        {
            SetIntroData(item);
        }
        else if (_type == BallardJournallPageType.Local)
        {
            SetLocalData(item);
        }
        else if (_type == BallardJournallPageType.Party)
        {
            SetPartyData(item);
        }
    }

    public void ShowNoneImage()
    {
        Hide();

        _noneGo.SetActive(true);
    }

    private void Hide()
    {
        _introGo.SetActive(false);
        _localGo.SetActive(false);
        _partyGo.SetActive(false);
    }


    [SerializeField] private GameObject _introGo;
    [SerializeField] private Image _introIcon;
    [SerializeField] private Text _introText;

    [SerializeField] private GameObject _localGo;
    [SerializeField] private Image _localIcon;
    [SerializeField] private Text _localText;

    [SerializeField] private GameObject _partyGo;
    [SerializeField] private Image _partyIcon;
    [SerializeField] private Text _partyText;

    [SerializeField] private GameObject _noneGo;
    private BallardJournallPageType _type;

    private void SetIntroData(BallardJournalItem item)
    {
        BallardJournalIntroItem introItem = (BallardJournalIntroItem)item;

        _introText.text = introItem.IntroName;
        _introIcon.sprite = introItem.Icon;
    }
    private void SetLocalData(BallardJournalItem item)
    {
        BallardJournalLocalItem localItem = (BallardJournalLocalItem)item;

        _localText.text = localItem.LocalName;
        _localIcon.sprite = localItem.Icon;
    }
    private void SetPartyData(BallardJournalItem item)
    {
        BallardJournalPartyItem partyItem = (BallardJournalPartyItem)item;

        _partyText.text = partyItem.PartyName;
        _partyIcon.sprite = partyItem.Icon;
    }
}
