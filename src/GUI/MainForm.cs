﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		/// <summary>
		/// Изход от програмата. Затваря главната форма, а с това и програмата.
		/// </summary>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		
		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}
		
		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();
			
			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";
			
			viewPort.Invalidate();
		}

		/// <summary>
		/// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
		/// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
		/// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
		/// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
		/// </summary>
		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked) {
				dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
				if (dialogProcessor.Selection != null) {
					statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
					dialogProcessor.IsDragging = true;
					dialogProcessor.LastLocation = e.Location;
					viewPort.Invalidate();
				}
			}
		}

		/// <summary>
		/// Прихващане на преместването на мишката.
		/// Ако сме в режм на "влачене", то избрания елемент се транслира.
		/// </summary>
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging) {
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

        private void DrawEllipseStripButton_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomEllipse();

            statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

            viewPort.Invalidate();
        }

        private void DrawTriangleButton(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomTriangle();

            statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";

            viewPort.Invalidate();

        }

        private void DrawPointButton(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomPoint();

            statusBar.Items[0].Text = "Последно действие: Рисуване на точка";

            viewPort.Invalidate();
        }

        private void DrawLineButton(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomLine();

            statusBar.Items[0].Text = "Последно действие: Рисуване на линия";

            viewPort.Invalidate();
        }


        

        private void ChooseColorButton(object sender, EventArgs e)
        {
			if (colorDialog1.ShowDialog()==DialogResult.OK)
			{
				if(dialogProcessor.Selection!=null)
					{
					dialogProcessor.Selection.FillColor = colorDialog1.Color;

					viewPort.Invalidate();
				}
			}
        }

        private void DrawStarButton(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomStar();

            statusBar.Items[0].Text = "Последно действие: Рисуване на линия";

            viewPort.Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
			if(dialogProcessor.Selection!=null)
			{
				dialogProcessor.Selection.FillOpacity = trackBar1.Value;

				viewPort.Invalidate();
			}
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection != null)
            {
                dialogProcessor.Selection.StrokeWidth = trackBar2.Value;

                viewPort.Invalidate();
            }
        }

        private void buttonGradientStart(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (dialogProcessor.Selection != null)
                {
                    dialogProcessor.Selection.GradientStartColor = colorDialog1.Color;
                    viewPort.Invalidate();
                }
            }
        }

        private void buttonGradientEnd(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (dialogProcessor.Selection != null)
                {
                    dialogProcessor.Selection.GradientEndColor = colorDialog1.Color;
                    viewPort.Invalidate();
                }
            }
        }

        private void checkBoxUseGradient_CheckedChanged(object sender, EventArgs e)
        {
            if (dialogProcessor.Selection != null)
            {
                dialogProcessor.Selection.UseGradient = checkBox1.Checked;
                viewPort.Invalidate();
            }
        }

        private void RotateButton(object sender, EventArgs e)
        {
			if(dialogProcessor.Selection != null)
			{
				dialogProcessor.Selection.TransformationMatrix.RotateAt(
					30,
					new PointF(
						dialogProcessor.Selection.Location.X + dialogProcessor.Selection.Width / 2,
						dialogProcessor.Selection.Location.Y + dialogProcessor.Selection.Height / 2

                        ));
				viewPort.Invalidate();
            }
        }
    }
}
