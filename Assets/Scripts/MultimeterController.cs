using System.Collections;
using UnityEngine;

public class MultimeterController : MonoBehaviour
{
    [SerializeField] private MultimeterModel model;
    [SerializeField] private MultimeterView view;
    [SerializeField] private GameObject highLighter;
    private bool _isMultimeterWorking = false;

    private void Start()
    {
        model.OnStateChanged += view.UpdateDisplay;
    }

    private IEnumerator ActivateForOneSecond()
    {
        highLighter.SetActive(true);
        yield return new WaitForSeconds(1f);
        highLighter.SetActive(false);
    }

    private void Update()
    {
        if (_isMultimeterWorking && Input.mouseScrollDelta.y != 0)
        {
            int enumLength = System.Enum.GetValues(typeof(MultimeterState)).Length;
            int newState = ((int)model.State + (Input.mouseScrollDelta.y > 0 ? 1 : -1) + enumLength) % enumLength;
            model.SetState((MultimeterState)newState);
        }
    }

    public void SetMultimeterWorking(bool state)
    {
        if (state)
        {
            StartCoroutine(ActivateForOneSecond());
        }

        _isMultimeterWorking = state;
    }
}