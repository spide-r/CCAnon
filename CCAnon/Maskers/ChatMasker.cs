using System;
using System.Globalization;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;

namespace CCAnon.Maskers;

public class ChatMasker
{
    private Plugin plugin;
    public ChatMasker(Plugin plugin)
    {
        this.plugin = plugin;
        Service.ChatGui.ChatMessage += onMessage;
    }
    
    

    private void onMessage(XivChatType type, int timestamp, ref SeString sender, ref SeString message, ref bool ishandled)
    {
        if (!plugin.Configuration.MaskChat)
        {
            return;
        }
        try
        {
            if (Service.isInCC() && type == XivChatType.Party)
            {
                //Service.PluginLog.Info($"New Party Chat message! Sender: {sender}, Message: {message.TextValue}");
                var job = "";
                foreach (var payload in sender.Payloads)
                {
                    //Service.PluginLog.Info($"Payload: {payload.Type} | {payload}");
                    if (payload.Type == PayloadType.Player)
                    {
                        ishandled = true;
                    }

                    int unusedValue;
                    var isNumber = int.TryParse(payload.ToString(), out unusedValue); //hacky

                    if (payload.Type == PayloadType.Icon && !payload.ToString()!.Contains("CrossWorld") && !isNumber)
                    {
                        var raw = payload.ToString();
                        if (raw != null) job = raw.Substring(7);
                    }
                }

                if (ishandled)
                {
                    var chat = new XivChatEntry();
                    chat.Message = message;
                    chat.Name = job;
                    chat.Type = XivChatType.Party;
                    Service.ChatGui.Print(chat);
                }
            }
        }
        catch (Exception e)
        {
            Service.PluginLog.Error(e, "Issue in chat masker!");
        }
    }
}
