// NOTE: Perform a Rebuild for changes to this file to take effect
#version 410 core

in vec3 fragCameraPos;
in vec3 fragCameraNormal;

layout(location = 0) out vec4 diffuseColor;

layout(std140) uniform AmbientLightData {
    vec4 ambientLightColor;
};

layout(std140) uniform DirLightData {
    vec4 dirLightDir;
    vec4 dirLightColor;
};

vec3 calcDirLightContribution(vec3 normal);

void main() {
    vec3 normal = normalize(fragCameraNormal);
    diffuseColor = vec4(vec3(ambientLightColor) + calcDirLightContribution(normal), 1.0f);
}

vec3 calcDirLightContribution(vec3 normal) {
    // Input light dir should be normalized and in camera space
    return -dot(vec3(dirLightDir), normal) * vec3(dirLightColor);
}
