# SilklessLib

A library mod for adding multiplayer functionality to Silksong.

Requires [BepInEx 5.4.23.4](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.4).

View the Nexusmods page [here]().

## 编译前

```shell
cp SilksongPath.props.example SilksongPath.props
```

编辑其中的路径为 Silksong 游戏路径.

## Featured mods

- [SilklessCoopVisual](https://www.nexusmods.com/hollowknightsilksong/mods/73) by nek5 (me)
- SilklessPvP (coming soon) by p11necone
- [Deathlink](https://www.nexusmods.com/hollowknightsilksong/mods/445) by kuba441

## Usage

<details open>
<summary>Example Code Snippet</summary>

```csharp
public class Plugin : BaseUnityPlugin 
{
    public class DummyPacket : SilklessPacket 
    {
        public string Content;
    }
    
    private void Awake() 
    {
        if (LogUtil.ConsoleLogger == null)
            LogUtil.ConsoleLogger = Logger;
        
        if (!SilklessAPI.Init(Logger)) 
        {
            LogUtil.LogError("SilklessAPI failed to initialize!");
            Destroy(this);
            return;
        }
        
        SilklessAPI.OnPlayerJoin += _ => SilklessAPI.SendPacket(new DummyPacket 
        {
            Content = "Hello World!",
        });
        SilklessAPI.AddHandler<DummyPacket>(packet => 
        {
            LogUtil.LogInfo($"Received dummy packet with content={content}", true);
        });
    }
}
```

</details>

## Reference

<details open>
<summary>Field reference for the SilklessAPI class</summary>

| Field name                              | Return type           | Description                                                                                            |
|-----------------------------------------|-----------------------|--------------------------------------------------------------------------------------------------------|
| Initialized                             | bool                  | Returns whether the API has been initialized before.                                                   |
| Connected                               | bool                  | Returns whether the current connector is enabled.                                                      |
| Ready                                   | bool                  | Returns Initialized && Connected, use this to know if SilklessAPI can currently send and receive data. |
| PlayerIDs                               | HashSet&lt;string&gt; | Returns set of connected players.                                                                      |
| GetId()                                 | string                | Returns the ID assigned by the current connector.                                                      |
| GetUsername()                           | string                | Returns the username assigned by the current connector.                                                |
| Init(ManualLogSource?)                  | bool                  | Attempts to initialize the API and the connector, returns false if unsuccessful.                       |
| Enable()                                | bool                  | Attempts to enable the connector, returns false if unsuccessful.                                       |
| Disable()                               | bool                  | Attempts to disconnect and disable the connector, returns false if unsuccessful.                       |
| Toggle()                                | bool                  | Returns either Enable() or Disable() depending on state.                                               |
| SendPacket&lt;T&gt;(T)                  | bool                  | Attempts to send SilklessPacket through the current connector, returns false if unsuccessful.          |
| AddHandler&lt;T&gt;(Action&lt;T&gt;)    | bool                  | Attempts to add handler for class T, returns false if unsuccessful.                                    |
| RemoveHandler&lt;T&gt;(Action&lt;T&gt;) | bool                  | Attempts to remove handler for class T, return false if unsuccessful.                                  |
| OnEnable                                | Action                | Called when Enable() is called, use this to add handlers and initialize local state.                   |
| OnDisable                               | Action                | Called when Disable() is called, use this to remove handlers and reset local state.                    |
| OnPlayerJoin                            | Action&lt;string&gt;  | Called when a new player joins.                                                                        |
| OnPlayerLeave                           | Action&lt;string&gt;  | Called when a player times out.                                                                        |

</details>

## License

This repository is licensed under CC BY-NC-SA.

See [here](./License) for details.
