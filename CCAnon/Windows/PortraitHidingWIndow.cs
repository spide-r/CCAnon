using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace CCAnon.Windows;

public class PortraitHidingWindow : Window, IDisposable
{
    private Configuration Configuration;
    
    //This will be used eventually once I figure out how to show Imgui windows despite GUI being hidden
    public PortraitHidingWindow(Plugin plugin) : base("Portrait Hider")
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
        ImGui.Text("UI is currently being hidden by the portrait hider!" +
                   "\nThe UI will automatically re-enabled shortly.");
    }
}
