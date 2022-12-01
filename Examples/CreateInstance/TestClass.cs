namespace CreateInstance
{
    public class TestClass
    {
        public int? V1 { get; set; }
        public string? S1 { get; set; }
        public int? V2 { get; set; }
        public string? S2 { get; set; }
        public float? F1 { get; set; }

        public TestClass(int v1, string s1, int v2 = 5, string? s2 = null)
        {
            V1 = v1;
            S1 = s1;
            V2 = v2;
            S2 = s2;
        }

        public TestClass(int v1, string s1)
        {
            V1 = v1;
            S1 = s1;
        }

        public TestClass(int v1, string s1, int? v2 = null, float? f1 = 17)
        {
            V1 = v1;
            S1 = s1;
            V2 = v2;
            F1 = f1;
        }
    }
}
