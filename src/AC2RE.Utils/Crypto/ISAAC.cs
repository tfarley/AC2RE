namespace AC2RE.Utils {

    public class ISAAC {

        private static readonly uint GOLDEN_RATIO = 0x9e3779b9;

        private readonly uint[] randrsl = new uint[256];
        private int randcnt = 255;
        private readonly uint[] mm = new uint[256];
        private uint aa, bb, cc;

        public ISAAC(uint seed) {
            uint a, b, c, d, e, f, g, h;
            a = b = c = d = e = f = g = h = GOLDEN_RATIO;

            // scramble it
            for (int i = 0; i < 4; i++) {
                mix(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);
            }

            // fill in mm[] with messy stuff
            for (int i = 0; i < 256; i += 8) {
                a += randrsl[i]; b += randrsl[i + 1]; c += randrsl[i + 2]; d += randrsl[i + 3];
                e += randrsl[i + 4]; f += randrsl[i + 5]; g += randrsl[i + 6]; h += randrsl[i + 7];
                mix(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);
                mm[i] = a; mm[i + 1] = b; mm[i + 2] = c; mm[i + 3] = d;
                mm[i + 4] = e; mm[i + 5] = f; mm[i + 6] = g; mm[i + 7] = h;
            }

            for (int i = 0; i < 256; i += 8) {
                a += mm[i]; b += mm[i + 1]; c += mm[i + 2]; d += mm[i + 3];
                e += mm[i + 4]; f += mm[i + 5]; g += mm[i + 6]; h += mm[i + 7];
                mix(ref a, ref b, ref c, ref d, ref e, ref f, ref g, ref h);
                mm[i] = a; mm[i + 1] = b; mm[i + 2] = c; mm[i + 3] = d;
                mm[i + 4] = e; mm[i + 5] = f; mm[i + 6] = g; mm[i + 7] = h;
            }

            aa = bb = cc = seed;
            isaac();
        }

        public uint next() {
            uint result = randrsl[randcnt];

            // QTIsaac starts at 255 and goes backwards
            randcnt--;

            if (randcnt < 0) {
                isaac();
                randcnt = 255;
            }

            return result;
        }

        private static void mix(ref uint a, ref uint b, ref uint c, ref uint d, ref uint e, ref uint f, ref uint g, ref uint h) {
            a ^= (b << 11); d += a; b += c;
            b ^= (c >> 2); e += b; c += d;
            c ^= (d << 8); f += c; d += e;
            d ^= (e >> 16); g += d; e += f;
            e ^= (f << 10); h += e; f += g;
            f ^= (g >> 4); a += f; g += h;
            g ^= (h << 8); b += g; h += a;
            h ^= (a >> 9); c += h; a += b;
        }

        private void isaac() {
            cc++; // cc just gets incremented once per 256 results
            bb += cc; // then combined with bb

            for (int i = 0; i < 256; i++) {
                uint x = mm[i];
                switch (i % 4) {
                    case 0: aa ^= (aa << 13); break;
                    case 1: aa ^= (aa >> 6); break;
                    case 2: aa ^= (aa << 2); break;
                    case 3: aa ^= (aa >> 16); break;
                }
                aa = mm[(i + 128) % 256] + aa;
                uint y = mm[(x >> 2) % 256] + aa + bb;
                mm[i] = y;
                randrsl[i] = bb = mm[(y >> 10) % 256] + x;
            }
        }
    }
}
