﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ImageGallery
{
    using ImageGallery.Properties;
    // Literally copied from XPRenderer.
    using Manina.Windows.Forms;
    public class GalleryRenderer : Manina.Windows.Forms.ImageListViewRenderers.XPRenderer
    {


        public override void DrawGroupHeader(Graphics g, string name, Rectangle bounds)
        {
            // Bottom border
            bounds.Inflate(0, -4);
            using (Pen pSpep = new Pen(Color.FromArgb(128, SystemColors.GrayText)))
            {
                g.DrawLine(pSpep, bounds.Left + 1, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
            }

            // Text
            if (bounds.Width > 4)
            {
                using (StringFormat sf = new StringFormat())
                {
                    sf.FormatFlags = StringFormatFlags.NoWrap;
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                    Brush bText = SystemBrushes.WindowText;
                    var baseFont = ImageListView.GroupHeaderFont == null ? ImageListView.Font : ImageListView.GroupHeaderFont;
                    var split = name.Split(new string[] { @"\\" }, 2, StringSplitOptions.None);
                    if (split.Length == 1)
                    {
                        g.DrawString(name, baseFont, bText, bounds, sf);
                    }
                    else
                    {
                        var boldedFont = new Font(baseFont, FontStyle.Bold);
                        split[1] = "\\\\" + split[1];
                        // Draw watcher
                        g.DrawString(split[0], boldedFont, bText, bounds, sf);

                        var watcherWidth = g.MeasureString(split[0], boldedFont).Width;
                        var fileBounds = new RectangleF(bounds.X + watcherWidth, bounds.Y, bounds.Width, bounds.Height);
                        g.DrawString(split[1], baseFont, bText, fileBounds, sf);
                    }
                }
            }
        }

        public override Size MeasureItem(Manina.Windows.Forms.View view)
        {
            Size itemSize = new Size();

            // Reference text height
            int textHeight = ImageListView.Font.Height*2;

            if (view == Manina.Windows.Forms.View.Details)
                return base.MeasureItem(view);
            else
            {
                // Calculate item size
                Size itemPadding = new Size(4, 4);
                itemSize = ImageListView.ThumbnailSize + itemPadding + itemPadding;
                itemSize.Height += textHeight + System.Math.Max(4, textHeight / 3) + itemPadding.Height; // textHeight / 3 = vertical space between thumbnail and text
                return itemSize;
            }
        }
        /// <summary>
        /// Draws the specified item on the given graphics.
        /// </summary>
        /// <param name="g">The System.Drawing.Graphics to draw on.</param>
        /// <param name="item">The ImageListViewItem to draw.</param>
        /// <param name="state">The current view state of item.</param>
        /// <param name="bounds">The bounding rectangle of item in client coordinates.</param>
        public override void DrawItem(System.Drawing.Graphics g, ImageListViewItem item, ItemState state, System.Drawing.Rectangle bounds)
        {
            // Paint background
            var inner = bounds;
            inner.Inflate(-1, -1);
            System.Drawing.SolidBrush borderColor = new System.Drawing.SolidBrush(System.Drawing.Color.LightGray);

            if (ImageListView.Enabled || !item.Enabled)

            {
                //Draw border
                g.FillRectangle(borderColor, bounds);
                // Fill inside
                if (ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                {
                    g.FillRectangle(SystemBrushes.Highlight, inner);
                }
                else if (!ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                {
                    g.FillRectangle(SystemBrushes.GrayText, inner);
                }
                else
                {
                    g.FillRectangle(SystemBrushes.Window, inner);

                }
            }
            else
                g.FillRectangle(SystemBrushes.Control, bounds);
            if (ImageListView.View != Manina.Windows.Forms.View.Details)
            {
                Size itemPadding = new Size(4, 4);

                // Draw the image
                Image img = item.GetCachedImage(CachedImageType.Thumbnail);
                if (img != null)
                {
                    Rectangle border = new Rectangle(bounds.Location + itemPadding, ImageListView.ThumbnailSize);
                    Rectangle pos = Utility.GetSizedImageBounds(img, border);
                    g.DrawImage(img, pos);


                    if (ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                    {
                        using (Pen pen = new Pen(SystemColors.Highlight, 3))
                        {
                            g.DrawRectangle(pen, border);
                        }
                    }
                    else if (!ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                    {
                        using (Pen pen = new Pen(SystemColors.GrayText, 3))
                        {
                            pen.Alignment = PenAlignment.Center;
                            g.DrawRectangle(pen, border);
                        }
                    }
                    else
                    {
                        using (Pen pGray128 = new Pen(Color.FromArgb(128, SystemColors.GrayText)))
                        {
                            g.DrawRectangle(pGray128, border);
                        }
                    }
                }

                // Draw item text
                
                SizeF szt = TextRenderer.MeasureText(g, item.Text, ImageListView.Font);
                RectangleF rt;
                using (StringFormat sf = new StringFormat())
                {
                    rt = new RectangleF(
                        bounds.Left + itemPadding.Width,
                        bounds.Top + 3 * itemPadding.Height + ImageListView.ThumbnailSize.Height,
                        ImageListView.ThumbnailSize.Width,
                        szt.Width <= ImageListView.ThumbnailSize.Width ? szt.Height : szt.Height*2
                    );
                    sf.Alignment = StringAlignment.Center;
                    sf.FormatFlags = StringFormatFlags.LineLimit;
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                    rt.Width += 1;
                    rt.Inflate(1, 2);
                    if (ImageListView.Focused && ((state & ItemState.Focused) != ItemState.None))
                        rt.Inflate(-1, -1);
                    // Highlight text rectangle on selection
                    /*
                    if (ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                    {
                        g.FillRectangle(SystemBrushes.Highlight, rt);
                    }
                    else if (!ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                    {
                        g.FillRectangle(SystemBrushes.GrayText, rt);
                    }
                    */
                    if ((state & ItemState.Disabled) != ItemState.None)
                    {
                        g.DrawString(item.Text, ImageListView.Font, SystemBrushes.GrayText, rt, sf);
                    }
                    else if (((state & ItemState.Selected) != ItemState.None))
                    {
                        g.DrawString(item.Text, ImageListView.Font, SystemBrushes.HighlightText, rt, sf);
                    }
                    else
                    {
                        g.DrawString(item.Text, ImageListView.Font, SystemBrushes.WindowText, rt, sf);
                    }
                }
                // focus text on focused item
                
                /*
                if (ImageListView.Focused && ((state & ItemState.Focused) != ItemState.None))
                {
                    Rectangle fRect = Rectangle.Round(rt);
                    fRect.Inflate(1, 1);
                    ControlPaint.DrawFocusRectangle(g, fRect);
                }
                */
                
            }
            else // if (ImageListView.View == Manina.Windows.Forms.View.Details)
            {
                if (ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                {
                    g.FillRectangle(SystemBrushes.Highlight, bounds);
                }
                else if (!ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                {
                    g.FillRectangle(SystemBrushes.GrayText, bounds);
                }

                Size offset = new Size(2, (bounds.Height - ImageListView.Font.Height) / 2);
                using (StringFormat sf = new StringFormat())
                {
                    sf.FormatFlags = StringFormatFlags.NoWrap;
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                    // Sub text
                    List<ImageListView.ImageListViewColumnHeader> uicolumns = ImageListView.Columns.GetDisplayedColumns();
                    RectangleF rt = new RectangleF(bounds.Left + offset.Width, bounds.Top + offset.Height, uicolumns[0].Width - 2 * offset.Width, bounds.Height - 2 * offset.Height);
                    foreach (ImageListView.ImageListViewColumnHeader column in uicolumns)
                    {
                        rt.Width = column.Width - 2 * offset.Width;
                        int iconOffset = 0;
                        if (column.Type == ColumnType.Name)
                        {
                            // Allocate space for checkbox and file icon
                            if (ImageListView.ShowCheckBoxes && ImageListView.ShowFileIcons)
                                iconOffset += 2 * 16 + 3 * 2;
                            else if (ImageListView.ShowCheckBoxes)
                                iconOffset += 16 + 2 * 2;
                            else if (ImageListView.ShowFileIcons)
                                iconOffset += 16 + 2 * 2;
                        }
                        rt.X += iconOffset;
                        rt.Width -= iconOffset;

                        Brush forecolor = SystemBrushes.WindowText;
                        if ((state & ItemState.Disabled) != ItemState.None)
                            forecolor = SystemBrushes.GrayText;
                        else if ((state & ItemState.Selected) != ItemState.None)
                            forecolor = SystemBrushes.HighlightText;

                        if (column.Type == ColumnType.Rating && ImageListView.RatingImage != null && ImageListView.EmptyRatingImage != null)
                        {
                            int w = ImageListView.RatingImage.Width;
                            int y = (int)(rt.Top + (rt.Height - ImageListView.RatingImage.Height) / 2.0f);
                            int rating = item.StarRating;
                            if (rating < 0) rating = 0;
                            if (rating > 5) rating = 5;
                            for (int i = 1; i <= rating; i++)
                                g.DrawImage(ImageListView.RatingImage, rt.Left + (i - 1) * w, y);
                            for (int i = rating + 1; i <= 5; i++)
                                g.DrawImage(ImageListView.EmptyRatingImage, rt.Left + (i - 1) * w, y);
                        }
                        else
                            g.DrawString(item.SubItems[column].Text, ImageListView.Font, forecolor, rt, sf);

                        rt.X -= iconOffset;
                        rt.X += column.Width;
                    }
                }

                if (ImageListView.Focused && ((state & ItemState.Focused) != ItemState.None))
                    ControlPaint.DrawFocusRectangle(g, bounds);
            }
        }
        
    }
}
