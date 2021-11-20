using System;
using System.Text;

namespace AC2RE.Definitions {

    public readonly struct PhysiqueSpeciesSexId : IEquatable<PhysiqueSpeciesSexId> {

        public static readonly PhysiqueSpeciesSexId NULL = new(0u);

        public readonly uint id;

        public PhysiqueType physique => (PhysiqueType)((id >> 16) & 0xFFFF);
        public SexType sex => (SexType)(id & 0x0000F000);
        public SpeciesType species => (SpeciesType)(id & 0x00000FFF);

        public PhysiqueSpeciesSexId(uint id) {
            this.id = id;
        }

        public PhysiqueSpeciesSexId(SpeciesType species) {
            id = (uint)species;
        }

        public PhysiqueSpeciesSexId(SexType sex) {
            id = (uint)sex;
        }

        public PhysiqueSpeciesSexId(SpeciesType species, SexType sex) {
            id = ((uint)sex | (uint)species);
        }

        public PhysiqueSpeciesSexId(PhysiqueType physique, SpeciesType species, SexType sex) {
            id = (((uint)physique << 16) | (uint)sex | (uint)species);
        }

        public static bool operator ==(PhysiqueSpeciesSexId lhs, PhysiqueSpeciesSexId rhs) => lhs.id == rhs.id;
        public static bool operator !=(PhysiqueSpeciesSexId lhs, PhysiqueSpeciesSexId rhs) => lhs.id != rhs.id;
        public bool Equals(PhysiqueSpeciesSexId other) => id == other.id;
        public override bool Equals(object obj) => obj is PhysiqueSpeciesSexId castObj && id == castObj.id;
        public override int GetHashCode() => id.GetHashCode();

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            if (physique != PhysiqueType.Undef) {
                sb.Append(physique);
            }
            if (species != SpeciesType.Undef) {
                if (sb.Length > 0) {
                    sb.Append('|');
                }
                sb.Append(species);
            }
            if (sex != SexType.Undef) {
                if (sb.Length > 0) {
                    sb.Append('|');
                }
                sb.Append(sex);
            }
            return sb.ToString();
        }
    }
}
