using System.Collections.Generic;

namespace ParserSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // ==========Encode=========
            TPS.Encoder e = new TPS.Encoder();
            // Пример работы с Array8
            var array8 = e.CreateArrayFormat(TPS.TypeTags.SInt64,
                                                                  165463562, 3654374, 654765, 67);
            // Пример работы с tagged structure
            var tagged = e.CreateTaggedStructureFormat((1, TPS.TypeTags.Float32, 37.12f),
                                                       (2, TPS.TypeTags.SInt16, 112),
                                                       (4, TPS.TypeTags.Unicode16, "reqwewf"));
            // Пример работы с Structure
            var structure = e.CreateStructureFormat((TPS.TypeTags.Float64, 4324.42),
                                                    (TPS.TypeTags.Array8, array8), 
                                                    (TPS.TypeTags.Utf8, "dwqdqw"));

            List <(TPS.TypeTags, dynamic)> ll = new List<(TPS.TypeTags, dynamic)>()
            {
                (TPS.TypeTags.Array8, array8),
                (TPS.TypeTags.TaggedStructure8, tagged),
                (TPS.TypeTags.Structure8, structure)

            };
            e.Encode(ll.ToArray());

            // ==========Decode=========
            TPS.Decoder d = new TPS.Decoder();
            d.Decode(e.Result.ToArray());
            // Например нужно поле 4 из tagged структуры
            string s = d.Result[1][4];
            int i = 0;
        }
    }
}
