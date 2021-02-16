using System.Collections.Generic;
using System.IO;
using TPS;

namespace ParserSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            test();
            // ==========Encode=========
            TPS.Encoder e = new TPS.Encoder();

            // ========Пример Hello=======
            TPS.Hello hello = new TPS.Hello();
            hello.ID = 123;
            hello.Login = "123";
            hello.PswdHash = "test";
            var res = hello.Encode();
            // ===========================

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

            var ll = new List<(TPS.TypeTags, dynamic)>() // В таком формате задаются исходные данные
            {
                (TPS.TypeTags.Array8, array8),
                (TPS.TypeTags.TaggedStructure8, tagged),
                (TPS.TypeTags.Structure8, structure)
            };
            e.Encode(ll.ToArray()); // Функция кодирует исходные данные в список байт
       
            // ==========Decode=========
            TPS.Decoder d = new TPS.Decoder();
            d.Decode(e.Result.ToArray()); // Раскодирование массива байт и помещения в Result списка с полученными данными
            // Например нужно поле 4 из tagged структуры
            string s = d.Result[1].Item2[4].Item2;
            var dec = d.Decode(File.ReadAllBytes("kek"));
            int i = 9;
        }
        static void test()
        {
            RunParams rp = new RunParams() { GUIMode = 1, ScriptIndex = 228, StepIndex = 228 };
            Result r = new Result() { ErrDetails = "test", ErrMes = "test", InternalCode = 228, ResultCode = 228 };
            JobParams jp = new JobParams()
            {
                BC_Meter = "test",
                BC_Modem = "test",
                Checker = "test",
                IPHost_PLC = "test",
                IPHost_TPS = "test",
                IPHost_ZigBeeGate = "test",
                IPPort_PL = 228,
                IPPort_TPS = 228,
                IPPort_ZigBeeGate = 228,
                MAC = "test",
                MeterAddr = "test",
                MeterSerial = "test",
                MeterVer = "test",
                ModemIMEI = "test",
                ModemSerial = "test",
                PlaceNo = 234,
                PLC = "test",
                SerialPortCSD = "test",
                SerialPortPlace = "test",
                SerialPortRS485 = "test",
                SerialPortZigBee = "test",
                ZigBeeGateIMEI = "test"
            };
            ScriptСmd sc = new ScriptСmd()
            {
                AttrMetNo = 228,
                DataIdx = 228,
                Details = "test",
                Enable = 228,
                LN = "test",
                Param = "test",
                Reserved = 228,
                SrcParamTag = 228,
                StopIfError = 228,
                Tag = 228,
                Verify = 228
            };
            Script scr = new Script() { CmdList = new List<ScriptСmd>() { sc, sc, sc }, Details = "test", Name = "test" };
            JobStep js = new JobStep()
            {
                Details = "test",
                Name = "test",
                result = r,
                ScriptList = new List<Script>() { scr, scr, scr },
                StepLog = new List<string>() { "test", "test", "test" },
                StepType = 228
            };
            ElMeterJob ej = new ElMeterJob()
            {
                DataCreate = new Date_Time()
                {
                    Day = 12,
                    Hour = 11,
                    Milliseconds = 98,
                    Minute = 50,
                    Month = 11,
                    UTC = 3,
                    Second = 30,
                    Year = 2021
                },
                Caption = "test",
                Creator = "test",
                Details = "test",
                CurStep = 228,
                JobLog = new List<string>() { "test", "test", "test" },
                jobParams = jp,
                PlaceState = 228,
                runParams = rp,
                State = 228,
                StepList = new List<JobStep>() { js, js, js }
            };
            File.WriteAllBytes("kek", ej.Encode().ToArray());
        }
    }
}
