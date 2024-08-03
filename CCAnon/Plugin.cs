using System.IO;
using CCAnon.Windows;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace CCAnon;

public sealed class Plugin : IDalamudPlugin
{

    private const string CommandName = "/panon";

    public Configuration Configuration { get; init; }
    
    private ICommandManager CommandManager { get; init; }
    public static IDalamudPluginInterface PluginInterface { get; set; }


    public readonly WindowSystem WindowSystem = new("CCAnon");
    private ConfigWindow ConfigWindow { get; init; }
    public Plugin(IDalamudPluginInterface pluginInterface,
                  ICommandManager commandManager)
    {
        Service.Initialize(pluginInterface);
        CommandManager = commandManager;
        PluginInterface = pluginInterface;
        Configuration = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        Configuration.Initialize(PluginInterface);
        
        ConfigWindow = new ConfigWindow(this);

        WindowSystem.AddWindow(ConfigWindow);
        
        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand));

        pluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUI;

    }
    

    private void OnCommand(string command, string arguments)
    {
        ToggleConfigUI();
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
