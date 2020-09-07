using AC2E.Def;
using AC2E.Render;
using AC2E.RenderCommon;
using AC2E.UICommon;
using System.Diagnostics;
using System.Numerics;
using System.Windows;
using System.Windows.Media;

namespace AC2E.PacketTool.UI {

    public partial class RenderPreview : Window {

        private static readonly float MIN_DT = 1.0f / 120.0f;

        public Vector3 cameraOffset = new Vector3(0.0f, -5.0f, 0.0f);
        public Quaternion cameraRot = Quaternion.Identity;

        private IRenderer renderer;
        private RenderManager renderManager;
        private Stopwatch renderStopwatch = new Stopwatch();
        private float lastRenderTime;

        private RenderObject testObject;

        public RenderPreview() {
            InitializeComponent();

            HwndElement renderElement = new HwndElement();

            renderContainer.Child = renderElement;

            renderer = IRenderer.createWinOGL(renderElement.hwnd);
            renderManager = new RenderManager(renderer);

            testObject = renderManager.addRenderObject(renderManager.loadDatMeshes(new DataId(0x1F000023)));

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
            float curElapsedTime = (float)renderStopwatch.Elapsed.TotalSeconds;
            float dt = curElapsedTime - lastRenderTime;
            if (renderManager != null && dt >= MIN_DT) {
                renderManager.worldToCameraMatrix = Matrix4x4.CreateFromQuaternion(cameraRot);
                renderManager.worldToCameraMatrix.Translation -= cameraOffset;

                // For absolute translation:
                //renderManager.worldToCameraMatrix.Translation -= Vector3.Transform(cameraOffset, renderManager.worldToCameraMatrix);

                renderManager.draw();
                lastRenderTime = curElapsedTime;

                cameraRot = cameraRot * Quaternion.CreateFromAxisAngle(new Vector3(0.0f, 0.0f, 1.0f), 1.0f * dt);
            }
        }
    }
}
