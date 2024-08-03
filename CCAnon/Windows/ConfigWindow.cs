using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace CCAnon.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;

    // We give this window a constant ID using ###
    // This allows for labels being dynamic, like "{FPS Counter}fps###XYZ counter window",
    // and the window ID will always be "###XYZ counter window" for ImGui
    public ConfigWindow(Plugin plugin) : base("CCAnon")
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(100, 100),
            MaximumSize = new Vector2(300, 200)
        };
        SizeCondition = ImGuiCond.Always;

        Configuration = plugin.Configuration;
    }

    public void Dispose() { }
    
    public override void Draw()
    {
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
        
        var maskPlayerAppearance = Configuration.MaskPlayerAppearance;
        if (ImGui.Checkbox("Mask Player Appearance", ref maskPlayerAppearance))
        {
            Configuration.MaskPlayerAppearance = maskPlayerAppearance;
            Configuration.Save();
        }
        
        var maskPlayerGLamours = Configuration.MaskPlayerGlamours;
        if (ImGui.Checkbox("Mask Player Glamours", ref maskPlayerGLamours))
        {
            Configuration.MaskPlayerGlamours = maskPlayerGLamours;
            Configuration.Save();
        }
    }
}
