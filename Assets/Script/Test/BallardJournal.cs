using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BallardJournal : MonoBehaviour
{
    ////////////////////////////////////////////////////////
    // public
    public void OnClickLeftArea()
    {
        if (_currentPage == 0 || _currentPage == 1)
            return;

        Debug.Log("_currentPage : " + _currentPage);

        _ballardJournalItems[_currentPage].Hide();
        _ballardJournalItems[_currentPage + 1].Hide();

        _currentPage -= 2;

        _ballardJournalItems[_currentPage].Show();
        _ballardJournalItems[_currentPage + 1].Show();
        Refresh();
    }
    public void OnClickRightArea()
    {
        if (_currentPage == _lastPage || _currentPage == _lastPage-1)
            return;

        Debug.Log("_currentPage : " + _currentPage);

        _ballardJournalItems[_currentPage].Hide();
        _ballardJournalItems[_currentPage+1].Hide();

        _currentPage += 2;

        _ballardJournalItems[_currentPage].Show();
        _ballardJournalItems[_currentPage + 1].Show();
        Refresh();
    }

    public void OnClickCountry()
    {
        _currentPage = 3;
        Refresh();
    }

    public void OnClickCook()
    {
        _currentPage = 6;
        Refresh();
    }

    public void OnClickEquipment()
    {
        _currentPage = 10;
        Refresh();
    }
    
    public void OnClickIndex()
    {
        _currentPage = 0;
        Refresh();
    }


    ////////////////////////////////////////////////////////
    // private

    [SerializeField] private Text _pageText;
    [SerializeField] private List<BallardJournalItem> _ballardJournalIntroItems;
    [SerializeField] private BallardJournalLocalItem _ballardJournalLocalItem;
    [SerializeField] private BallardJournalPartyItem _ballardJournalPartyItem;
    [SerializeField] private List<Transform> _areaRoots;

    private int _currentPage = 0;
    private int _lastPage;
    private Dictionary<int, BallardJournalItem> _ballardJournalItems = new Dictionary<int, BallardJournalItem>();

    private void Start()
    {
        Refresh();
        Init();
    }

    private void Refresh()
    {
        this._pageText.text = _currentPage.ToString();
    }

    private void Init()
    {
        SpecDataManager.instance.onDataLoadFinished.AddListener(() =>
        {
            CreateItems();

            _ballardJournalItems[_currentPage].Show();
            _ballardJournalItems[_currentPage + 1].Show();
        });
        SpecDataManager.instance.Init(this);


    }

    // 책 내용 생성
    private void CreateItems()
    {
        int pageCount = 0;

        // 인트로 아이템 생성
        for (int idx = 0; idx < _ballardJournalIntroItems.Count; idx++)
        {
            var introItem = Instantiate(_ballardJournalIntroItems[idx], _areaRoots[idx % 2]);
            //introItem.Init(idx);
            _ballardJournalItems.Add(idx, introItem);
            introItem.Hide();
            pageCount++;
        }


        // 지역 아이템 생성
        foreach (var localData in SpecDataManager.instance.LocalDatas)
        {
            var localItem = Instantiate(_ballardJournalLocalItem, _areaRoots[pageCount % 2]);
            ((BallardJournalLocalItem)localItem).Init(localData.local);
            _ballardJournalItems.Add(pageCount, localItem);
            localItem.Hide();
            pageCount++;


            var partyDatas = SpecDataManager.instance.PartyDatas.ToList().FindAll(x => x.local == localData.local);
            // 지역에 속한 세력 아이템 생성
            foreach (var partyData in partyDatas)
            {
                var partyItem = Instantiate(_ballardJournalPartyItem, _areaRoots[pageCount % 2]);
                ((BallardJournalPartyItem)partyItem).Init(partyData.party);
                _ballardJournalItems.Add(pageCount, partyItem);
                partyItem.Hide();
                pageCount++;
            }
        }


        // 홀수 페이지 처리
        if(pageCount % 2 == 1)
        {
            var introItem = Instantiate(_ballardJournalIntroItems[0], _areaRoots[pageCount % 2]);
            //introItem.Init(pageCount);
            _ballardJournalItems.Add(pageCount, _ballardJournalIntroItems[0]);
            introItem.Hide();
            pageCount++;
        }

        _lastPage = pageCount - 1;
    }

}
