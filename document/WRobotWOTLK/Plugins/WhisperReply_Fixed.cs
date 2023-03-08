using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

public class Main : wManager.Plugin.IPlugin
{
    private bool _isLaunched;
    public void Initialize()
    {
        _isLaunched = true;
        _lastReadMessageId = Chat.Messages.Count - 1;
        UpdateReply();
        Logging.Write("[WhisperReply] Loadded.");
    }

    public void Dispose()
    {
        _isLaunched = false;
        Logging.Write("[WhisperReply] Disposed.");
    }

    public void Settings()
    {
        _settings.ToForm();
        _settings.Save();
    }

    private WhisperReplySettings _settings
    {
        get
        {
            try
            {
                if (WhisperReplySettings.CurrentSetting == null)
                    WhisperReplySettings.Load();
                return WhisperReplySettings.CurrentSetting;
            }
            catch
            {
            }
            return new WhisperReplySettings();
        }
        set
        {
             try
            {
                WhisperReplySettings.CurrentSetting = value;
            }
            catch
            {
            }
        }
    }
    private Dictionary<string, int> _nbReplyByName = new Dictionary<string, int>(); 
    private int _lastReadMessageId;
    private readonly List<ChatTypeId> _chatTypeIdFilter = new List<ChatTypeId> { ChatTypeId.WHISPER, ChatTypeId.BN_WHISPER };
    void UpdateReply()
    {
        while (Products.IsStarted && _isLaunched)
        {
            if (!Products.InPause)
            {
                if (_settings.ListOfAvailableResponse == null || _settings.ListOfAvailableResponse.Count <= 0 ||
                    _settings.ReplyMaxByPlayer <= 0)
                {
                    Logging.WriteError("[WhisperReply] Bad settings for plugin, please fix it.");
                    Dispose();
                    return;
                }

                var msgs = Chat.Messages;
                while (_lastReadMessageId + 1 <= msgs.Count - 1)
                {
                    _lastReadMessageId++;
                    if (_chatTypeIdFilter.Contains(msgs[_lastReadMessageId].Channel))
                    {
                        if (_nbReplyByName.ContainsKey(msgs[_lastReadMessageId].UserName) &&
                            _nbReplyByName[msgs[_lastReadMessageId].UserName] >= _settings.ReplyMaxByPlayer)
                            continue;
                        string reply =
                            _settings.ListOfAvailableResponse[
                                Others.Random(0, _settings.ListOfAvailableResponse.Count - 1)]
                                .ToString();
                        Logging.Write("[WhisperReply] New whisper " + msgs[_lastReadMessageId] + ", WRobot reply: " +
                                      reply);
                        Thread.Sleep(Others.Random(3500, 7500));
                        if (Products.IsStarted && !Products.InPause && _isLaunched)
                        {
                            Chat.SendChatMessageWhisper(reply, msgs[_lastReadMessageId].UserName);
                            if (_nbReplyByName.ContainsKey(msgs[_lastReadMessageId].UserName))
                            {
                                _nbReplyByName[msgs[_lastReadMessageId].UserName]++;
                            }
                            else
                            {
                                _nbReplyByName.Add(msgs[_lastReadMessageId].UserName, 1);
                            }
                        }
                    }
                }
            }
            Thread.Sleep(80);
        }
    }

    [Serializable]
    public class WhisperReplySettings : Settings
    {
        public WhisperReplySettings()
        {
            ReplyMaxByPlayer = 1;
        }

        [Serializable]
        public class StringClass
        {
            public StringClass()
            {
                Text = "";
            }

            public StringClass(string text)
            {
                Text = text;
            }
            public string Text { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public uint ReplyMaxByPlayer { get; set; }
        public List<StringClass> ListOfAvailableResponse { get; set; }

        public static WhisperReplySettings CurrentSetting { get; set; }

        public bool Save()
        {
            try
            {
                return Save(AdviserFilePathAndName("WhisperReply", ObjectManager.Me.Name + "." + Usefuls.RealmName));
            }
            catch (Exception e)
            {
                Logging.WriteError("WhisperReply > Save(): " + e);
                return false;
            }
        }

        public static bool Load()
        {
            try
            {
                if (File.Exists(AdviserFilePathAndName("WhisperReply", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
                {
                    CurrentSetting =
                        Load<WhisperReplySettings>(AdviserFilePathAndName("WhisperReply",
                                                                      ObjectManager.Me.Name + "." + Usefuls.RealmName));
                    return true;
                }
                CurrentSetting = new WhisperReplySettings();
            }
            catch (Exception e)
            {
                Logging.WriteError("WhisperReply > Load(): " + e);
            }
            return false;
        }
    }
}
