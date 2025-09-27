using BepInEx.Configuration;

namespace SilklessLib;

public static class SilklessConfig
{
    public enum EConnectionType { SteamP2P, Standalone, Debug }

    // misc
    public static bool PrintDebugOutput;

    // connection
    public static EConnectionType ConnectionType;
    public static float ConnectionTimeout;
    public static int TickRate;

    // standalone
    public static string StandaloneIP;
    public static int StandalonePort;
    public static string StandaloneUsername;

    public static string Version;

    public static void Bind(ConfigFile config)
    {
        PrintDebugOutput = config.Bind("General", "Print Debug Output", true, "Enables advanced logging to help find bugs.").Value;
        
        TickRate = config.Bind("General", "Tick Rate", 20, "Messages per second sent to the server.").Value;
        ConnectionTimeout = config.Bind<float>("General", "Connection Timeout", 5, "Set after how many seconds inactive users are kicked.").Value;
        ConnectionType = config.Bind("General", "Connection Type", EConnectionType.SteamP2P, "Method used to connect with other players.").Value;

        StandaloneIP = config.Bind("Standalone", "Server IP Address", "127.0.0.1", "IP Address of the standalone server.").Value;
        StandalonePort = config.Bind("Standalone", "Server Port", 45565, "Port of the standalone server.").Value;
        StandaloneUsername = config.Bind("Standalone", "Username", "unknown", "Username shown above your head when connecting through echoserver.").Value;
        
        Version = MyPluginInfo.PLUGIN_VERSION;
    }
}
