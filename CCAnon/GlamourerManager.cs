using System;
using System.Collections.Generic;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Plugin;
using FFXIVClientStructs.FFXIV.Client.Game;
using Glamourer.Api.Enums;
using Glamourer.Api.IpcSubscribers;

namespace CCAnon;


internal class GlamourerManager : IDisposable
{

    private ApplyState ApplyState;

    public GlamourerManager(IDalamudPluginInterface pluginInterface)
    {

        ApplyState = new ApplyState(pluginInterface);

    }

    public void ApplyCustomization(ushort objectIndex, String state)
    {
        ApplyState.Invoke(state, objectIndex, 0, ApplyFlag.Equipment | ApplyFlag.Once); //when i tested things the ApplyFlag is actually flip-flopped
    }
    
    public void ApplyOutfit(ushort objectIndex, String outfit)
    {
        ApplyState.Invoke(outfit, objectIndex, 0, ApplyFlag.Customization | ApplyFlag.Once); 
    }

    
    public void Dispose() { }
}
		
