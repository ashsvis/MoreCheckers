using System.Collections.Generic;
using System.Drawing;

namespace Checkers
{
    internal class Args
    {
        private readonly Engine engine;

        public Args(Engine engine, string args)
        {
            this.engine = engine;
            var items = args.Split(new[] { ' ' });
            var others = new List<string>();
            foreach (var item in items)
            {
                if (engine.Markers.ContainsKey(item))
                    Markers.Add(engine.Markers[item]);
                else if (engine.Rects.ContainsKey(item))
                    Rects.Add(engine.Rects[item]);
                else if (engine.Strings.ContainsKey(item))
                    Strings.Add(engine.Strings[item]);
                else if (engine.Floats.ContainsKey(item))
                    Floats.Add(engine.Floats[item]);
                else if (float.TryParse(item, out float f))
                    Floats.Add(f);
                else
                    others.Add(item);
            }
            if (others.Count > 0)
                Text = string.Join(" ", others);
        }

        public List<PointF> Markers { get; private set; } = new List<PointF>();
        public List<Color> Colors { get; private set; } = new List<Color>();
        public List<float> Floats { get; private set; } = new List<float>();
        public List<string> Strings { get; private set; } = new List<string>();
        public List<RectangleF> Rects { get; private set; } = new List<RectangleF>();
        public string Text { get; private set; } = string.Empty;
    }
}
