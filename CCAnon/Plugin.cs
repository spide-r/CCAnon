using System.IO;
using CCAnon.Maskers;
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
    public static NameMasker NameMasker { get; set; }
    public static AppearanceMasker AppearanceMasker { get; set; }


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
        AppearanceMasker.TerritoryChanged(0);
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
