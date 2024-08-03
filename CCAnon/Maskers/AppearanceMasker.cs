using System;
using System.Collections.Generic;
using System.Diagnostics;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.Gui.NamePlate;

namespace CCAnon.Maskers;

public class AppearanceMasker
{
    private String hyur =
        "Bh+LCAAAAAAAAArEV01z4jgQ/S++bkjZxtjAbfJBYAuyWWCTw9QeOnYbVBGWV5Inxaby37dlO0wItiGZcu1Nll73627JT+oXa8Q43qNUTCTW0DmzJhEmmsUMpTW0HqEXOV036kAQQ8cbBF4HII47vusA2NDvBkFgnVmXEkGTgyvQSFau7Xodu9+xu0snGLr9od07d7uub3vOb7Y9tG0ymYLS1xHTB/CuO3QH5z0/8N3A28FvYWM8j7eZpK8rVKFkqaGkSZoYCRliNMdIwrM1jIErpKgEF7JY/zNj4RNZsRUZaJnR6hJWyhp+//vMepBM450UGkON0c78+p+MpRsqhjV8sWbAkjEktPpiTTRuJjRyfNvJc1d6Z/QtTfn2jSL/WGgy3ZvZtyjX7XLk0vD1zPojjj/See7AG/h+P/B+gbSYqeYcI1QQBo7fFuGFiLaVhN22MqSSqkpGt62NnOKqmtFui3GEqKsYe4O2GK9BVubY67fFeIvhUyVj0BYjiYTS1Vn6bXHORyxZGR2uIO21dmD/D9Ix5Ed2sRbPVb4Jcc+UKIJSS7Fa8XdCvQ98QEjNtdDgLZeeJMt384aDUpgP8zmTr30Av8yUFhv2L+Z3gYiQl7g5hPncPfCMBs7OMqclyxtMoqKaJeSD80IFl9u02k2JGSNbrfU7RO/AzSWHpMnFaD/S4IACmFR6y/fiCPoHMAqEm2BUU06LJ5aU1299RNdbzDHzD7k5VaF99NZ16iOrZ37bFSoGAz6iV0sm0WnKZA9Jp/VEZPdkpHcysncy0j8ZGTQh6e6CcLsErYVowhWI41WnDX+U4lmdciimGOsjuMUamv+axQY4n5BwNwV/K1Sjk9/Ni7J+eSYyvW4CTFmqNL0+GwvN0uPVm2Uq5DgjuWrUgSUwfrQyF6Rni1LOaj0ZxbgjjX6/D/WgOf6gLsLI8gng49k+oE5wL9V9IS9wBLwDSb2BJm6D3TH89WOWcc1SnjcyP0M6t6vE4M2Gnt2qeD014IuNWIokL98dUuuRaFjVoM1BnrLNI/BJojFRTG9PMctF8Qt2RnmvWBxnxammlqjE3UjEpBxf1OZmpPYXzXc6/BUHplr0a3/FNK/YF21LNdwdy0+HzdJPVY2na6j2dIUh8E/EUefJnFPqxiVJvfkxcqUyvY9peKcseSpePBgLSa7M5LeY0Gb4+vofAAAA//8=";
    private Plugin plugin;
    private List<ulong> appliedUsers = new();
    public AppearanceMasker(Plugin plugin)
    {
        this.plugin = plugin;
        Service.ClientState.TerritoryChanged += TerritoryChanged;
    }

    public void TerritoryChanged(ushort obj)
    {
        Service.PluginLog.Info("Territory Changed!");
        appliedUsers.Clear();
        try
        {
            applyMasking();

        }
        catch (Exception e)
        {
            Service.PluginLog.Error("aaa", e);
        }
    }

    private void applyMasking()
    {
        if (!Service.ClientState.IsPvPExcludingDen)
        {
            Service.PluginLog.Info("Not PVP Excluding den"); //todo why does this only work when manually loading

            //return;
        }
        foreach (var gameObject in Service.ObjectTable)
        {
            if (gameObject.ObjectKind == ObjectKind.Player)
            {
                if (appliedUsers.Contains(gameObject.GameObjectId))
                {
                    continue;
                }
                
                Service.PluginLog.Info("Applied users list does not contain the game object id");

                if (gameObject.GameObjectId == Service.ClientState.LocalPlayer.GameObjectId)
                {
                    Service.PluginLog.Info("ID is us - ignore");

                    continue;
                }
                IPlayerCharacter pc = (IPlayerCharacter) gameObject;
                Service.PluginLog.Info($"PC is {pc.Name}");

                String job = pc.ClassJob.GameData.Abbreviation.ToString();
                String glamour = Glamours.getGlamour(job);
                if (plugin.Configuration.MaskPlayerAppearance)
                {
                    Service.PluginLog.Info("Applying player appearance");
                    Service.GlamourerManager.ApplyCustomization(gameObject.ObjectIndex, hyur);

                }

                if (plugin.Configuration.MaskPlayerGlamours)
                {
                    Service.PluginLog.Info("Applying outfit change");

                    Service.GlamourerManager.ApplyOutfit(gameObject.ObjectIndex, glamour);

                }
                appliedUsers.Add(gameObject.GameObjectId);

            }
        }
    }
    
    
    
}
