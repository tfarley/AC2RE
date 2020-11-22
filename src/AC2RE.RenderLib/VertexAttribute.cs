using System;

namespace AC2RE.RenderLib {

    public class VertexAttribute {

        public readonly uint id;
        public readonly uint numComponents;
        public readonly Type componentType;
        public readonly uint offset;
        public readonly bool normalize;

        public VertexAttribute(uint id, uint numComponents, Type componentType, uint offset, bool normalize = false) {
            this.id = id;
            this.numComponents = numComponents;
            this.componentType = componentType;
            this.offset = offset;
            this.normalize = normalize;
        }
    }
}
