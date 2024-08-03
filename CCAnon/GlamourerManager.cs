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

    private String smol_state =
        "Bh+LCAAAAAAAAAq8WF1T6zYQ/S9+LWFkSf7KGxcI0AmUkhQeOn0QyTpoUOzUUu6dlOG/d2UHR7YTl2lJ8yRLZ8/uatfHUt68kVTwCIWWeeYN/RPvZg6ZkamEwht6gtMwijgMUvpMB5wBGQgehwPGWQQBg1mSzr0T77wAYZDgQhhAK0ooH5B4QNjUD4cBH/rsNOZ+GEfRT4QMCUGTsdDmci7NYXjkc1bD78TSMutlrvDpAvSskCvrEidxYpQXM5g/wLwQP7xhKpQGjCpXeVGt/7qWs1e0kgs0MMUaV6diob3h73+ceE+FNHBf5AZmBua1+eWfa7la4mZ4wzfvVsjsWmS4+ubdGFje4MgPaEjK5LWprc5WK7X58FE+TAzaNmaaFtt1sh1RHL6feL+kadsfpwlPwjDx/f/gtJrZ7/MaRDPBKODsSL6+5fON64vGLAq/yhf1W4nhTmrXG2NJlHxZ6druxmBbyykcwdIdo1Goj95GAKbZJoSHX5db4LhL0N2lKJpbmQQkONZbcAezV9dZQhN6LF8oA9o0UovDhB3L28NIZgursY3kjraR4//X3bUoW3Lykv9o2FYPCHiUOq+i0dN8sVCO7jaBTyBWVuUPk5Vikq3Lyl0poTWUw3LO5kk68PO1NvlS/gWlsudzUFvcg5iVc49CrXHAassyNLS8gmxe7eIW0iKvdG26Wbk0fhtzDXLxYlwE6fCcK5E5iKC9PjoU6ocPIQttNsoFhR0QxqFsLNoNhsZt3ORVZtuvqQPrxHy5gRL10E4vCPaF12aMyOH4Olh/z4ZIoUZ4EFkX4PeVqIHEhv0kkn0ayT+NDD6NDD+NjPqQ+GkSs81UGJPnfbgK0ak52Vfz5yL/oft68aMvxpD+Q1sgcvIiGm9Ph22yFErdoFj3xX+Xa+hb/9meEw8v3+Zr89IXxFiutMEzZf97g6j2FgZJx9dazxTconC5r2rUrYhU7b3hHfVBZZtsha2nWeAepdr0EdWgB/iOlwMrz32J1vBOx8Qd/09gMmgk2xT+SmsReC8KPPUbdG+xtYvfvt+ulZErVV5RapKBf0o6eu0Y4YFaVwelj9D24KtaTPOs3MF7wFtFZsTiANr281gun4W6yQxkWppN24zsMSvl8V/YWRW+kGm6rlobbzsWl5AkJgy76qoAsF/i0zghfhKiQH6rGuA0DiyGdwitCHcJGQnimLLQYaRh+Yt3lNQPwjh0GsHlrGXbZaUBTwh1SP2QRDFndEdKLARD37vRKA0OHaJKa4ePJWVQ0Y6Pl4kEbH8FvpJwK711929JOY243XuHlCTc96Mw2JHiE4/InqTlqlscuo3T3Ue8FiKlEyb9aIAztXoRdqbLfgEzoTrx2poy5rKTOA5Cn2N/7dhZRAOKdavpKfPt3E7O63f4Fv8RKPDbZF/hUlftPcxeuscye63OaZDmBRLbybMU0Xb4/v43AAAA//8=";
    private ApiVersion ApiVersionSubscriber;
    private SetItem SetItemSubscriber;
    private RevertState RevertStateSubscriber;
    private RevertToAutomation RevertToAutomationSubscriber;
    private ApplyDesign ApplyDesign;
    private ApplyState ApplyState;

    public GlamourerManager(IDalamudPluginInterface pluginInterface)
    {

        ApiVersionSubscriber = new ApiVersion(pluginInterface);
        SetItemSubscriber = new SetItem(pluginInterface);
        RevertStateSubscriber = new RevertState(pluginInterface);
        RevertToAutomationSubscriber = new RevertToAutomation(pluginInterface);
        ApplyDesign = new ApplyDesign(pluginInterface);
        ApplyState = new ApplyState(pluginInterface);

    }

    public void applyStuff(ICharacter? character)
    {
        ApplyState.Invoke(smol_state, character.ObjectIndex);
    }
    
    public void RevertCharacter(ICharacter? character)
    {
        if (character == null) return;
        try
        {
            RevertStateSubscriber.Invoke(character.ObjectIndex);
        }
        catch (Exception e)
        {
            Service.PluginLog.Error(e, "Failed to contact RevertCharacter");
        }
    }

    public bool RevertToAutomationCharacter(ICharacter? character)
    {
        if (character == null) return false;
        try
        {
            return RevertToAutomationSubscriber.Invoke(character.ObjectIndex) == GlamourerApiEc.Success;
        }
        catch (Exception e)
        {
            Service.PluginLog.Error(e, "Failed to contact RevertToAutomation");
            return false;
        }
    }

    
    public void Dispose() { }
}
		
