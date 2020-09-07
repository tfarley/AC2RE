// NOTE: Perform a Rebuild for changes to this file to take effect
#version 410 core

layout(location = 0) in vec3 pos;
layout(location = 1) in vec3 normal;

layout(std140) uniform ViewData {
    mat4 modelToClipMatrix;
    mat4 modelToCameraMatrix;
};

out vec3 fragCameraPos;
out vec3 fragCameraNormal;

void main() {
    gl_Position = modelToClipMatrix * vec4(pos, 1.0f);
    fragCameraPos = vec3(modelToCameraMatrix * vec4(pos, 1.0f));
    fragCameraNormal = vec3(modelToCameraMatrix * vec4(normal, 0.0f));
}
