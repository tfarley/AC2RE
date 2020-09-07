// NOTE: Perform a Rebuild for changes to this file to take effect

layout(location = 0) in vec3 pos;

#ifdef HAS_NORMAL
layout(location = 1) in vec3 normal;
#endif

#ifdef HAS_DIFFUSE_COLOR
layout(location = 2) in vec4 diffuseColor;
#endif

#ifdef HAS_SPECULAR_COLOR
layout(location = 3) in vec4 specularColor;
#endif

#ifdef HAS_TANGENT
layout(location = 4) in vec3 tangent;
#endif

#ifdef HAS_BITANGENT
layout(location = 5) in vec3 bitangent;
#endif

#if (NUM_TEXCOORDS > 0)
layout(location = 6) in vec2 texCoords[NUM_TEXCOORDS];
#endif

#if (NUM_MATRICES > 0)
layout(location = 8) in int matrixIndices[NUM_MATRICES];
layout(location = 12) in float matrixWeights[NUM_MATRICES];
#endif

layout(std140) uniform ViewData {
    mat4 modelToClipMatrix;
    mat4 modelToCameraMatrix;
};

#ifdef HAS_NORMAL
out vec3 cameraPosFrag;
out vec3 cameraNormalFrag;

#ifdef HAS_TANGENT
#ifdef HAS_BITANGENT
out mat3 tbnFrag;
#endif
#endif
#endif

#ifdef HAS_DIFFUSE_COLOR
out vec4 diffuseColorFrag;
#endif

#ifdef HAS_SPECULAR_COLOR
out vec4 specularColorFrag;
#endif

#if (NUM_TEXCOORDS > 0)
out vec2 texCoordsFrag[NUM_TEXCOORDS];
#endif

void main() {
    gl_Position = modelToClipMatrix * vec4(pos, 1.0f);

#ifdef HAS_NORMAL
    cameraPosFrag = vec3(modelToCameraMatrix * vec4(pos, 1.0f));
    cameraNormalFrag = vec3(modelToCameraMatrix * vec4(normal, 0.0f));
#ifdef HAS_TANGENT
#ifdef HAS_BITANGENT
    tbnFrag = mat3(
        normalize(vec3(modelToCameraMatrix * vec4(tangent, 0.0f))),
        normalize(vec3(modelToCameraMatrix * vec4(bitangent, 0.0f))),
        normalize(vec3(modelToCameraMatrix * vec4(normal, 0.0f))));
#endif
#endif
#endif

#ifdef HAS_DIFFUSE_COLOR
    diffuseColorFrag = diffuseColor;
#endif

#ifdef HAS_SPECULAR_COLOR
    specularColorFrag = specularColor;
#endif

#if (NUM_TEXCOORDS > 0)
    for (int i = 0; i < NUM_TEXCOORDS; i++) {
        texCoordsFrag[i] = texCoords[i];
    }
#endif
}
