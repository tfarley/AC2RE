// NOTE: Perform a Rebuild for changes to this file to take effect
#version 410 core

layout(location = 0) in vec3 pos;

layout(std140) uniform ViewData {
    mat4 modelToClipMatrix;
    mat4 modelToCameraMatrix;
};

void main() {
    gl_Position = modelToClipMatrix * vec4(pos, 1.0);
}
