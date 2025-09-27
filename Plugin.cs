using BepInEx;
using UnityEngine;

namespace SilklessLib;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        LogUtil.ConsoleLogger = Logger;
        
        SilklessConfig.Bind(Config);
        
        if (!SilklessAPI.Init(Logger))
        {
            LogUtil.LogError("SilklessAPI failed to initialize!");
            Destroy(this);
            return;
        }
        
        LogUtil.LogInfo($"SilklessAPI Initialized.");
        LogUtil.LogInfo($"{MyPluginInfo.PLUGIN_NAME} {MyPluginInfo.PLUGIN_VERSION} loaded.");
    }

    private void Update()
    {
        SilklessAPI.Update(Time.unscaledDeltaTime);
    }
}
