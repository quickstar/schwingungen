using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace Schwingungen
{
    public sealed partial class Form1 : Form
    {
        private readonly List<PointF> _points1 = new List<PointF>();
        private readonly List<PointF> _points2 = new List<PointF>();
        private double _timeElapsed;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var auslenkung1 = 50 + 30 * Math.Cos(2 * Math.PI * 1.5 * _timeElapsed);
            var auslenkung2 = 50 + 30 * Math.Sin(2 * Math.PI * 1.5 * _timeElapsed);
            e.Graphics.FillEllipse(Brushes.Red, 30, (float)auslenkung1, 6, 6);
            e.Graphics.FillEllipse(Brushes.Green, 30, (float)auslenkung2, 6, 6);
            _points1.Add(new PointF((float)(50 + 75 * _timeElapsed), (float)auslenkung1));
            _points2.Add(new PointF((float)(50 + 150 * _timeElapsed), (float)auslenkung2));
            if (_points1.Count > 1)
            {
                e.Graphics.DrawLines(Pens.Red, _points1.ToArray());
            }
            if (_points2.Count > 1)
            {
                e.Graphics.DrawLines(Pens.Green, _points2.ToArray());
            }
        }

        private void renderingTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            _timeElapsed += renderingTimer.Interval * 0.001;
            Text = Math.Round(_timeElapsed, 2).ToString(CultureInfo.InvariantCulture);
        }
    }
}