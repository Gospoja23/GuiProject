﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Draw
{
	/// <summary>
	/// Базовия клас на примитивите, който съдържа общите характеристики на примитивите.
	/// </summary>
	public abstract class Shape
	{
		#region Constructors
		
		public Shape()
		{
		}
		
		public Shape(RectangleF rect)
		{
			rectangle = rect;
		}
		
		public Shape(Shape shape)
		{
			this.Height = shape.Height;
			this.Width = shape.Width;
			this.Location = shape.Location;
			this.rectangle = shape.rectangle;
			
			this.FillColor =  shape.FillColor;
		}
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Обхващащ правоъгълник на елемента.
		/// </summary>
		private RectangleF rectangle;		
		public virtual RectangleF Rectangle {
			get { return rectangle; }
			set { rectangle = value; }
		}
		
		/// <summary>
		/// Широчина на елемента.
		/// </summary>
		public virtual float Width {
			get { return Rectangle.Width; }
			set { rectangle.Width = value; }
		}
		
		/// <summary>
		/// Височина на елемента.
		/// </summary>
		public virtual float Height {
			get { return Rectangle.Height; }
			set { rectangle.Height = value; }
		}
		
		/// <summary>
		/// Горен ляв ъгъл на елемента.
		/// </summary>
		public virtual PointF Location {
			get { return Rectangle.Location; }
			set { rectangle.Location = value; }
		}
		
		/// <summary>
		/// Цвят на елемента.
		/// </summary>
		private Color fillColor;		
		public virtual Color FillColor 
		{
			get { return fillColor; }
			set { fillColor = value; }
		}

        private Color strokeColor;
        public virtual Color StrokeColor
        {
            get { return strokeColor; }
            set { strokeColor = Color.Black; }
        }

		private float strokeWidth=1;
		public virtual float StrokeWidth
		{
			get { return strokeWidth; }
			set { strokeWidth = value; }
		}

        private int fillOpacity=255;
		public virtual int FillOpacity
		{
			get { return fillOpacity; }
			set { fillOpacity = value; }
		}

		private Matrix transformationMatrix = new Matrix();

		public Matrix TransformationMatrix
		{
			get { return transformationMatrix; }
			set { transformationMatrix = value; }
			}


        private Color gradientStartColor = Color.White;
        public virtual Color GradientStartColor
        {
            get { return gradientStartColor; }
            set { gradientStartColor = value; }
        }

        private Color gradientEndColor = Color.Black;
        public virtual Color GradientEndColor
        {
            get { return gradientEndColor; }
            set { gradientEndColor = value; }
        }

        private bool useGradient = false;
        public virtual bool UseGradient
        {
            get { return useGradient; }
            set { useGradient = value; }
        }



        #endregion


        /// <summary>
        /// Проверка дали точка point принадлежи на елемента.
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>Връща true, ако точката принадлежи на елемента и
        /// false, ако не пренадлежи</returns>
        public virtual bool Contains(PointF point)
		{
			return Rectangle.Contains(point.X, point.Y);
		}
		
		/// <summary>
		/// Визуализира елемента.
		/// </summary>
		/// <param name="grfx">Къде да бъде визуализиран елемента.</param>
		public virtual void DrawSelf(Graphics grfx)
		{
			// shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
		}
		
	}
}
