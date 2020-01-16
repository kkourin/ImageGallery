
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ImageGallery
{
    // Literally copied from XPRenderer.
    using Manina.Windows.Forms;
    public class GalleryRenderer : Manina.Windows.Forms.ImageListViewRenderers.XPRenderer
    {
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
            if (ImageListView.Enabled || !item.Enabled)
                g.FillRectangle(SystemBrushes.Window, bounds);
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

                    // Draw image border
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
                    //sf.FormatFlags = StringFormatFlags.NoWrap;
                    //sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                    rt.Width += 1;
                    rt.Inflate(1, 2);
                    if (ImageListView.Focused && ((state & ItemState.Focused) != ItemState.None))
                        rt.Inflate(-1, -1);
                    if (ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                    {
                        g.FillRectangle(SystemBrushes.Highlight, rt);
                    }
                    else if (!ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                    {
                        g.FillRectangle(SystemBrushes.GrayText, rt);
                    }
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
                

                if (ImageListView.Focused && ((state & ItemState.Focused) != ItemState.None))
                {
                    Rectangle fRect = Rectangle.Round(rt);
                    fRect.Inflate(1, 1);
                    ControlPaint.DrawFocusRectangle(g, fRect);
                }
                
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
