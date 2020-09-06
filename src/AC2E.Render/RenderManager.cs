using AC2E.Def;
using AC2E.RenderCommon;
using System;
using System.Collections.Generic;

namespace AC2E.Render {

    public class RenderManager : IDisposable {

        private readonly RenderResourceManager resourceManager = new RenderResourceManager();
        private readonly IRenderer renderer;

        private float vFov = 60.0f;
        private float nearClip = 0.05f;
        private float farClip = 1000.0f;

        private Mat4x4 cameraToClipMatrix = Mat4x4.identity();
        private Mat4x4 worldToCameraMatrix = Mat4x4.identity();
        private Mat4x4 modelToWorldMatrix = Mat4x4.identity();

        private readonly List<RenderObject> renderObjects = new List<RenderObject>();

        public void Dispose() {
            resourceManager.Dispose();
        }

        public RenderManager(IRenderer renderer) {
            this.renderer = renderer;
        }

        public void resize(uint width, uint height) {
            renderer.resize(width, height);
            cameraToClipMatrix = Mat4x4.perspective(vFov, width, height, nearClip, farClip);
        }

        public void setCamera(float x, float y, float z) {
            worldToCameraMatrix = Mat4x4.identity();
            worldToCameraMatrix.translate(-x, -y, -z);
        }

        public List<IMesh> loadDatMeshes(DataId did) {
            return resourceManager.loadDatMeshes(renderer, "G:\\Asheron's Call 2\\portal.dat", did);
        }

        public RenderObject addRenderObject(List<IMesh> meshes, float x, float y, float z) {
            RenderObject renderObject = new RenderObject {
                meshes = meshes,
                x = x,
                y = y,
                z = z,
            };
            renderObjects.Add(renderObject);

            return renderObject;
        }

        public void draw() {
            renderer.clear();

            Mat4x4 worldToClipMatrix = cameraToClipMatrix * worldToCameraMatrix;

            foreach (RenderObject renderObject in renderObjects) {
                modelToWorldMatrix = Mat4x4.identity();
                modelToWorldMatrix.translate(renderObject.x, renderObject.y, renderObject.z);

                Mat4x4 modelToClipMatrix = worldToClipMatrix * modelToWorldMatrix;
                renderer.setModelToClipTransform(modelToClipMatrix.m);

                foreach (IMesh mesh in renderObject.meshes) {
                    renderer.draw(mesh);
                }
            }

            renderer.swap();
        }
    }
}
