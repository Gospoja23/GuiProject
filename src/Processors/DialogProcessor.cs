﻿using System;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor
		
		public DialogProcessor()
		{
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Избран елемент.
		/// </summary>
		private Shape selection;
		public Shape Selection {
			get { return selection; }
			set { selection = value; }
		}
		
		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
		
		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation {
			get { return lastLocation; }
			set { lastLocation = value; }
		}
		
		#endregion
		
		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>
		public void AddRandomRectangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100,1000);
			int y = rnd.Next(100,600);

            RectangleShape rect = 
				new RectangleShape(
					new Rectangle(x,y,100,200)
				);
			rect.FillColor = Color.White;

			ShapeList.Add(rect);
		}

        public void AddRandomEllipse()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            EllipseShape ellipse =
                new EllipseShape(
                    new Rectangle(x, y, 100, 200)
                );
			ellipse.FillColor = Color.White;
            ShapeList.Add(ellipse);
        }
        public void AddRandomTriangle()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            TriangleShape triangle =
                new TriangleShape(
                    new Rectangle(x, y, 100, 200)
                );
            triangle.FillColor = Color.White;

            ShapeList.Add(triangle);
        }

        public void AddRandomPoint()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            PointShape point =
                new PointShape(
                    new Rectangle(x, y, 10, 10)
                );
            point.FillColor = Color.Black;

            ShapeList.Add(point);
        }

        public void AddRandomLine()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            LineShape line =
                new LineShape(
                    new Rectangle(x, y, 100, 200)
                );
            line.FillColor = Color.White;

            ShapeList.Add(line);
        }

        public void AddRandomStar()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            StarShape star =
                new StarShape(
                    new Rectangle(x, y, 100, 100)
                );
            star.FillColor = Color.White;

            ShapeList.Add(star);
        }
        /// <summary>
        /// Проверява дали дадена точка е в елемента.
        /// Обхожда в ред обратен на визуализацията с цел намиране на
        /// "най-горния" елемент т.е. този който виждаме под мишката.
        /// </summary>
        /// <param name="point">Указана точка</param>
        /// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
        public Shape ContainsPoint(PointF point)
		{
			for(int i = ShapeList.Count - 1; i >= 0; i--){
				if (ShapeList[i].Contains(point)){
					//ShapeList[i].FillColor = Color.Blue;
						
					return ShapeList[i];
				}	
			}
			return null;
		}
		
		/// <summary>
		/// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
		/// </summary>
		/// <param name="p">Вектор на транслация.</param>
		public void TranslateTo(PointF p)
		{
			if (selection != null) {
				selection.Location = new PointF(selection.Location.X + p.X - lastLocation.X, selection.Location.Y + p.Y - lastLocation.Y);
				lastLocation = p;
			}
		}
	}
}
