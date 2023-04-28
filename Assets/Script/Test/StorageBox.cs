using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StorageBox : MonoBehaviour
{

    public void OnClickBoard()
    {
        if (_isSpinning)
            return;

        _isSpinning = true;

        Vector3 angle = _boardGo.transform.rotation.eulerAngles;
        angle.z -= 90;
        _boardGo.transform.DORotate(angle, 1).onComplete = SpinningComplete;
    }



    public void OnClickHandle()
    {
        if (_isSpinning)
            return;

        _isOpen = !_isOpen;

        if(_isOpen)
        {
            _tempOpenCheckText.text = "ON";
            if (_index == 0)
                _stampCaseGo.SetActive(true);
            else if (_index == 1)
                _colbellGo.SetActive(true);
            else if (_index == 2)
                _handHammerGo.SetActive(true);
            else if (_index == 3)
                _ballardJournalGo.SetActive(true);
        }
        else
        {
            _tempOpenCheckText.text = "OFF";
            float dis = Vector3.Distance(_boardGo.transform.position, _handHammerGo.transform.position);
            Debug.Log(dis);

            if (Vector3.Distance(_boardGo.transform.position, _stampCaseGo.transform.position) < _boardAnchor)
                _stampCaseGo.SetActive(false);
            if (Vector3.Distance(_boardGo.transform.position, _colbellGo.transform.position) < _boardAnchor)
                _colbellGo.SetActive(false);
            if (Vector3.Distance(_boardGo.transform.position, _handHammerGo.transform.position) < _boardAnchor)
                _handHammerGo.SetActive(false);
            if (Vector3.Distance(_boardGo.transform.position, _ballardJournalGo.transform.position) < _boardAnchor)
                _ballardJournalGo.SetActive(false);
        }

    }


    [SerializeField] private Text _tempOpenCheckText;
    [SerializeField] private GameObject _boardGo;

    [SerializeField] private GameObject _stampCaseGo;
    [SerializeField] private GameObject _colbellGo;
    [SerializeField] private GameObject _handHammerGo;
    [SerializeField] private GameObject _ballardJournalGo;

    private bool _isOpen = false;
    private bool _isSpinning = false;
    private int _index = 1;
    private float _boardAnchor = 2f;

    private void SpinningComplete()
    {
        _isSpinning = false;

        _index++;
        if (_index == 4)
            _index = 0;
    }
}
