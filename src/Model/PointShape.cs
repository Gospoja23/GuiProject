using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
    /// <summary>
    /// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
    /// </summary>
    public class PointShape : Shape
    {
        #region Constructor

        public PointShape(RectangleF rect) : base(rect)
        {
        }

        public PointShape(PointShape rectangle) : base(rectangle)
        {
        }

        #endregion

        /// <summary>
        /// Проверка за принадлежност на точка point към правоъгълника.
        /// В случая на правоъгълник този метод може да не бъде пренаписван, защото
        /// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
        /// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
        /// елемента в този случай).
        /// </summary>
        public override bool Contains(PointF point)
        {
            if (base.Contains(point))
                // Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
                // В случая на правоъгълник - директно връщаме true
                return true;
            else
                // Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
                return false;
        }

        /// <summary>
        /// Частта, визуализираща конкретния примитив.
        /// </summary>
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);
            var state = grfx.Save();

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(Rectangle);
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
