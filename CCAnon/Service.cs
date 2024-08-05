using System.Collections.Generic;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Glamourer.Api.Api;

namespace CCAnon;

internal class Service {

    [PluginService]
    internal static IDataManager DataManager { get; private set; }

    [PluginService]
    internal static IChatGui ChatGui { get; private set; }
    
    [PluginService]
    internal static IPluginLog PluginLog { get; private set; }

    [PluginService]
    internal static IObjectTable ObjectTable { get; private set; }

    [PluginService]
    internal static IPartyList PartyList { get; private set; }

    [PluginService]
    internal static IClientState ClientState { get; private set; }
    
    [PluginService]
    internal static IDalamudPluginInterface DalamudPluginInterface { get; private set; }
    
    [PluginService]
    internal static ICondition Condition { get; private set; }
    
    [PluginService]
    internal static IFramework Framework { get; private set; }
    
    [PluginService]
    internal static IGameInteropProvider GameInteropProvider { get; private set; }
    
    [PluginService]
    internal static INamePlateGui NamePlateGui { get; private set; }

    
    [PluginService]
    internal static IDutyState DutyState { get; private set; }
    
    [PluginService]
    internal static IGameConfig GameConfig { get; private set; }
    
    [PluginService]
    internal static IAddonLifecycle AddonLifecycle { get; private set; }
    
    [PluginService]
    internal static IToastGui ToastGui { get; private set; }
    
    [PluginService]
    internal static IKeyState KeyState { get; private set; }
    
    internal static GlamourerManager GlamourerManager { get; private set; }
    
    private static List<uint> CCMaps = new ()
    {
        1032, 1058, 1033, 1059, 1034, 1060, 1116,  1117, 1138, 1139
    };
    public static bool isInCC()
    {
        return CCMaps.Contains(ClientState.TerritoryType);
    }
    
    
    internal static void Initialize(IDalamudPluginInterface pluginInterface)
    {
        GlamourerManager = new GlamourerManager(pluginInterface);
        pluginInterface.Create<Service>();
    }
}
