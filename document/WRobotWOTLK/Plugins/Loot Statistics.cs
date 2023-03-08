using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Plugin;
using wManager.Wow.Helpers;

public class Main : IPlugin
{
    private bool isLaunched;
    private int _startTime;
    private Dictionary<int, int> _itemsLast = new Dictionary<int, int>();
    private Dictionary<int, int> _itemsStat = new Dictionary<int, int>();
    public void Initialize()
    {
        isLaunched = true;
        _startTime = Others.TimesSec;
        Logging.Write("[Loot Stat] Loadded.");
        while (isLaunched && Products.IsStarted)
        {
            try
            {
                if (Conditions.ProductIsStartedNotInPause)
                {
                    var items = new Dictionary<int, int>();
                    var itemsNew = new Dictionary<int, int>();
                    var itemsObj = Bag.GetBagItem();

                    // Extract current bag items
                    foreach (var i in itemsObj)
                    {
                        if (!i.IsValid)
                            continue;
                        if (!items.ContainsKey(i.Entry))
                            items.Add(i.Entry, 0);
                        items[i.Entry] += i.StackCount;
                    }

                    if (items.Count > 0 && _itemsLast.Count > 0)
                    {
                        // compare with last bag:
                        foreach (var i in items)
                        {
                            if (!_itemsLast.ContainsKey(i.Key))
                            {
                                itemsNew.Add(i.Key, i.Value);
                            }
                            else
                            {
                                var diff = items[i.Key] - _itemsLast[i.Key];
                                if (diff > 0)
                                    itemsNew.Add(i.Key, diff);
                            }
                        }

                        // write result:
                        if (itemsNew.Count > 0)
                        {
                            var itemsString = string.Empty;
                            foreach (var i in itemsNew)
                                if (i.Key > 0 && i.Value > 0)
                                    itemsString += ItemsManager.GetNameById(i.Key) + " x " + i.Value + ", ";
                            Logging.Write("[Loot Stat] Looted items: " + itemsString.Trim(' ', ',') + ".");

                            // Put new items in _itemsStat for end session stat
                            foreach (var i in itemsNew)
                            {
                                if (!_itemsStat.ContainsKey(i.Key))
                                    _itemsStat.Add(i.Key, 0);
                                _itemsStat[i.Key] += i.Value;
                            }
                        }
                    }

                    // Put current bag items in last bag for next check
                    _itemsLast = items;
                }
            }
            catch { }

            Thread.Sleep(1000 * 3); // Wait 3 sec
        }
    }

    public int ByHour(int totalCount)
    {
        try
        {
            return totalCount * (60 * 60) / (Others.TimesSec - _startTime);
        }
        catch { }
        return 0;
    }

    public void Dispose()
    {
        isLaunched = false;

        try
        {
            // Write session result
            if (_itemsStat.Count > 0)
            {
                var itemsString = Environment.NewLine;
                foreach (var i in _itemsStat)
                    itemsString += ItemsManager.GetNameById(i.Key) + " x " + i.Value + " (" + ByHour(i.Value) + "/hr)" + Environment.NewLine;
                Logging.Write("[Loot Stat] Items looted during session: " + itemsString + ".");
            }
        }
        catch { }

        Logging.Write("[Loot Stat] Disposed.");
    }

    public void Settings()
    {
        MessageBox.Show("[Loot Stat] No settings for this plugin.");
    }
}
