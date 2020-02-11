using ImageGallery.Database;
using NHotkey;
using NHotkey.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageGallery
{
    public class WatcherKeyManager
    {
        Dictionary<int, (Keys ShortcutKeys, bool GlobalShortcut)> _watcherKeyMap;
        Dictionary<Keys, (List<int> watcherIds, HashSet<int> globalIds)> _keyWatcherMap;
        Guid _slugPrefix;
        MainForm _mainForm;
        object _updateLock = new object();
        bool _disabled;
        public WatcherKeyManager(MainForm mainForm)
        {
            _mainForm = mainForm;
            _slugPrefix = Guid.NewGuid();
            _watcherKeyMap = new Dictionary<int, (Keys ShortcutKeys, bool GlobalShortcut)>();
            _keyWatcherMap = new Dictionary<Keys, (List<int> watcherIds, HashSet<int> globalIds)>();
            UpdateData();
        }

        private static string keySlug(Keys key, Guid prefix)
        {
#if DEBUG
            return $"{prefix.ToString()},Key={(int)key},Debug=True";
#else
            return $"{prefix.ToString()},Key={(int)key},Debug=False";
#endif
        }

        private static Keys NameFromSlug(string slug)
        {
            var keyPart = slug.Split(new char[] { ',' })[1];
            var keyString = keyPart.Replace("Key=", "");
            return (Keys)Int32.Parse(keyString);
        }

        public void DisableKeys()
        {
            lock (_updateLock)
            {
                if (_disabled)
                {
                    return;
                }
                _disabled = true;
                RemoveAllGlobalKeys();
            }
        }

        private void RemoveAllGlobalKeys()
        {
            foreach (var kv in _keyWatcherMap)
            {
                if (kv.Value.globalIds.Any())
                {
                    HotkeyManager.Current.Remove(keySlug(kv.Key, _slugPrefix));
                }
            }
        }

        public void UpdateData()
        {
            lock (_updateLock)
            {
                
                if (!_disabled)
                {
                    RemoveAllGlobalKeys();
                }

                _slugPrefix = Guid.NewGuid();
                _watcherKeyMap = new Dictionary<int, (Keys ShortcutKeys, bool GlobalShortcut)>();
                _keyWatcherMap = new Dictionary<Keys, (List<int> watcherIds, HashSet<int> globalIds)>();
                using (var ctx = new FilesContext())
                {
                    _watcherKeyMap = ctx.GetWatcherShortcutMap();
                }
                foreach (var kv in _watcherKeyMap)
                {
                    var watcherId = kv.Key;
                    var shortcutKeys = kv.Value.ShortcutKeys;
                    var globalShortcut = kv.Value.GlobalShortcut;
                    if (shortcutKeys == Keys.None)
                    {
                        continue;
                    }

                    if (!_keyWatcherMap.ContainsKey(shortcutKeys))
                    {
                        _keyWatcherMap[shortcutKeys] = (new List<int>(), new HashSet<int>());
                    }
                    _keyWatcherMap[shortcutKeys].watcherIds.Add(watcherId);
                    if (globalShortcut)
                    {
                        _keyWatcherMap[shortcutKeys].globalIds.Add(watcherId);
                    }
                }
                foreach (var kv in _keyWatcherMap)
                {
                    var shortcutKeys = kv.Key;
                    if (kv.Value.globalIds.Any())
                    {
                        HotkeyManager.Current.AddOrReplace(
                            keySlug(shortcutKeys, _slugPrefix),
                            shortcutKeys,
                            true,
                            GlobalHotkeyPressed
                        );
                    }
                }
                _disabled = false;
            }

        }

        private void GlobalHotkeyPressed(object sender, HotkeyEventArgs e)
        {
            if (_disabled)
            {
                return;
            }
            lock (_updateLock)
            {
                var keys = NameFromSlug(e.Name);
                (List<int> watcherIds, HashSet<int> globalIds) watcherIdPair;
                _keyWatcherMap.TryGetValue(keys, out watcherIdPair);
                if (watcherIdPair == default)
                {
                    return;
                }

                if (watcherIdPair.globalIds.Any())
                {
                    _mainForm.SetActiveIdsFromSet(watcherIdPair.globalIds, true);
                }
                e.Handled = true;
            }

        }

        internal bool HandleKey(KeyEventArgs e)
        {
            if (!_keyWatcherMap.ContainsKey(e.KeyData))
            {
                return false;
            }
            (List<int> watcherIds, HashSet<int> globalIds) watcherIdPair = _keyWatcherMap[e.KeyData];
            if (watcherIdPair.watcherIds.Any())
            {
                _mainForm.SetActiveIdsFromSet(watcherIdPair.watcherIds.ToHashSet(), true);
            }
            e.Handled = true;
            e.SuppressKeyPress = true;
            return true;
        }
    }
}
