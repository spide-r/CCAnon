using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dalamud.Game.ClientState.Keys;
using Dalamud.Game.Config;
using ECommons;
using ECommons.Interop;

namespace CCAnon.Maskers;

public class PortraitHider
{
    private Plugin plugin;
    public PortraitHider(Plugin plugin)
    {
        this.plugin = plugin;
        Service.ClientState.TerritoryChanged += newTerritory;
    }

    private void newTerritory(ushort territory)
    {
        Service.PluginLog.Info("New Territory");
        if (Service.isInCC())
        {
            Service.PluginLog.Info("In CC");
            toggleUI();
            flipBackUI();
        }
    }

    public void flipBackUI()
    {
        Task.Delay(TimeSpan.FromMilliseconds(32 * 1000)).ContinueWith(task =>
        {
            toggleUI();
        });
    }

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
            Service.ChatGui.Print("UI has been toggled by CCAnon!", "CCAnon", 2);
            ECommons.Automation.WindowsKeypress.SendKeypress((LimitedKeys) plugin.Configuration.HideUiKeybind);
        }
        else
        {
            
            Service.PluginLog.Info("Hiding UI not sent");
            Service.ChatGui.Print("UI has not been toggled!", "CCAnon", 2);

        }
    }
}
