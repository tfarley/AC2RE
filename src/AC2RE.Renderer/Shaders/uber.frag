// NOTE: Perform a Rebuild for changes to this file to take effect

#ifdef HAS_NORMAL
in vec3 cameraPosFrag;
in vec3 cameraNormalFrag;

#ifdef HAS_TANGENT
#ifdef HAS_BITANGENT
in mat3 tbnFrag;
#endif
#endif
#endif

#ifdef HAS_DIFFUSE_COLOR
in vec4 diffuseColorFrag;
#endif

#ifdef HAS_SPECULAR_COLOR
in vec4 specularColorFrag;
#endif

#if (NUM_TEXCOORDS > 0)
in vec2 texCoordsFrag[NUM_TEXCOORDS];
#endif

layout(location = 0) out vec4 colorOut;

layout(std140) uniform AmbientLightData {
    vec4 ambientLightColor;
};

layout(std140) uniform DirLightData {
    vec4 dirLightDir;
    vec4 dirLightColor;
};

#if (NUM_TEXCOORDS > 0)
layout(binding = 0) uniform sampler2D textures[NUM_TEXCOORDS];
#endif

#ifdef HAS_NORMAL
vec3 calcDirLightContribution(vec3 normal);
#endif

void main() {
#ifdef HAS_DIFFUSE_COLOR
    vec4 diffuseColor = diffuseColorFrag;
#else
    vec4 diffuseColor = vec4(0.0f);
#endif

#ifdef HAS_SPECULAR_COLOR
    vec4 specularColor = specularColorFrag;
#else
    vec4 specularColor = vec4(1.0f);
#endif

#if (NUM_TEXCOORDS > 0)
    diffuseColor += texture(textures[0], texCoordsFrag[0]);
#endif

    vec3 lightColor = vec3(ambientLightColor);

#ifdef HAS_NORMAL
    vec3 normal = normalize(cameraNormalFrag);

#if (NUM_TEXCOORDS > 1)
    // TODO: This is not correct, there may be a different way that normal map is bound
    //normal = texture(textures[1], texCoordsFrag[1]).xyz;
    //normal = normal * 2.0f - 1.0f;
    //normal = normalize(tbnFrag * normal);
#endif

    lightColor += calcDirLightContribution(normal);
#endif

    // TODO: Diffuse color actually is 0x00000000 in data for some reason, yet specular is 0xFFFFFFFF - should they be switched?
    colorOut = vec4(lightColor * vec3(diffuseColor), 1.0f);
}

#ifdef HAS_NORMAL
vec3 calcDirLightContribution(vec3 normal) {
    // Input light dir should be normalized and in camera space
    return max(-dot(vec3(dirLightDir), normal), 0.0f) * vec3(dirLightColor);
}
#endif
