using ImageGallery.Database;
using ImageGallery.Database.Models;
using ImageGallery.XMPLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageGallery
{
    public partial class MultiXMPEditForm : Form
    {
        private List<File> _files;
        private List<Watcher> _watchers;
        private SearchTagEditor _searchTagEditor;
        private WatcherMonitor _watcherMonitor;

        public MultiXMPEditForm(List<File> files, SearchTagEditor searchTagEditor, WatcherMonitor watcherMonitor)
        {
            InitializeComponent();
            _files = files;
            _searchTagEditor = searchTagEditor;
            _watcherMonitor = watcherMonitor;
            using (var ctx = new FilesContext())
            {
                _watchers = ctx.Watchers.ToList();
            }
            RefreshView();
        }

        private string[] EnumerableToSortedArray(IEnumerable<string> items)
        {
            return items.ToList().OrderBy(f => f).ToArray();
        }
        private void RefreshView()
        {
            selectedListBox.BeginUpdate();
            selectedListBox.Items.Clear();
            foreach (var file in _files)
            {
                selectedListBox.Items.Add(file.Name);
            }
            selectedListBox.EndUpdate();

            watcherListBox.BeginUpdate();
            watcherListBox.Items.Clear();
            foreach(var watcher in _watchers)
            {
                watcherListBox.Items.Add(watcher.Name);
            }
            watcherListBox.EndUpdate();

            inSelectedListBox.BeginUpdate();
            inSelectedListBox.Items.Clear();
            inSelectedListBox.Items.AddRange(EnumerableToSortedArray(getAllUsedTags().ToArray()));
            inSelectedListBox.EndUpdate();

            inAllSelectedListBox.BeginUpdate();
            inAllSelectedListBox.Items.Clear();
            inAllSelectedListBox.Items.AddRange(EnumerableToSortedArray(getAllCommonTags().ToArray()));
            inAllSelectedListBox.EndUpdate();

            if (selectedListBox.Items.Count > 0)
            {
                selectedListBox.SelectedIndex = 0;
            }

        }

        private HashSet<string> getAllCommonTags()
        {
            // Avoid empty intersection.
            if (_files.Count == 0)
            {
                return new HashSet<string>();
            }

            var commonTags = new HashSet<string>();
            bool seenOne = false;
            foreach (var file in _files)
            {
                // Encounter first set.
                if (!seenOne)
                {
                    seenOne = true;
                    if (file.XMPTags_fts != null)
                    {
                        commonTags.UnionWith(file.XMPTags_fts);
                    }
                }
                else
                {
                    if (file.XMPTags_fts != null)
                    {
                        commonTags.IntersectWith(file.XMPTags_fts);
                    } else
                    {
                        commonTags.Clear();
                    }
                }
            }
            return commonTags;
        }

        private HashSet<string> getAllUsedTags()
        {
            var usedTags = new HashSet<string>();
            foreach (var file in _files)
            {
                if (file.XMPTags_fts == null)
                {
                    continue;
                }
                usedTags.UnionWith(file.XMPTags_fts);
            }
            return usedTags;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void selectedItemsLabel_Click(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MultiXMPEditForm_Load(object sender, EventArgs e)
        {

        }

        private void selectedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = selectedListBox.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            var file = _files[index];
            var filePath = file.FullName;
            selectedItemTagsLabel.Text = getTagTextBox(file.XMPTags_fts);
            try
            {
                var info = new System.IO.FileInfo(filePath);
                if (!info.Exists || info.Length > 10 * 1024 * 1024)
                {
                    return;
                }


                previewPictureBox.Image = Helpers.LoadImage(new System.IO.FileInfo(filePath), 10 * 1024 * 1024);
                
            } catch (ArgumentException)
            {
                previewPictureBox.Image = null;
            }
        }

        private static string getTagTextBox(IEnumerable<string> tagSet)
        {
            if (tagSet == null)
            {
                return "XMP tags for previewed item:\nItem has no tags.";
            }
            var tagList = tagSet.ToList();
            if (tagList.Count == 0)
            {
                return "XMP tags for previewed item:\nItem has no tags.";
            }
            tagList.Sort();
            return $"XMP tags for previewed item:\n{String.Join(", ", tagList.ToArray())}";
        }

        private void watcherListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = watcherListBox.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            HashSet<string> tagSet = new HashSet<string>();
            using(var ctx = new FilesContext())
            {
                Watcher found = ctx.Watchers.Include(watcher => watcher.Files).Where(watcher => watcher.Id == _watchers[index].Id).FirstOrDefault();
                if (found == null)
                {
                    return;
                }
                foreach (var fileTagSet in found.Files.Select(t => t.XMPTags_fts).Where(set => set != null))
                {
                    tagSet.UnionWith(fileTagSet);
                }
            }
            tagsWatcherListBox.BeginUpdate();
            tagsWatcherListBox.Items.Clear();
            tagsWatcherListBox.Items.AddRange(EnumerableToSortedArray(tagSet));
            tagsWatcherListBox.EndUpdate();
        }

        private void AddSelectedItemsToOpenEditTab(ListBox listBox)
        {
            var strings = new List<String>();
            foreach (var item in listBox.SelectedItems)
            {
                strings.Add(item as string);
            }
            TextBox activeBox = editTabControl.SelectedTab == removeTabPage ? removeTagsTextBox : addTagsTextBox;
            if (activeBox.Text.Any()) {
                activeBox.Text += " ";
            }
            activeBox.Text += String.Join(" ", strings.ToArray());
        }
        private void addWatcherTagButton_Click(object sender, EventArgs e)
        {
            AddSelectedItemsToOpenEditTab(tagsWatcherListBox);
        }

        private void inAllSelectedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addAllSelectedTagButton_Click(object sender, EventArgs e)
        {
            AddSelectedItemsToOpenEditTab(inAllSelectedListBox);
        }

        private void addInSelectedTagButton_Click(object sender, EventArgs e)
        {
            AddSelectedItemsToOpenEditTab(inSelectedListBox);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var tagsText = addTagsTextBox.Text;
            if (tagsText.Contains('|') || tagsText.Contains('"'))
            {
                MessageBox.Show(
                    "Cannot use character '|' or '\"' in tags.",
                    "Invalid tags",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
                return;
            }
            var newTags = Helpers.TagStringToHash(tagsText);
            if (newTags.Count == 0)
            {
                return;
            }
            int count = 0;
            var succeeded = new List<File>();
            bool finished = false ;
            using (var ctx = new FilesContext())
            {
                foreach (var file in _files)
                {
                    if (!System.IO.File.Exists(file.FullName))
                    {
                        continue;
                    }
                    var currentTags = file.XMPTags_fts;
                    if (currentTags == null)
                    {
                        file.XMPTags_fts = newTags;
                    }
                    else
                    {
                        currentTags.UnionWith(newTags);
                    }
                }
                succeeded = ctx.UpdateXMPFilesTags(_files);
            }

            var watcherIds = succeeded.Select(file => file.WatcherId).ToHashSet();
            try
            {
                foreach(var id in watcherIds)
                {
                    _watcherMonitor.DisableFSOperations(id);
                }

                int currentFile = 1;
                foreach (var file in succeeded)
                {
                    addStatusLabel.Text = $"Status: ({currentFile}/{succeeded.Count}) Adding tag to {file.Name}...";
                    addStatusLabel.Update();
                    ++currentFile;
                    bool successfullySetTags = _searchTagEditor.setSearchTag(file.FullName, file.XMPTags_fts);
                    if (successfullySetTags)
                    {
                        count++;
                    } else
                    {
                        Console.WriteLine($"Failed: {file.FullName}");
                    }
                }
                removeStatusLabel.Text = $"Status: Syncing...";
                removeStatusLabel.Update();
                foreach (var id in watcherIds)
                {
                    _watcherMonitor.SyncById(id, false);
                }
                finished = true;
            }
            finally
            {
                foreach (var id in watcherIds)
                {
                    _watcherMonitor.ReenableFSOperations(id);
                }
                if (finished)
                {
                    addStatusLabel.Text = $"Status: Done. Successfully added tags to {count} of {_files.Count} file(s).";
                }
                else
                {
                    addStatusLabel.Text = $"Status: Failed.";

                }
                addStatusLabel.Update();
            }
            RefreshView();


        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            var tagsText = removeTagsTextBox.Text;
            var tagsToRemove = Helpers.TagStringToHash(tagsText);
            int count = 0;
            var succeeded = new List<File>();
            bool finished = false;
            using (var ctx = new FilesContext())
            {
                foreach (var file in _files)
                {
                    if (!System.IO.File.Exists(file.FullName))
                    {
                        continue;
                    }
                    var currentTags = file.XMPTags_fts;
                    if (currentTags == null)
                    {
                        continue;
                    }
                    else
                    {
                        currentTags.ExceptWith(tagsToRemove);
                    }
                }
                succeeded = ctx.UpdateXMPFilesTags(_files);
            }
            var watcherIds = succeeded.Select(file => file.WatcherId).ToHashSet();
            try
            {
                foreach (var id in watcherIds)
                {
                    _watcherMonitor.DisableFSOperations(id);
                }

                int currentFile = 1;
                foreach (var file in succeeded)
                {
                    removeStatusLabel.Text = $"Status: ({currentFile}/{succeeded.Count}) Removing tag from {file.Name}...";
                    removeStatusLabel.Update();
                    ++currentFile;
                    if (file.XMPTags_fts == null)
                    {
                        continue;
                    }
                    
                    bool successfullySetTags = _searchTagEditor.setSearchTag(file.FullName, file.XMPTags_fts);
                    if (successfullySetTags)
                    {
                        ++count;
                    }
                }
                removeStatusLabel.Text = $"Status: Syncing...";
                removeStatusLabel.Update();
                foreach (var id in watcherIds)
                {
                    _watcherMonitor.SyncById(id, false);
                }
                finished = true;
            } finally
            {
                foreach (var id in watcherIds)
                {
                    _watcherMonitor.ReenableFSOperations(id);
                }
                if (finished)
                {
                    removeStatusLabel.Text = $"Status: Done. Successfully removed tags from {count} file(s).";
                }
                else
                {
                    removeStatusLabel.Text = $"Status: Failed.";
                }
                removeStatusLabel.Update();
            }
            RefreshView();

        }
    }
}
