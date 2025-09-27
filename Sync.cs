using UnityEngine;

namespace SilklessLib;

public abstract class Sync : MonoBehaviour
{
    protected virtual int TickRate => SilklessConfig.TickRate;
    
    private float _tickTimeout;
    
    protected void Awake()
    {
        if (!SilklessAPI.Init())
        {
            Destroy(this);
            return;
        }

        SilklessAPI.OnEnable += OnEnable;
        SilklessAPI.OnDisable += Reset;
        SilklessAPI.OnDisable += OnDisable;
        SilklessAPI.OnPlayerJoin += OnPlayerJoin;
        SilklessAPI.OnPlayerLeave += OnPlayerLeave;
    }

    protected void OnDestroy()
    {
        SilklessAPI.OnEnable -= OnEnable;
        SilklessAPI.OnDisable -= Reset;
        SilklessAPI.OnDisable -= OnDisable;
        SilklessAPI.OnPlayerJoin -= OnPlayerJoin;
        SilklessAPI.OnPlayerLeave -= OnPlayerLeave;
    }

    protected abstract void OnEnable();
    protected abstract void OnDisable();

    protected abstract void OnPlayerJoin(string id);

    protected abstract void OnPlayerLeave(string id);
    
    protected virtual void Update()
    {
        // tick
        if (SilklessAPI.Ready)
        {
            _tickTimeout -= Time.unscaledDeltaTime;
            while (_tickTimeout <= 0)
            {
                Tick();
                
                _tickTimeout += 1.0f / TickRate;
            }
        }
    }

    protected abstract void Tick();

    protected abstract void Reset();
}