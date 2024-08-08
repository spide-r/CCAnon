using System;
using Dalamud.Configuration;
using Dalamud.Game.ClientState.Keys;
using Dalamud.Plugin;

namespace CCAnon;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    public bool HidePortraits { get; set; } = true;
    public bool HideNames { get; set; } = true;
    public bool AbbreviateJobs { get; set; } = false;
    public bool MaskPlayerAppearance { get; set; } = true;
    public bool MaskPlayerGlamours { get; set; } = true;
    public bool MaskChat { get; set; } = true;
    public VirtualKey HideUiKeybind { get; set; } = VirtualKey.NO_KEY;

    // the below exist just to make saving less cumbersome
    
    [NonSerialized]
    private IDalamudPluginInterface? pluginInterface;
    
    public void Initialize(IDalamudPluginInterface pluginInterface)
    {
        this.pluginInterface = pluginInterface;
    }
    public void Save()
    {
        pluginInterface.SavePluginConfig(this);
    }
}
