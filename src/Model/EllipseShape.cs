﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
	/// </summary>
	public class EllipseShape : Shape
	{
		#region Constructor
		
		public EllipseShape(RectangleF rect) : base(rect)
		{
		}
		
		public EllipseShape(EllipseShape rectangle) : base(rectangle)
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
			{
				float x = (float)point.X;
				float y = (float)point.Y;

				float h = Location.X + Width / 2;
				float k =Location.Y+ Height / 2;

				float rx = Width / 2;
				float ry = Height / 2;
				return Math.Pow(x - h, 2) / Math.Pow(rx,2)+Math.Pow(y-k,2) / Math.Pow(ry, 2) <= 1;
					}
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
                // Применяем прозрачность к цвету заливки
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

                // Рисуем контур с учетом ширины
                using (Pen pen = new Pen(StrokeColor, StrokeWidth))
                {
                    grfx.DrawPath(pen, path);
                }
            }
			grfx.Restore(state);
        }
    }
}
