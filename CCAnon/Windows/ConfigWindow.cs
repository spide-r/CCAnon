using System;
using System.Numerics;
using Dalamud.Game.ClientState.Keys;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using OtterGui.Widgets;
using VirtualKeyExtensions = OtterGui.Classes.VirtualKeyExtensions;

namespace CCAnon.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;
    public static VirtualKey[] ValidKeys;
    public ConfigWindow(Plugin plugin) : base("CCAnon")
    {
        ValidKeys = Service.KeyState.GetValidVirtualKeys() as VirtualKey[];
        //ValidKeys = Dalamud.Keys.GetValidVirtualKeys().Prepend(VirtualKey.NO_KEY).ToArray();
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(300, 200),
            MaximumSize = new Vector2(500, 500)
        };
        SizeCondition = ImGuiCond.Always;

        Configuration = plugin.Configuration;
    }

    public void Dispose() { }
    
    public override void Draw()
    {
        ImGui.Text("Unfortunately I cannot yet hide player names in party and enemy lists." +
                   "\nPlease look forward to it.");
        ImGui.Separator();
        var hideNames = Configuration.HideNames;
        if (ImGui.Checkbox("Hide Player Names", ref hideNames))
        {
            Configuration.HideNames = hideNames;
            Configuration.Save();
        }

        var abbreviateJobs = Configuration.AbbreviateJobs;
        if (hideNames)
        {
            if (ImGui.Checkbox("Abbreviate Jobs When Hiding names", ref abbreviateJobs))
            {
                Configuration.AbbreviateJobs = abbreviateJobs;
                Configuration.Save();
            }
        }

        var hidePortraits = Configuration.HidePortraits;
        if (ImGui.Checkbox("Hide Portraits", ref hidePortraits))
        {
            Configuration.HidePortraits = hidePortraits;
            Configuration.Save();
        }

        if (hidePortraits)
        {
            ImGui.Text("Two disclaimers: ");
            ImGui.Text("1. Set the keybind yourself. It needs to be a single button. I'm sorry.");
            ImGui.Text("2. This will auto-hide your UI at the start of a match and show it after ~32 seconds. ");
            
            var hideUI = Configuration.HideUiKeybind;
            Widget.KeySelector("Hide UI Keybind", "",  hideUI, key =>
            {
                Service.PluginLog.Info("New Hide UI Keybind: " + key.GetFancyName());
                Configuration.HideUiKeybind = key;
                Configuration.Save();
                Service.PluginLog.Info("New Hide UI Keybind: " + Configuration.HideUiKeybind.GetFancyName());

            }, ValidKeys );
            /*if (ImGui.InputText("Hide UI Keybind.", ref hideUI, 60))
            {
                Configuration.HideUIKeybind = hideUI;
                Configuration.Save();
            }*/
        }
        
        var maskPlayerAppearance = Configuration.MaskPlayerAppearance;
        if (ImGui.Checkbox("Mask Player Appearance", ref maskPlayerAppearance))
        {
            Configuration.MaskPlayerAppearance = maskPlayerAppearance;
            Configuration.Save();
        }
        
        var maskPlayerGlamours = Configuration.MaskPlayerGlamours;
        if (ImGui.Checkbox("Mask Player Glamours", ref maskPlayerGlamours))
        {
            Configuration.MaskPlayerGlamours = maskPlayerGlamours;
            Configuration.Save();
        }

        var maskChat = Configuration.MaskChat;
        if (ImGui.Checkbox("Hide identities of people in chat.", ref maskChat))
        {
            Configuration.MaskChat = maskChat;
            Configuration.Save();
        }
        ImGui.Separator();
        ImGui.BulletText("Thank you to Mutant Standard (CC BY-NC-SA) - https://mutant.tech");
    }
}
