using AC2E.Def;
using AC2E.Render;
using AC2E.RenderCommon;
using AC2E.UICommon;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace AC2E.PacketTool.UI {

    public partial class RenderPreview : Window {

        private IRenderer renderer;
        private RenderManager renderManager;
        private Stopwatch renderStopwatch = new Stopwatch();
        private long lastRenderMs;

        public RenderPreview() {
            InitializeComponent();

            HwndElement renderElement = new HwndElement();

            renderContainer.Child = renderElement;

            renderer = IRenderer.createWinOGL(renderElement.hwnd);
            renderManager = new RenderManager(renderer);
            renderManager.setCamera(0.0f, -5.0f, 0.0f);

            renderManager.addRenderObject(renderManager.loadDatMeshes(new DataId(0x1F000023)), 0.0f, 0.0f, 0.0f);

            CompositionTarget.Rendering += CompositionTarget_Rendering;

            renderElement.SizeChanged += (sender, e) => {
                renderManager.resize((uint)e.NewSize.Width, (uint)e.NewSize.Height);
            };

            Closed += (sender, e) => {
                CompositionTarget.Rendering -= CompositionTarget_Rendering;
                renderManager.Dispose();
                renderer.Dispose();
            };

            renderStopwatch.Start();
        }

        private void CompositionTarget_Rendering(object sender, System.EventArgs e) {
            long curElapsedMs = renderStopwatch.ElapsedMilliseconds;
            if (renderManager != null && curElapsedMs - lastRenderMs >= (1000.0f / 120.0f)) {
                renderManager.draw();
                lastRenderMs = curElapsedMs;
            }
        }
    }
}
