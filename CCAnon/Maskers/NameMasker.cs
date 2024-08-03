using System;
using System.Collections.Generic;
using System.Diagnostics;
using Dalamud.Game.Gui.NamePlate;

namespace CCAnon.Maskers;

public class NameMasker
{
    private Plugin plugin;
    public NameMasker(Plugin plugin)
    {
        this.plugin = plugin;
        Service.NamePlateGui.OnNamePlateUpdate += OnNamePlateUpdate;
    }

    private void OnNamePlateUpdate(INamePlateUpdateContext context, IReadOnlyList<INamePlateUpdateHandler> handlers)
    {
        if (!Service.ClientState.IsPvPExcludingDen || !plugin.Configuration.HideNames)
        {
            return;
        }

        foreach (var plate in handlers)
        {
            if(plate.PlayerCharacter == null) continue;
            if(plate.BattleChara == null) continue;
            if(plate.BattleChara.ClassJob.GameData == null) continue;
            if(plate.PlayerCharacter.GameObjectId == Service.ClientState.LocalPlayer.GameObjectId) continue;
            if (!plate.Name.ToString().Contains('》')) //《 and 》
            {
                String job;
                if (plugin.Configuration.AbbreviateJobs)
                {
                    job = plate.BattleChara.ClassJob.GameData.Abbreviation.ToString();
                }
                else
                {
                    job = plate.BattleChara.ClassJob.GameData.NameEnglish.ToString();
                }
                plate.Name = '《' + job + '》';
                plate.Title = "";
                plate.FreeCompanyTag = "";
                plate.RemoveField(NamePlateStringField.Title);
                plate.RemoveField(NamePlateStringField.FreeCompanyTag);
            }
        }
    }
    
}
