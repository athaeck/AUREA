using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUi : MonoBehaviour
{

    private GameObject _ui;

    void Start()
    {


        OnStart();
    }

    protected virtual void OnStart()
    {

    }




    private void OnDestroy()
    {
        //BaseGameController.Instance.UiController.RemoveBaseUi(this);
        Destroy();
    }

    protected virtual void Destroy()
    {

    }

 

    private void ValidateStateChange(bool state)
    {
        if (state)
        {
            BeforeUiOpened();
        }
        else
        {
            BeforeUiClosed();
        }

        _ui.SetActive(state);

        if (state)
        {
            AfterUiOpened();
        }
        else
        {
            AfterUiClosed();
        }
    }

    public virtual void ExtendableBehaviour(bool state) { }

    public GameObject GetUi() { return _ui; }

    protected abstract void BeforeUiOpened();
    protected abstract void AfterUiOpened();
    protected abstract void AfterUiClosed();
    protected abstract void BeforeUiClosed();


}