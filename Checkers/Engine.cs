using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Checkers
{
    /// <summary>
    /// Идея Storm23
    /// </summary>
    public class Engine
    {
        List<Action> compiled = new List<Action>();
        GraphicsPath Path = new GraphicsPath();
        Graphics Graphics;

        public Dictionary<string, PointF> Markers { get; private set; } = new Dictionary<string, PointF>();
        public Dictionary<string, Color> Colors { get; private set; } = new Dictionary<string, Color>();
        public Dictionary<string, float> Floats { get; private set; } = new Dictionary<string, float>();
        public Dictionary<string, string> Strings { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, RectangleF> Rects { get; private set; } = new Dictionary<string, RectangleF>();

        public void Draw(Graphics gr)
        {
            Graphics = gr;
            Path.Reset();
            foreach (var act in compiled)
                act();
        }

        public void Parse(string text)
        {
            compiled.Clear();
            var lines = text.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var items = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var args = string.Join(" ", items, 1, items.Length - 1);
                switch (items[0])
                {
                    case "offset":
                        compiled.Add(Offset(args));
                        break;
                    case "scale":
                        compiled.Add(Scale(args));
                        break;
                    case "rotate":
                        compiled.Add(Rotate(args));
                        break;
                    case "poly":
                        compiled.Add(Poly(args));
                        break;
                    case "rect":
                        compiled.Add(Rect(args));
                        break;
                    case "circle":
                        compiled.Add(Circle(args));
                        break;
                    case "text":
                        compiled.Add(Text(args));
                        break;
                    case "draw":
                        compiled.Add(Draw());
                        break;
                    case "fill":
                        compiled.Add(Fill());
                        break;
                    default:
                        if (items.Length > 2 && items[1].Contains("="))
                        {
                            var vals = items[2].Split(new[] { ';' });
                            if (vals.Length == 1)
                            {
                                if (items[0].StartsWith("TEXT"))
                                    Strings[items[0]] = vals[0];
                                else if (float.TryParse(vals[0], out float fval))
                                    Floats[items[0]] = fval;
                                else
                                    Strings[items[0]] = string.Join(" ", items, 2, items.Length - 2);
                            }
                            else
                            if (vals.Length == 2 &&
                                float.TryParse(vals[0], out float x) &&
                                float.TryParse(vals[1], out float y))
                                Markers[items[0]] = new PointF(x, y);
                            else
                            if (vals.Length == 3 &&
                                int.TryParse(vals[0], out int red) &&
                                int.TryParse(vals[1], out int green) &&
                                int.TryParse(vals[2], out int blue))
                                Colors[items[0]] = Color.FromArgb(red, green, blue);
                            else
                            if (vals.Length == 4 &&
                                float.TryParse(vals[0], out float left) &&
                                float.TryParse(vals[1], out float top) &&
                                float.TryParse(vals[2], out float width) &&
                                float.TryParse(vals[3], out float height))
                                Rects[items[0]] = new RectangleF(left, top, width, height);
                        }
                        break;
                }
            }

        }

        private Action Rect(string args)
        {
            var ar = new Args(this, args);
            if (ar.Rects.Count < 1)
                throw new Exception("Expected one rectangle");
            return () =>
            {
                Path.Reset();
                var rect = ar.Rects[0];
                Path.AddRectangle(rect);
            };
        }

        private Action Circle(string args)
        {
            var ar = new Args(this, args);
            if (ar.Rects.Count < 1)
                throw new Exception("Expected one rectangle");
            return () =>
            {
                Path.Reset();
                var rect = ar.Rects[0];
                Path.AddEllipse(rect);
                //var c = ar.Markers[0];
                //var m = ar.Markers[1];
                //var dx = m.X - c.X;
                //var dy = m.Y - c.Y;
                //var r = (float)Math.Sqrt(dx * dx + dy * dy);
                //Path.AddEllipse(c.X - r / 2, c.Y - r / 2, 2 * r, 2 * r);
            };
        }

        private Action Rotate(string args)
        {
            var ar = new Args(this, args);
            if (ar.Markers.Count < 1)
                throw new Exception("Expected one marker");

            if (ar.Floats.Count < 1)
                throw new Exception("Expected one float value");

            return () =>
            {
                var angle = ar.Floats[0];
                var c = ar.Markers[0];
                Graphics.TranslateTransform(c.X, c.Y);
                Graphics.RotateTransform(angle);
                Graphics.TranslateTransform(-c.X, -c.Y);
            };
        }

        private Action Offset(string args)
        {
            var ar = new Args(this, args);
            if (ar.Floats.Count < 2)
                throw new Exception("Expected two float values");

            return () =>
            {
                var x = ar.Floats[0];
                var y = ar.Floats[1];
                Graphics.TranslateTransform(x, y);
            };
        }

        private Action Scale(string args)
        {
            var ar = new Args(this, args);

            if (ar.Markers.Count < 1)
                throw new Exception("Expected one marker");

            if (ar.Floats.Count < 1)
                throw new Exception("Expected one float value");

            return () =>
            {
                var scale = ar.Floats[0];
                var c = ar.Markers[0];
                Graphics.TranslateTransform(c.X, c.Y);
                Graphics.ScaleTransform(scale, scale);
                Graphics.TranslateTransform(-c.X, -c.Y);
            };
        }

        private Action Text(string args)
        {
            var ar = new Args(this, args);
            if (ar.Markers.Count < 1)
                throw new Exception("Expected one marker");
            var text = ar.Strings.Count > 0 ? ar.Strings[0] : ar.Text;
            var emSize = Floats.ContainsKey("FontSize") ? Floats["FontSize"] : 8;
            var fontName = Strings.ContainsKey("FontName") ? Strings["FontName"] : "Arial";
            var textAlign = Strings.ContainsKey("TextAlign") ? Strings["TextAlign"] : "TopLeft";
            if (ar.Markers.Count == 1)
                return () =>
                {
                    Path.Reset();
                    using (var sf = GetTextAlign(textAlign))
                        Path.AddString(text, new FontFamily(fontName), 0, emSize, ar.Markers[0], sf);
                };
            if (ar.Markers.Count == 3)
                return () =>
                {
                    Path.Reset();
                    var layoutRect = new RectangleF(ar.Markers[0], 
                        new SizeF(ar.Markers[1].X - ar.Markers[0].X, ar.Markers[2].Y - ar.Markers[0].Y));
                    using (var sf = GetTextAlign(textAlign))
                        Path.AddString(text, new FontFamily(fontName), 0, emSize, layoutRect, sf);
                };
            return () => { };
        }

        private StringFormat GetTextAlign(string alignment)
        {
            var sf = new StringFormat();
            switch (alignment)
            {
                case "TopLeft":
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case "TopCenter":
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case "TopRight":
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case "BottomLeft":
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                case "BottomCenter":
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                case "BottomRight":
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                case "MiddleLeft":
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case "MiddleCenter":
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case "MiddleRight":
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Center;
                    break;
            }
            return sf;
        }

        /// <summary>
        /// Задание пути полигоном
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private Action Poly(string args)
        {
            var ar = new Args(this, args);
            if (ar.Markers.Count < 2)
                throw new Exception("Expected two markers");
            if (ar.Floats.Count > 0)
                return () =>
                {
                    Path.Reset();
                    Path.AddCurve(ar.Markers.ToArray(), ar.Floats[0]);
                };
            else
                return () =>
                {
                    Path.Reset();
                    Path.AddPolygon(ar.Markers.ToArray());
                };
        }

        /// <summary>
        /// Заливка контура пути
        /// </summary>
        /// <returns></returns>
        private Action Fill()
        {
            var fillColor = Colors.ContainsKey("FillColor") ? Colors["FillColor"] : Color.White;
            var fillOpacity = Floats.ContainsKey("FillOpacity") ? (int)Floats["FillOpacity"] : 255;
            return () =>
            {
                using (var brush = new SolidBrush(Color.FromArgb(fillOpacity, fillColor)))
                    Graphics.FillPath(brush, Path);
            };
        }

        /// <summary>
        /// Рисоание контура пути
        /// </summary>
        /// <returns></returns>
        private Action Draw()
        {
            var fillColor = Colors.ContainsKey("FillColor") ? Colors["FillColor"] : Color.Black;
            var fillOpacity = Floats.ContainsKey("FillOpacity") ? (int)Floats["FillOpacity"] : 255;
            var penWidth = Floats.ContainsKey("PenWidth") ? Floats["PenWidth"] : 1;
            return () =>
            {
                using (var pen = new Pen(Color.FromArgb(fillOpacity, fillColor), penWidth))
                    Graphics.DrawPath(pen, Path);
            };
        }
    }
}
