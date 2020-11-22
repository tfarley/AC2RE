using AC2RE.Definitions;
using AC2RE.RenderLib;
using AC2RE.RenderLib.OpenGL;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2RE.Renderer {

    public class RenderManager : IDisposable {

        public Matrix4x4 worldToCameraMatrix;

        private readonly IRenderer renderer;
        private readonly RenderResourceManager resourceManager;

        private float vFov = 60.0f;
        private float nearClip = 0.05f;
        private float farClip = 1000.0f;

        private Matrix4x4 cameraToClipMatrix = Matrix4x4.Identity;

        private readonly List<RenderObject> renderObjects = new();

        private DirLight dirLight = new() {
            dir = new(-1.0f, 0.0f, -1.0f),
            color = new(0.75f, 0.75f, 0.75f),
        };

        public void Dispose() {
            resourceManager.Dispose();
        }

        public RenderManager(IRenderer renderer, DatReader datReader) {
            this.renderer = renderer;
            resourceManager = new(datReader);

            renderer.setClearColor(0.0f, 0.25f, 0.25f);
            renderer.setAmbientLight(0.25f, 0.25f, 0.25f);
        }

        public void resize(uint width, uint height) {
            renderer.resize(width, height);
            cameraToClipMatrix = RenderUtil.perspective(vFov, width, height, nearClip, farClip);
        }

        public List<RenderMesh>? loadDatMeshes(DataId did) {
            return resourceManager.loadDatMeshes(renderer, did);
        }

        public RenderObject addRenderObject(List<RenderMesh> meshes) {
            return addRenderObject(meshes, Vector3.Zero, Quaternion.Identity);
        }

        public RenderObject addRenderObject(List<RenderMesh> meshes, Vector3 pos) {
            return addRenderObject(meshes, pos, Quaternion.Identity);
        }

        public RenderObject addRenderObject(List<RenderMesh> meshes, Vector3 pos, Quaternion rot) {
            RenderObject renderObject = new(meshes) {
                pos = pos,
                rot = rot,
            };
            renderObjects.Add(renderObject);

            return renderObject;
        }

        public void removeRenderObject(RenderObject renderObject) {
            renderObjects.Remove(renderObject);
        }

        public void draw() {
            renderer.clear();

            Matrix4x4 worldToClipMatrix = worldToCameraMatrix * cameraToClipMatrix;

            DirLight dirLightCameraSpace = new(dirLight);
            dirLightCameraSpace.dir = Vector3.TransformNormal(dirLightCameraSpace.dir, worldToCameraMatrix);
            renderer.setDirLight(dirLightCameraSpace);

            foreach (RenderObject renderObject in renderObjects) {
                Matrix4x4 modelToWorldMatrix = Matrix4x4.CreateFromQuaternion(renderObject.rot);
                modelToWorldMatrix.Translation += renderObject.pos;

                Matrix4x4 modelToClipMatrix = modelToWorldMatrix * worldToClipMatrix;
                Matrix4x4 modelToCameraMatrix = modelToWorldMatrix * worldToCameraMatrix;
                renderer.setTransforms(modelToClipMatrix, modelToCameraMatrix);

                foreach (RenderMesh mesh in renderObject.meshes) {
                    renderer.draw(mesh.mesh, resourceManager.uberShaderManager.getShader(renderer, mesh.vertexFormat), mesh.textures);
                }
            }

            renderer.swap();
        }
    }
}
