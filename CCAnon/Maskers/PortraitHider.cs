using System.Collections.Generic;
using Dalamud.Game.ClientState.Keys;
using Dalamud.Game.Config;
using ECommons;
using ECommons.Interop;

namespace CCAnon.Maskers;

public class PortraitHider
{
    private Plugin plugin;

    

    /*
     *     public static readonly Dictionary<uint, CrystallineConflictMap> CrystallineConflictMapLookup = new() {
        { 1032, CrystallineConflictMap.Palaistra },
        { 1058, CrystallineConflictMap.Palaistra }, //custom match
        { 1033, CrystallineConflictMap.VolcanicHeart },
        { 1059, CrystallineConflictMap.VolcanicHeart }, //custom match
        { 1034, CrystallineConflictMap.CloudNine },
        { 1060, CrystallineConflictMap.CloudNine }, //custom match
        { 1116, CrystallineConflictMap.ClockworkCastleTown },
        { 1117, CrystallineConflictMap.ClockworkCastleTown }, //custom match
        { 1138, CrystallineConflictMap.RedSands },
        { 1139, CrystallineConflictMap.RedSands } //custom match
    };
     */
    public PortraitHider(Plugin plugin)
    {
        this.plugin = plugin;
        Service.ClientState.TerritoryChanged += newTerritory;
        Service.ClientState.EnterPvP += enterPvP;
    }

    private void newTerritory(ushort territory)
    {
        Service.PluginLog.Info("New Territory");
        if (Service.isInCC())
        {
            Service.PluginLog.Info("In CC");
            toggleUI();
        }
    }

    public void enterPvP()
    {
        Service.PluginLog.Info("Entered PVP");
        if (Service.isInCC())
        {
            Service.PluginLog.Info("In CC");
            toggleUI();
        }
    }
    //the time it takes for the portraits to cycle is roughly 30s - lets make it 35

    public void toggleUI()
    {
        if (!plugin.Configuration.HidePortraits)
        {
            return;
        }
        Service.PluginLog.Info("Hiding portraits enabled");

        if (plugin.Configuration.HideUiKeybind != VirtualKey.NO_KEY)
        {
            Service.PluginLog.Info("Toggling UI");
            Service.ChatGui.Print("UI has been toggled by CCAnon!", "CCAnon", 100);
            ECommons.Automation.WindowsKeypress.SendKeypress((LimitedKeys) plugin.Configuration.HideUiKeybind);
        }
        else
        {
            
            Service.PluginLog.Info("Hiding UI not sent");
            Service.ChatGui.Print("UI has not been toggled!", "CCAnon", 100);

        }
    }
}
