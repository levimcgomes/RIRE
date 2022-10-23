namespace RIRE
{
    public readonly struct Color
    {
        private readonly float _r;
        private readonly float _g;
        private readonly float _b;

        public float r => _r;
        public float g => _g;
        public float b => _b;

        public Color(float r = 0, float g = 0, float b = 0) {
            this._r = r;
            this._g = g;
            this._b = b;
        }
    }
}
