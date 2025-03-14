﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
    /// <summary>
    /// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
    /// </summary>
    public class TriangleShape : Shape

    {
        public PointF[] Points =new PointF[3];
        #region Constructor

        public TriangleShape(RectangleF rect) : base(rect)
        {
            Points[0] = new PointF(
                   Location.X + Width / 2,
                   Location.Y);

            Points[1]=new PointF(
                Location.X + Width,
                Location.Y + Height);

            Points[2]=new PointF(
                Location.X,
                Location.Y + Height);
        }

        public TriangleShape(TriangleShape rectangle) : base(rectangle)
        {
            Points[0] = new PointF(
                  Location.X + Width / 2,
                  Location.Y);

            Points[1] = new PointF(
                Location.X + Width,
                Location.Y + Height);

            Points[2] = new PointF(
                Location.X,
                Location.Y + Height);
        }

        #endregion

        /// <summary>
        /// Проверка за принадлежност на точка point към правоъгълника.
        /// В случая на правоъгълник този метод може да не бъде пренаписван, защото
        /// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
        /// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
        /// елемента в този случай).
        /// </summary>
        /// 

        public override bool Contains(PointF p)
        {
            Points[0] = new PointF(
                  Location.X + Width / 2,
                  Location.Y);

            Points[1] = new PointF(
                Location.X + Width,
                Location.Y + Height);

            Points[2] = new PointF(
                Location.X,
                Location.Y + Height);
            if (base.Contains(p))
            {

                int intersectCount = 0;
                for (int i = 0; i < Points.Length; i++)
                {
                    int next = (i + 1) % Points.Length;
                    if (((Points[i].Y <= p.Y && p.Y < Points[next].Y) ||
                        (Points[next].Y <= p.Y && p.Y < Points[i].Y)) &&
                            (p.X < (Points[next].X - Points[i].X) *
                            (p.Y - Points[i].Y) / (Points[next].Y - Points[i].Y) + Points[i].X))
                    {
                        intersectCount++;
                    }
                }
                return intersectCount % 2 == 1;
            }
            return false;
        }

        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            var state = grfx.Save();

            PointF top = new PointF(Rectangle.X + Rectangle.Width / 2, Rectangle.Y);
            PointF left = new PointF(Rectangle.X, Rectangle.Y + Rectangle.Height);
            PointF right = new PointF(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height);

            PointF[] points = { top, left, right };

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPolygon(points);
                grfx.Transform = TransformationMatrix;

                Color fillColorWithOpacity = Color.FromArgb(FillOpacity, FillColor);

                if (UseGradient)
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(
                        new PointF(Rectangle.Left, Rectangle.Top),
                        new PointF(Rectangle.Left, Rectangle.Bottom),
                        Color.FromArgb(FillOpacity, GradientStartColor),
                        Color.FromArgb(FillOpacity, GradientEndColor)))
                    {
                        grfx.FillPath(brush, path);
                    }
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(fillColorWithOpacity))
                    {
                        grfx.FillPath(brush, path);
                    }
                }

                using (Pen pen = new Pen(StrokeColor, StrokeWidth))
                {
                    grfx.DrawPath(pen, path);
                }
            }
            grfx.Restore(state);

        }
    }
}
