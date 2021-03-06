namespace Assets.Minamo.Editor {
    struct AssignableType<T> {
        public readonly T Value;
        public readonly bool Flag;

        private AssignableType(T value, bool flag) {
            this.Value = value;
            this.Flag = flag;
        }

        public static AssignableType<T> FromDict(AnyDictionary dict, string key, T defaultVal = default(T)) {
            T val;
            bool flag = dict.TryGetValue<T>(key, out val, defaultVal);
            return new AssignableType<T>(val, flag);
        }

        public static AssignableType<T> FromEnumDict(AnyDictionary dict, string key, T defaultVal = default(T)) {
            var str = dict.GetValue<string>(key);
            if (str != null) {
                var d = StringEnumConverter.Get<T>();
                return AssignableType<T>.Create(d[str]);
            }
            // else..
            return new AssignableType<T>(defaultVal, false);
        }

        public static AssignableType<T> Create(T val) {
            return new AssignableType<T>(val, true);
        }

        public override string ToString() {
            return Value.ToString();
        }

        public static implicit operator T(AssignableType<T> v) {
            return v.Value;
        }
    }
}
