using System;
using System.Drawing;

namespace Draw
{
    /// <summary>
    /// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
    /// </summary>
    public class StarShape : Shape
    {
        #region Constructor

        public StarShape(RectangleF rect) : base(rect)
        {
        }

        public StarShape(StarShape rectangle) : base(rectangle)
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

            PointF[] starPoints = new PointF[10];
            double angle = -Math.PI / 2;
            double step = Math.PI / 5;
            float centerX = Location.X + Width / 2;
            float centerY = Location.Y + Height / 2;
            float outerRadius = Width / 2;
            float innerRadius = outerRadius / 2.5f;

            for (int i = 0; i < 10; i++)
            {
                float radius = (i % 2 == 0) ? outerRadius : innerRadius;
                starPoints[i] = new PointF(
                    centerX + (float)(radius * Math.Cos(angle)),
                    centerY + (float)(radius * Math.Sin(angle))
                );
                angle += step;
            }

            grfx.FillPolygon(new SolidBrush(FillColor), starPoints);
            grfx.DrawPolygon(Pens.Black, starPoints);

            for (int i = 0; i < 5; i++)
            {
                grfx.DrawLine(Pens.Black, starPoints[i * 2], new PointF(centerX, centerY));
            }

            for (int i = 0; i < 5; i++)
            {
                grfx.DrawLine(Pens.Black, starPoints[i * 2], starPoints[(i * 2 + 5) % 10]);
            }
            grfx.Restore(state);

        }


    }
}
