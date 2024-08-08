using System;
using System.IO;
using CCAnon.Maskers;
using CCAnon.Windows;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using ECommons;
using Module = System.Reflection.Module;

namespace CCAnon;

public sealed class Plugin : IDalamudPlugin
{

    private const string CommandName = "/panon";

    public Configuration Configuration { get; init; }
    
    private ICommandManager CommandManager { get; init; }
    public static IDalamudPluginInterface PluginInterface { get; set; }
    public static NameMasker NameMasker { get; set; }
    public static AppearanceMasker AppearanceMasker { get; set; }


    public readonly WindowSystem WindowSystem = new("CCAnon");
    private ConfigWindow ConfigWindow { get; init; }
    private PortraitHidingWindow PortraitHidingWindow { get; init; }
    private PortraitHider PortraitHider { get; init; }
    private ChatMasker ChatMasker { get; init; }
    public Plugin(IDalamudPluginInterface pluginInterface,
                  ICommandManager commandManager)
    {
        Service.Initialize(pluginInterface);
        CommandManager = commandManager;
        PluginInterface = pluginInterface;
        Configuration = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        Configuration.Initialize(PluginInterface);
        
        ConfigWindow = new ConfigWindow(this);
        PortraitHidingWindow = new PortraitHidingWindow(this);

        WindowSystem.AddWindow(ConfigWindow);
        WindowSystem.AddWindow(PortraitHidingWindow);
        
        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "Open Config"
        });
        
        CommandManager.AddHandler("/cctest", new CommandInfo(OnTestCommand)
        {
            HelpMessage = "Test Stuff"
        });

        pluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUI;
        pluginInterface.UiBuilder.Draw += DrawUI;

        NameMasker = new NameMasker(this);
        AppearanceMasker = new AppearanceMasker(this);
        PortraitHider = new PortraitHider(this);
        ChatMasker = new ChatMasker(this);
        
        ECommonsMain.Init(pluginInterface, this, ECommons.Module.All);

    }
    
    private void DrawUI()
    {
        this.WindowSystem.Draw();
    }


    private void OnCommand(string command, string arguments)
    {
        ToggleConfigUI();
    }
    
    

    private void OnTestCommand(string command, string arguments)
    {
        try
        {
            PortraitHider.toggleUI();
            PortraitHider.flipBackUI();

        }
        catch (Exception e)
        {
            Service.PluginLog.Error(e, "error in test command!");
        }

    }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();

        ConfigWindow.Dispose();

        CommandManager.RemoveHandler(CommandName);
        PluginInterface.UiBuilder.OpenConfigUi -= ToggleConfigUI;

    }
    
    public void ToggleConfigUI() => ConfigWindow.Toggle();
}
