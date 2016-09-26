﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FlowSharpLib
{
	public enum AvailableLineCap
	{
		None,
		Arrow,
	};

	public class HorizontalLine : GraphicElement, ILine
	{
		public AvailableLineCap StartCap { get; set; }
		public AvailableLineCap EndCap { get; set; }

		public HorizontalLine(Canvas canvas) : base(canvas)
		{
			FillBrush = new SolidBrush(Color.White);
			BorderPen = new Pen(Color.Black);
			BorderPen.Width = 1;
			HasCornerAnchors = false;
			HasCenterAnchors = false;
			HasLeftRightAnchors = true;
		}

		public override ElementProperties CreateProperties()
		{
			return new LineProperties(this);
		}

		public override GraphicElement Clone(Canvas canvas)
		{
			HorizontalLine line = (HorizontalLine)base.Clone(canvas);
			line.StartCap = StartCap;
			line.EndCap = EndCap;

			return line;
		}

		public override Rectangle DefaultRectangle()
		{
			return new Rectangle(20, 20, 40, 20);
		}

		protected override void Draw(Graphics gr)
		{
			// See CustomLineCap for creating other possible endcaps besides arrows.
			// https://msdn.microsoft.com/en-us/library/system.drawing.drawing2d.customlinecap(v=vs.110).aspx

			AdjustableArrowCap adjCap = new AdjustableArrowCap(5, 5, true);
			Pen pen = (Pen)BorderPen.Clone();

			if (StartCap == AvailableLineCap.Arrow)
			{
				pen.CustomStartCap = adjCap;
			}

			if (EndCap == AvailableLineCap.Arrow)
			{
				pen.CustomEndCap = adjCap;
			}

			gr.DrawLine(pen, DisplayRectangle.LeftMiddle(), DisplayRectangle.RightMiddle());
			pen.Dispose();

			base.Draw(gr);
		}
	}
}
