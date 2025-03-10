using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
    /// <summary>
    /// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
    /// </summary>
    public class LineShape : Shape
    {
        #region Constructor

        public LineShape(RectangleF rect) : base(rect)
        {
            StrokeColor = Color.Black;  
            StrokeWidth = 2;
        }

        public LineShape(LineShape rectangle) : base(rectangle)
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

            PointF start = new PointF(Rectangle.Left, Rectangle.Top);
            PointF end = new PointF(Rectangle.Right, Rectangle.Top);

            using (Pen pen = new Pen(StrokeColor, StrokeWidth))
            {
                grfx.Transform = TransformationMatrix;

                if (UseGradient)
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(
                        start, end, GradientStartColor, GradientEndColor))
                    {
                        pen.Brush = brush;
                        grfx.DrawLine(pen, start, end);
                    }
                }
                else
                {
                    grfx.DrawLine(pen, start, end);
                }
            }
            grfx.Restore(state);

        }
    }
}
