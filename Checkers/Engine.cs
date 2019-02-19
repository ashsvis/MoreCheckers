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
                    case "scale":
                        compiled.Add(Scale(args));
                        break;
                    case "poly":
                        compiled.Add(Poly(args));
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
                        if (items.Length == 3 && items[1].Contains("="))
                        {
                            var vals = items[2].Split(new[] { ';' });
                            if (vals.Length == 1)
                            {
                                if (float.TryParse(vals[0], out float f))
                                    Floats[items[0]] = f;
                                else
                                    Strings[items[0]] = vals[0];
                            }
                            else
                            if (vals.Length == 2 &&
                                float.TryParse(vals[0], out float x) &&
                                float.TryParse(vals[1], out float y))
                                Markers[items[0]] = new PointF(x, y);
                            else
                            if (vals.Length == 3 &&
                                int.TryParse(vals[0], out int r) &&
                                int.TryParse(vals[1], out int g) &&
                                int.TryParse(vals[2], out int b))
                                Colors[items[0]] = Color.FromArgb(r, g, b);
                        }
                        break;
                }
            }

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
            if (ar.Strings.Count < 1)
                throw new Exception("Expected text");
            var emSize = Floats.ContainsKey("FontSize") ? Floats["FontSize"] : 8;
            return () =>
            {
                Path.AddString(ar.Strings[0], new FontFamily("Arial"), 0, emSize, ar.Markers[0], null);
            };
        }

        private Action Poly(string args)
        {
            var ar = new Args(this, args);
            if (ar.Markers.Count < 2)
                throw new Exception("Expected two markers");
            return () =>
            {
                Path.AddPolygon(ar.Markers.ToArray());
            };
        }

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

        private Action Draw()
        {
            var drawColor = Colors.ContainsKey("DrawColor") ? Colors["DrawColor"] : Color.Black;
            var drawOpacity = Floats.ContainsKey("DrawOpacity") ? (int)Floats["DrawOpacity"] : 255;
            return () =>
            {
                using (var pen = new Pen(Color.FromArgb(drawOpacity, drawColor)))
                    Graphics.DrawPath(pen, Path);
            };
        }
    }

    internal class Args
    {
        private readonly Engine engine;

        public Args(Engine engine, string args)
        {
            this.engine = engine;
            var items = args.Split(new[] { ' ' });
            foreach (var item in items)
            {
                if (engine.Markers.ContainsKey(item))
                    Markers.Add(engine.Markers[item]);
                else if (engine.Strings.ContainsKey(item))
                    Strings.Add(engine.Strings[item]);
                else if (engine.Floats.ContainsKey(item))
                    Floats.Add(engine.Floats[item]);
                else if (float.TryParse(item, out float f))
                    Floats.Add(f);
            }
        }

        public List<PointF> Markers { get; private set; } = new List<PointF>();
        public List<Color> Colors { get; private set; } = new List<Color>();
        public List<float> Floats { get; private set; } = new List<float>();
        public List<string> Strings { get; private set; } = new List<string>();
    }
}
