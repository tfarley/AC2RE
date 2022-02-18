using AC2RE.Definitions;
using AC2RE.Renderer;
using AC2RE.RenderLib;
using AC2RE.UICommon;
using AC2RE.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Windows;
using System.Windows.Media;

namespace AC2RE.DatTool.UI;

public partial class ModelViewerWindow : Window {

    private static readonly float MIN_DT = 1.0f / 120.0f;

    public Vector3 cameraOffset = new(0.0f, -5.0f, 0.0f);
    public Quaternion cameraRot = Quaternion.Identity;

    private readonly IRenderer renderer;
    private readonly RenderManager renderManager;
    private readonly Stopwatch renderStopwatch = new();
    private float lastRenderTime;

    private RenderObject? testObject;

    public ModelViewerWindow(DatReader datReader, DataId initialDid) {
        InitializeComponent();

        HwndElement renderElement = new();

        renderContainer.Child = renderElement;

        renderer = IRenderer.createWinOGL(renderElement.hwnd);
        renderManager = new(renderer, datReader);

        CompositionTarget.Rendering += CompositionTarget_Rendering;

        renderElement.SizeChanged += (_, e) => {
            renderManager.resize((uint)e.NewSize.Width, (uint)e.NewSize.Height);
        };

        Closed += (_, _) => {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
            renderElement.Dispose();
            renderManager.Dispose();
            renderer.Dispose();
        };

        renderStopwatch.Start();

        renderDidTextBox.TextChanged += (_, _) => updateRenderObject();

        renderDidTextBox.Text = initialDid.ToString();
    }

    private void updateRenderObject() {
        uint inputDid;
        try {
            inputDid = Convert.ToUInt32(renderDidTextBox.Text, 16);
        } catch (Exception) {
            return;
        }

        if (testObject != null) {
            renderManager.removeRenderObject(testObject);
            testObject = null;
        }

        UIUtil.swallowException(() => {
            List<RenderMesh>? meshes = renderManager.loadDatMeshes(new(inputDid));
            if (meshes != null) {
                testObject = renderManager.addRenderObject(meshes);
            }
        });
    }

    private void CompositionTarget_Rendering(object? sender, EventArgs e) {
        float curElapsedTime = (float)renderStopwatch.Elapsed.TotalSeconds;
        float dt = curElapsedTime - lastRenderTime;
        if (renderManager != null && dt >= MIN_DT) {
            renderManager.worldToCameraMatrix = Matrix4x4.CreateFromQuaternion(cameraRot);
            renderManager.worldToCameraMatrix.Translation -= cameraOffset;

            // For absolute translation:
            //renderManager.worldToCameraMatrix.Translation -= Vector3.Transform(cameraOffset, renderManager.worldToCameraMatrix);

            renderManager.draw();
            lastRenderTime = curElapsedTime;

            cameraRot *= MathUtil.quaternionFromAxisAngleLeftHanded(new(0.0f, 0.0f, 1.0f), 1.0f * dt);
        }
    }
}
