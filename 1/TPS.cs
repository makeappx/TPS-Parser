using System;
using System.Collections.Generic;

namespace TPS
{
    public enum TypeTags
    {
        Null = 0x00,
        Array8 = 0x01,
        Structure8 = 0x02,
        Bool = 0x03,
        BitString8 = 0x04,
        SInt32 = 0x05,
        UInt32 = 0x06,
        OctetString16 = 0x07,
        OctetString32 = 0x08,
        OctetString8 = 0x09,
        VisibleString8 = 0x0A,
        Array16 = 0x0B,
        Utf8 = 0x0C,
        Unicode16 = 0x0E,
        SInt8 = 0x0F,
        SInt16 = 0x10,
        UInt8 = 0x11,
        UInt16 = 0x12,
        Sequence32 = 0x13,
        SInt64 = 0x14,
        UInt64 = 0x15,
        Enum8 = 0x16,
        Float32 = 0x17,
        Float64 = 0x18,
        DateTime = 0x19,
        Date = 0x1A,
        Time = 0x1B,
        GUID = 0x1C,
        Header = 0x1D,
        StructureContext = 0x1E,
        Array32 = 0x1F,
        TaggedStructure8 = 0x21,
        Numeric18t = 0x22,
        HelloSignature = 0xFF,
        Hello = 0x30
    };
    public class Date_Time
    {
        public short Year;
        public byte Month;
        public byte Day;
        public byte Hour;
        public byte Minute;
        public byte Second;
        public short Milliseconds;
        public byte UTC;
    }
    public struct Numeric18t
    {
        public ulong Value;
        public byte Perc;
    }
    public struct GUID
    {
        public byte[] Data1;
        public byte[] Data2;
        public byte[] Data3;
        public byte[] Data4;
    }
    public struct Header
    {
        public byte FF;
        public uint SSSS;
        public uint BBBB;
        public uint TTTT;
    }
    public class Hello
    {
        public ulong Signature = 0xFFF77FFF;
        public ushort? ID = null;
        public ulong? Version = null;
        public byte? Protocol = null;
        public string IMEI = null;
        public string DeviceSerial = null;
        public string DeviceModName = null;
        public GUID? LicenseID = null;
        public GUID? HostID = null;
        public string HostName = null;
        public string Login = null;
        public string PswdHash = null;
        public string AsmblVersion = null;
        public List<byte> Encode()
        {
            Encoder e = new Encoder();
            List<(int, TypeTags, dynamic)> tmp = new List<(int, TypeTags, dynamic)>();
            if (ID != null) tmp.Add((1, TypeTags.UInt16, ID));
            if (Version != null) tmp.Add((2, TypeTags.UInt64, Version));
            if (Protocol != null) tmp.Add((3, TypeTags.Enum8, Protocol));
            if (IMEI != null) tmp.Add((4, TypeTags.VisibleString8, IMEI));
            if (DeviceSerial != null) tmp.Add((5, TypeTags.Unicode16, DeviceSerial));
            if (DeviceModName != null) tmp.Add((6, TypeTags.Unicode16, DeviceModName));
            if (LicenseID != null) tmp.Add((8, TypeTags.GUID, LicenseID));
            if (HostID != null) tmp.Add((9, TypeTags.GUID, HostID));
            if (HostName != null) tmp.Add((10, TypeTags.Unicode16, HostName));
            if (Login != null) tmp.Add((11, TypeTags.Unicode16, Login));
            if (PswdHash != null) tmp.Add((12, TypeTags.Unicode16, PswdHash));
            if (AsmblVersion != null) tmp.Add((13, TypeTags.Unicode16, AsmblVersion));
            List<(TypeTags, dynamic)> hello = new List<(TypeTags, dynamic)>()
            {
                (TypeTags.HelloSignature, default),
                (TypeTags.Hello, e.CreateTaggedStructureFormat(tmp.ToArray()))
            };
            return e.Encode(hello.ToArray());
        }
    }
    public class RunParams
    {
        public byte? GUIMode;
        public ushort? StepIndex;
        public ushort? ScriptIndex;
    }
    public class Result
    {
        public byte? ResultCode;
        public uint? InternalCode;
        public string ErrMes;
        public string ErrDetails;
    }
    public class JobParams
    {
        public ushort? PlaceNo;
        public string BC_Meter;
        public string BC_Modem;
        public string MeterSerial;
        public string ModemIMEI;
        public string Checker;
        public string ModemSerial;
        public string MeterAddr;
        public string MeterVer;
        public string MAC;
        public string SerialPortPlace;
        public string SerialPortRS485;
        public string SerialPortCSD;
        public string SerialPortZigBee;
        public string ZigBeeGateIMEI;
        public string IPHost_TPS;
        public ushort? IPPort_TPS;
        public string IPHost_ZigBeeGate;
        public ushort? IPPort_ZigBeeGate;
        public string IPHost_PLC;
        public string PLC;
        public ushort? IPPort_PL;
    }
    public class JobStep
    {
        public byte? StepType;
        public string Name;
        public string Details;
        public List<Script> ScriptList;
        public Result result;
        public List<string> StepLog;
    }
    public class Script
    {
        public string Name;
        public string Details;
        public List<ScriptСmd> CmdList;
    }
    public class ScriptСmd
    {
        public ushort? Tag;
        public string Details;
        public string Param;
        public string LN;
        public ushort? AttrMetNo;
        public ushort? DataIdx;
        public byte? Enable;
        public byte? StopIfError;
        public byte? Reserved;
        public byte? Verify;
        public ushort? SrcParamTag;
    }
    public class ElMeterJob //TODO найти где забыл форматировать тагед структуру.
    {
        public Date_Time DataCreate;
        public string Creator;
        public string Caption;
        public string Details;
        public RunParams runParams;
        public JobParams jobParams;
        public List<JobStep> StepList;
        public byte? State;
        public byte? CurStep;
        public byte? PlaceState;
        public List<string> JobLog;
        public List<byte> Encode()
        {
            Encoder e = new Encoder();
            var tmp = new List<(int, TypeTags, dynamic)>();
            var temp = new List<(int, TypeTags, dynamic)>() {(1, TypeTags.Enum8, runParams.GUIMode),
                                                             (2, TypeTags.UInt16, runParams.StepIndex),
                                                             (3, TypeTags.UInt16, runParams.ScriptIndex)};
            temp.ForEach(x => { if (x.Item3 == null) temp.Remove(x); });
            var rp = e.CreateTaggedStructureFormat(temp.ToArray());

            temp = new List<(int, TypeTags, dynamic)>() {(1, TypeTags.UInt16, jobParams.PlaceNo),
                                                         (2, TypeTags.Unicode16, jobParams.BC_Modem),
                                                         (3, TypeTags.Unicode16, jobParams.MeterSerial),
                                                         (4, TypeTags.Unicode16, jobParams.ModemIMEI),
                                                         (5, TypeTags.Unicode16, jobParams.Checker),
                                                         (6, TypeTags.Unicode16, jobParams.ModemSerial),
                                                         (7, TypeTags.Unicode16, jobParams.MeterAddr),
                                                         (8, TypeTags.Unicode16, jobParams.MeterVer),
                                                         (9, TypeTags.Unicode16, jobParams.MAC),
                                                         (10, TypeTags.Unicode16, jobParams.SerialPortPlace),
                                                         (11, TypeTags.Unicode16, jobParams.SerialPortRS485),
                                                         (12, TypeTags.Unicode16, jobParams.SerialPortCSD),
                                                         (13, TypeTags.Unicode16, jobParams.SerialPortZigBee),
                                                         (14, TypeTags.Unicode16, jobParams.ZigBeeGateIMEI),
                                                         (15, TypeTags.Unicode16, jobParams.IPHost_TPS),
                                                         (16, TypeTags.UInt16, jobParams.IPPort_TPS),
                                                         (17, TypeTags.Unicode16, jobParams.IPHost_ZigBeeGate),
                                                         (18, TypeTags.UInt16, jobParams.IPPort_ZigBeeGate),
                                                         (19, TypeTags.Unicode16, jobParams.IPHost_PLC),
                                                         (20, TypeTags.Unicode16, jobParams.PLC),
                                                         (21, TypeTags.UInt16, jobParams.IPPort_PL)};
            temp.ForEach(x => { if (x.Item3 == null) temp.Remove(x); });
            var jp = e.CreateTaggedStructureFormat(temp.ToArray());
            var kek = new List<dynamic>();
            foreach(var js in StepList)
            {
                var scl = new List<dynamic>();
                foreach (var sc in js.ScriptList)
                {
                    var tmp2 = new List<(int, TypeTags, dynamic)>() { (1, TypeTags.Unicode16, sc.Name),
                                                                      (2, TypeTags.Unicode16, sc.Details)};
                    var cmdl = new List<dynamic>();
                    foreach (var scmd in sc.CmdList)
                    {
                        var tmp3 = new List<(int, TypeTags, dynamic)>() {(1, TypeTags.UInt16, scmd.Tag),
                                                                         (2, TypeTags.Unicode16, scmd.Details),
                                                                         (3, TypeTags.Unicode16, scmd.Param),
                                                                         (4, TypeTags.Unicode16, scmd.LN),
                                                                         (5, TypeTags.UInt16, scmd.AttrMetNo),
                                                                         (6, TypeTags.UInt16, scmd.DataIdx),
                                                                         (7, TypeTags.Bool, scmd.Enable),
                                                                         (8, TypeTags.Bool, scmd.StopIfError),
                                                                         (9, TypeTags.Bool, scmd.Reserved),
                                                                         (10, TypeTags.Bool, scmd.Verify),
                                                                         (11, TypeTags.UInt16, scmd.SrcParamTag)};
                        tmp3.ForEach(x => { if (x.Item3 == null) temp.Remove(x); });

                        cmdl.Add(e.CreateTaggedStructureFormat(tmp3.ToArray()));
                    }
                    tmp2.Add((3, TypeTags.Array16, e.CreateArrayFormat(TypeTags.TaggedStructure8, cmdl.ToArray())));
                    tmp2.ForEach(x => { if (x.Item3 == null) temp.Remove(x); });

                    scl.Add(e.CreateContextStructureFormat(112, tmp2.ToArray()));
                }
                var rtmp = new List<(int, TypeTags, dynamic)>() { (1, TypeTags.Enum8, js.result.ResultCode),
                                                                  (2, TypeTags.UInt32, js.result.InternalCode),
                                                                  (3, TypeTags.Unicode16, js.result.ErrMes),
                                                                  (4, TypeTags.Unicode16, js.result.ErrDetails)};
                rtmp.ForEach(x => { if (x.Item3 == null) temp.Remove(x); });
                var tmp1 = new List<(int, TypeTags, dynamic)>() { (1, TypeTags.Enum8, js.StepType),
                                                                  (2, TypeTags.Unicode16, js.Name),
                                                                  (3, TypeTags.Unicode16, js.Details),
                                                                  (4, TypeTags.Array16, e.CreateArrayFormat(TypeTags.StructureContext, scl.ToArray())),
                                                                  (5, TypeTags.TaggedStructure8, e.CreateTaggedStructureFormat(rtmp.ToArray())),
                                                                  (6, TypeTags.Array16, e.CreateArrayFormat(TypeTags.Unicode16, js.StepLog.ToArray()))};
                tmp1.ForEach(x => { if (x.Item3 == null) temp.Remove(x); });
                kek.Add(e.CreateTaggedStructureFormat(tmp1.ToArray()));
            }
            var jobst = e.CreateArrayFormat(TypeTags.TaggedStructure8, kek.ToArray());
            var log = e.CreateArrayFormat(TypeTags.Unicode16, JobLog.ToArray());
            if (DataCreate != null) tmp.Add((1, TypeTags.DateTime, DataCreate));
            if (Creator != null) tmp.Add((2, TypeTags.Unicode16, Creator));
            if (Caption != null) tmp.Add((3, TypeTags.Unicode16, Caption));
            if (Details != null) tmp.Add((4, TypeTags.Unicode16, Details));
            if (runParams != null) tmp.Add((5, TypeTags.TaggedStructure8, rp));
            if (jobParams != null) tmp.Add((6, TypeTags.TaggedStructure8, jp));
            if (jobst != null) tmp.Add((7, TypeTags.Array16, jobst));
            if (State != null) tmp.Add((8, TypeTags.Enum8, State));
            if (CurStep != null) tmp.Add((9, TypeTags.UInt8, CurStep));
            if (PlaceState != null) tmp.Add((10, TypeTags.Enum8, PlaceState));
            if (log != null) tmp.Add((11, TypeTags.Array16, log));

            return e.Encode((TypeTags.TaggedStructure8, e.CreateTaggedStructureFormat(tmp.ToArray())));
        }
    }
    /// <summary>
    /// Encoder
    /// </summary>
    public class Encoder
    {
        public Encoder()
        {
            Result = new List<byte>();
        }

        /// <summary>
        /// Encoded bytes
        /// </summary>
        public List<byte> Result;

        /// <summary>
        /// Encode values
        /// </summary>
        /// <param name="args">Values to be encoded</param>
        /// <returns>List of encoded bytes</returns>
        public List<byte> Encode(params (TypeTags, dynamic)[] args)
        {
            return Result = Enc(args);
        }
        private List<byte> Enc((TypeTags, dynamic)[] args, bool tag = true)
        {
            List<byte> encoded = new List<byte>();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Item1 == TypeTags.Null) { encoded.Add((byte)args[i].Item2); continue; }
                else if (tag) encoded.Add((byte)args[i].Item1);

                switch (args[i].Item1)
                {
                    case TypeTags.UInt32:
                        {
                            byte[] tmp = BitConverter.GetBytes((uint)args[i].Item2);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            break;
                        }
                    case TypeTags.UInt64:
                        {
                            byte[] tmp = BitConverter.GetBytes((ulong)args[i].Item2);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            break;
                        }
                    case TypeTags.SInt64:
                        {
                            byte[] tmp = BitConverter.GetBytes((long)args[i].Item2);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            break;
                        }
                    case TypeTags.SInt32:
                        {
                            byte[] tmp = BitConverter.GetBytes((int)args[i].Item2);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            break;
                        }
                    case TypeTags.UInt16:
                        {
                            byte[] tmp = BitConverter.GetBytes((ushort)args[i].Item2);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            break;
                        }
                    case TypeTags.Bool:
                    case TypeTags.UInt8:
                    case TypeTags.Enum8:
                    case TypeTags.SInt8:
                        {
                            encoded.Add((byte)args[i].Item2);
                            break;
                        }
                    case TypeTags.SInt16:
                        {
                            byte[] tmp = BitConverter.GetBytes((short)args[i].Item2);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            break;
                        }
                    case TypeTags.Float32:
                        {
                            byte[] tmp = BitConverter.GetBytes((float)args[i].Item2);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            break;
                        }
                    case TypeTags.Float64:
                        {
                            byte[] tmp = BitConverter.GetBytes((double)args[i].Item2);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            break;
                        }
                    case TypeTags.BitString8:
                    case TypeTags.VisibleString8:
                    case TypeTags.OctetString8:
                        {
                            encoded.Add((byte)args[i].Item2.Length);
                            foreach (byte b in args[i].Item2)
                                encoded.Add(b);
                            break;
                        }
                    case TypeTags.OctetString16:
                    case TypeTags.Utf8:
                        {
                            byte[] tmp = BitConverter.GetBytes((ushort)args[i].Item2.Length);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            foreach (byte b in args[i].Item2)
                                encoded.Add(b);
                            break;
                        }
                    case TypeTags.OctetString32:
                        {
                            byte[] tmp = BitConverter.GetBytes((uint)args[i].Item2.Length);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            foreach (byte b in args[i].Item2)
                                encoded.Add(b);
                            break;
                        }
                    case TypeTags.Unicode16:
                        {
                            byte[] tmp = BitConverter.GetBytes((uint)args[i].Item2.Length);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            foreach (char c in args[i].Item2)
                            {
                                tmp = BitConverter.GetBytes(c);
                                Array.Reverse(tmp);
                                encoded.AddRange(tmp);
                            }
                            break;
                        }
                    case TypeTags.DateTime:
                        {
                            byte[] tmp = BitConverter.GetBytes((ushort)args[i].Item2.Year);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            encoded.Add((byte)args[i].Item2.Month);
                            encoded.Add((byte)args[i].Item2.Day);
                            encoded.Add((byte)args[i].Item2.Hour);
                            encoded.Add((byte)args[i].Item2.Minute);
                            encoded.Add((byte)args[i].Item2.Second);
                            encoded.Add((byte)args[i].Item2.Milliseconds);
                            encoded.Add((byte)args[i].Item2.UTC);
                            break;
                        }
                    case TypeTags.Date:
                        {
                            byte[] tmp = BitConverter.GetBytes((ushort)args[i].Item2);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            encoded.Add((byte)args[i].Item2.Month);
                            encoded.Add((byte)args[i].Item2.Day);
                            encoded.Add((byte)args[i].Item2.UTC);
                            break;
                        }
                    case TypeTags.Time:
                        {
                            encoded.Add((byte)args[i].Item2.Hour);
                            encoded.Add((byte)args[i].Item2.Minute);
                            encoded.Add((byte)args[i].Item2.Second);
                            encoded.Add((byte)args[i].Item2.Milliseconds);
                            encoded.Add((byte)args[i].Item2.UTC);
                            break;
                        }
                    case TypeTags.Array8:
                        {
                            encoded.Add((byte)args[i].Item2.Length);
                            if (args[i].Item2[0].Item1 == TypeTags.Null)
                            {
                                encoded.Add((byte)args[i].Item2[0].Item2);
                                break;
                            }
                            encoded.Add((byte)args[i].Item2[0].Item1);
                            encoded.AddRange(Enc(args[i].Item2, false));
                            break;
                        }
                    case TypeTags.Array16:
                        {
                            byte[] tmp = BitConverter.GetBytes((ushort)args[i].Item2.Length);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            if (args[i].Item2[0].Item1 == TypeTags.Null)
                            {
                                encoded.Add((byte)args[i].Item2[0].Item2);
                                break;
                            }
                            encoded.Add((byte)args[i].Item2[0].Item1);
                            encoded.AddRange(Enc(args[i].Item2, false));
                            break;
                        }
                    case TypeTags.Array32:
                        {
                            byte[] tmp = BitConverter.GetBytes((uint)args[i].Item2.Length);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            if (args[i].Item2[0].Item1 == TypeTags.Null)
                            {
                                encoded.Add((byte)args[i].Item2[0].Item2);
                                break;
                            }
                            encoded.Add((byte)args[i].Item2[0].Item1);
                            encoded.AddRange(Enc(args[i].Item2, false));
                            break;
                        }
                    case TypeTags.Sequence32:
                        {
                            byte[] tmp = BitConverter.GetBytes((uint)args[i].Item2.Length);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            encoded.AddRange(Enc(args[i].Item2));
                            break;
                        }
                    case TypeTags.Numeric18t:
                        {
                            encoded.Add((byte)args[i].Item2.Perc);
                            byte[] tmp = BitConverter.GetBytes((ulong)args[i].Item2);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            break;
                        }
                    case TypeTags.GUID:
                        {
                            encoded.AddRange(BitConverter.GetBytes((uint)args[i].Item2.Data1));
                            encoded.AddRange(BitConverter.GetBytes((ushort)args[i].Item2.Data2));
                            encoded.AddRange(BitConverter.GetBytes((ushort)args[i].Item2.Data3));
                            byte[] tmp = BitConverter.GetBytes((ulong)args[i].Item2.Data4);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            break;
                        }
                    case TypeTags.Header:
                        {
                            encoded.Add((byte)args[i].Item2.FF);
                            byte[] tmp = BitConverter.GetBytes((uint)args[i].Item2.SSSS);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            tmp = BitConverter.GetBytes((uint)args[i].Item2.BBBB);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            tmp = BitConverter.GetBytes((uint)args[i].Item2.TTTT);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            break;
                        }
                    case TypeTags.StructureContext:
                        {
                            byte[] tmp = BitConverter.GetBytes((ushort)args[i].Item2[0].Item2);
                            Array.Reverse(tmp);
                            encoded.AddRange(tmp);
                            encoded.Add((byte)((args[i].Item2.Length - 1) / 2));

                            var t = new List<(TypeTags, dynamic)>(args[i].Item2);
                            t.RemoveAt(0);
                            encoded.AddRange(Enc(t.ToArray()));//Ошибка здесь
                            break;
                        }
                    case TypeTags.Structure8:
                        {
                            encoded.Add((byte)args[i].Item2.Length);
                            encoded.AddRange(Enc(args[i].Item2));
                            break;
                        }
                    case TypeTags.HelloSignature:
                        {
                            encoded.AddRange(new byte[] { 0xFF, 0xF7, 0x7F, 0xFF });
                            break;
                        }
                    default://any other structure
                        {
                            encoded.Add((byte)(args[i].Item2.Length / 2));
                            encoded.AddRange(Enc(args[i].Item2));
                            break;
                        }
                }
            }
            return encoded;
        }

        /// <summary>
        /// Create array{8, 16, 32} in defined encoder format
        /// </summary>
        /// <param name="elemTag">Tag of elements</param>
        /// <param name="args">Elements</param>
        /// <returns>Defined encoder format array</returns>
        public (TypeTags, dynamic)[] CreateArrayFormat(TypeTags elemTag, params dynamic[] args)
        {
            List<(TypeTags, dynamic)> res = new List<(TypeTags, dynamic)>();

            if (args.Length == 0)
                res.Add((TypeTags.Null, elemTag));
            else
                for (int i = 0; i < args.Length; i++)
                    res.Add((elemTag, args[i]));

            return res.ToArray();
        }

        /// <summary>
        /// Create Structure8 or Sequence32 in defined encoder format
        /// </summary>
        /// <param name="args">Elements (Tag of type, value)</param>
        /// <returns>Defined encoder format array</returns>
        public (TypeTags, dynamic)[] CreateStructureFormat(params (TypeTags, dynamic)[] args)
        {
            List<(TypeTags, dynamic)> res = new List<(TypeTags, dynamic)>();
            for (int i = 0; i < args.Length; i++)
                res.Add(args[i]);
            return res.ToArray();
        }

        /// <summary>
        /// Create Tagged or any other structure with tags of fields in defined encoder format
        /// </summary>
        /// <param name="args">Elements (number of field, tag of element, value)</param>
        /// <returns>Defined encoder format array</returns>
        public (TypeTags, dynamic)[] CreateTaggedStructureFormat(params (int numOfField, TypeTags tagOfElem, dynamic elem)[] args)
        {
            List<(TypeTags, dynamic)> res = new List<(TypeTags, dynamic)>();
            for (int i = 0; i < args.Length; i++)
            {
                res.Add((TypeTags.Null, args[i].numOfField));
                res.Add((args[i].tagOfElem, args[i].elem));
            }
            return res.ToArray();
        }

        /// <summary>
        /// Create ContextStructure with tags of fields in defined encoder format
        /// </summary>
        /// <param name="context">Context identifier</param>
        /// <param name="args">Elements (number of field, tag of element, value)</param>
        /// <returns>Defined encoder format array</returns>
        public (TypeTags, dynamic)[] CreateContextStructureFormat(ushort context, params (int numOfField, TypeTags tagOfElem, dynamic elem)[] args)
        {
            List<(TypeTags, dynamic)> res = new List<(TypeTags, dynamic)>();
            res.Add((TypeTags.Null, context));
            for (int i = 0; i < args.Length; i++)
            {
                res.Add((TypeTags.Null, args[i].numOfField));
                res.Add((args[i].tagOfElem, args[i].elem));
            }
            return res.ToArray();
        }
    }

    /// <summary>
    /// Decoder
    /// </summary>
    class Decoder
    {
        public Decoder()
        {
            i = 0;
            Result = new List<(TypeTags, dynamic)>();
        }
        private int i;

        /// <summary>
        /// Decoded values
        /// </summary>
        public List<(TypeTags, dynamic)> Result;

        /// <summary>
        /// Decode values
        /// </summary>
        /// <param name="arr">Bytes to decode</param>
        /// <param name="count">Count of decoding values(0 = all)</param>
        /// <returns>List of decoded values</returns>
        public List<(TypeTags, dynamic)> Decode(byte[] arr, int count = 0)
        {
            i = 0;
            return Result = Dec(arr, count);
        }
        private List<(TypeTags, dynamic)> Dec(byte[] arr, int count, bool str = false, int tag = 0)
        {
            List<(TypeTags, dynamic)> parsed = new List<(TypeTags, dynamic)>();
            for (int arrsize = arr.Length; i + (str ? 1 : 0) < arrsize; i += tag != 0 ? 0 : 1)
            {
                if (str) parsed.Add((TypeTags.Null, arr[i++]));
                try
                {
                    int _tag = tag != 0 ? tag : arr[i];
                    switch ((TypeTags)_tag)
                    {
                        case TypeTags.Null:
                            {
                                parsed.Add((TypeTags.Null, 0));
                                break;
                            }
                        case TypeTags.UInt32:
                            {
                                uint tmp = 0;
                                for (int j = ++i; j < i + 4 && j < arrsize; j++)
                                    tmp += (uint)arr[j] << 8 * (i + 3 - j); i += 3;

                                parsed.Add((TypeTags.UInt32, tmp));
                                break;
                            }
                        case TypeTags.UInt64:
                            {
                                ulong tmp = 0;
                                for (int j = ++i; j < i + 8 && j < arrsize; j++)
                                    tmp += (ulong)arr[j] << 8 * (i + 7 - j); i += 7;

                                parsed.Add((TypeTags.UInt64, tmp));
                                break;
                            }
                        case TypeTags.SInt64:
                            {
                                long tmp = 0;
                                for (int j = ++i; j < i + 8 && j < arrsize; j++)
                                    tmp += arr[j] << 8 * (i + 7 - j); i += 7;

                                parsed.Add((TypeTags.SInt64, tmp));
                                break;
                            }
                        case TypeTags.SInt32:
                            {
                                int tmp = 0;
                                for (int j = ++i; j < i + 4 && j < arrsize; j++)
                                    tmp += arr[j] << 8 * (i + 3 - j); i += 3;

                                parsed.Add((TypeTags.SInt32, tmp));
                                break;
                            }
                        case TypeTags.UInt16:
                            {
                                ushort tmp = 0;
                                for (int j = ++i; j < i + 2 && j < arrsize; j++)
                                    tmp += (ushort)(arr[j] << 8 * (i + 1 - j)); i++;

                                parsed.Add((TypeTags.UInt16, tmp));
                                break;
                            }
                        case TypeTags.UInt8:
                            {
                                byte tmp = arr[++i];

                                parsed.Add((TypeTags.UInt8, tmp));
                                break;
                            }
                        case TypeTags.SInt16:
                            {
                                short tmp = 0;
                                for (int j = ++i; j < i + 2 && j < arrsize; j++)
                                    tmp += (short)(arr[j] << 8 * (i + 1 - j)); i++;

                                parsed.Add((TypeTags.SInt16, tmp));
                                break;
                            }
                        case TypeTags.SInt8:
                            {
                                short tmp = arr[++i];

                                parsed.Add((TypeTags.SInt8, tmp));
                                break;
                            }
                        case TypeTags.Bool:
                            {
                                byte k = arr[++i];

                                parsed.Add((TypeTags.Bool, k));
                                break;
                            }
                        case TypeTags.OctetString8:
                            {
                                byte size = arr[++i];
                                List<byte> tmp = new List<byte>();
                                for (int j = 0; j < size; j++)
                                    tmp.Add(arr[++i]);

                                parsed.Add((TypeTags.OctetString8, tmp));
                                break;
                            }
                        case TypeTags.BitString8:
                            {
                                byte size = arr[++i];
                                List<byte> tmp = new List<byte>();
                                for (int j = 0; j < size; j++)
                                    tmp.Add(arr[++i]);

                                parsed.Add((TypeTags.BitString8, tmp));
                                break;
                            }
                        case TypeTags.OctetString16:
                            {
                                List<byte> tmp = new List<byte>();
                                int size = 0;
                                for (int j = ++i; j < i + 2 && j < arrsize; j++)
                                    size += (arr[j] << 8 * (i + 1 - j)); i++;
                                for (int j = ++i; j < i + size && j < arrsize; j++)
                                    tmp.Add(arr[j]); i += size - 1;

                                parsed.Add((TypeTags.OctetString16, tmp));
                                break;
                            }
                        case TypeTags.OctetString32:
                            {
                                List<byte> tmp = new List<byte>();
                                int size = 0;
                                for (int j = ++i; j < i + 4 && j < arrsize; j++)
                                    size += arr[j] << 8 * (i + 3 - j); i += 3;
                                for (int j = ++i; j < i + size && j < arrsize; j++)
                                    tmp.Add(arr[j]); i += size - 1;

                                parsed.Add((TypeTags.OctetString32, tmp));
                                break;
                            }
                        case TypeTags.Utf8:
                            {
                                int size = (++i < arr.Length ? arr[i] << 8 : 0) + arr[++i];
                                string k = "";
                                for (int j = ++i; j < i + size; j++)
                                    k += (char)arr[j];
                                i += size - 1;

                                parsed.Add((TypeTags.Utf8, k));
                                break;
                            }
                        case TypeTags.Unicode16:
                            {
                                string k = "";
                                int size = 0;
                                for (int j = ++i; j < i + 4 && j < arrsize; j++)
                                    size += arr[j] << 8 * (i + 3 - j); i += 3;
                                for (int j = ++i; j < i + size * 2; j++)
                                    k += (char)((arr[j++] << 8) + arr[j]);
                                i += size * 2 - 1;

                                parsed.Add((TypeTags.Unicode16, k));
                                break;
                            }
                        case TypeTags.Enum8:
                            {
                                short k = arr[++i];

                                parsed.Add((TypeTags.Enum8, k));
                                break;
                            }
                        case TypeTags.DateTime:
                            {
                                Date_Time d = new Date_Time();
                                d.Year = 0;
                                for (int j = ++i; j < i + 2 && j < arrsize; j++)
                                    d.Year += (short)(arr[j] << 8 * (i + 1 - j)); i++;
                                d.Month = arr[++i];
                                d.Day = arr[++i];
                                d.Hour = arr[++i];
                                d.Minute = arr[++i];
                                d.Second = arr[++i];
                                d.Milliseconds = arr[++i];
                                d.UTC = arr[++i];
                                parsed.Add((TypeTags.DateTime, d));
                                break;
                            }
                        case TypeTags.Date:
                            {
                                Date_Time d = new Date_Time();
                                d.Year = 0;
                                for (int j = ++i; j < i + 2 && j < arrsize; j++)
                                    d.Year += (short)(arr[j] << 8 * (i + 1 - j)); i++;
                                d.Month = arr[++i];
                                d.Day = arr[++i];
                                d.UTC = arr[++i];
                                parsed.Add((TypeTags.Date, d));
                                break;
                            }
                        case TypeTags.Time:
                            {
                                Date_Time d = new Date_Time
                                {
                                    Hour = arr[++i],
                                    Minute = arr[++i],
                                    Second = arr[++i],
                                    Milliseconds = arr[++i]
                                };
                                parsed.Add((TypeTags.Time, d));
                                break;
                            }
                        case TypeTags.VisibleString8:
                            {
                                byte size = arr[++i];
                                string tmp = "";
                                for (int j = 0; j < size; j++)
                                    tmp += arr[++i];

                                parsed.Add((TypeTags.VisibleString8, tmp));
                                break;
                            }
                        case TypeTags.Array8:
                            {
                                int size = arr[++i];
                                int temptag = arr[++i];
                                parsed.Add((TypeTags.Array8, Dec(arr, size, false, temptag)));
                                break;
                            }
                        case TypeTags.Array16:
                            {
                                int size = 0;
                                for (int j = ++i; j < i + 2 && j < arrsize; j++)
                                    size += (arr[j] << 8 * (i + 1 - j)); i++;

                                int temptag = arr[++i];
                                parsed.Add((TypeTags.Array16, Dec(arr, size, false, temptag)));
                                break;
                            }
                        case TypeTags.Array32:
                            {
                                int size = 0;
                                for (int j = ++i; j < i + 4 && j < arrsize; j++)
                                    size += arr[j] << 8 * (i + 3 - j); i += 3;

                                int temptag = arr[++i];
                                parsed.Add((TypeTags.Array32, Dec(arr, size, false, temptag)));
                                break;
                            }
                        case TypeTags.Sequence32:
                            {
                                int size = 0;
                                for (int j = ++i; j < i + 4 && j < arrsize; j++)
                                    size += arr[j] << 8 * (i + 3 - j); i += 3;
                                parsed.Add((TypeTags.Array32, Dec(arr, size)));
                                break;
                            }
                        case TypeTags.Structure8:
                            {
                                i++;
                                parsed.Add((TypeTags.Structure8, Dec(arr, arr[i++])));
                                break;
                            }
                        case TypeTags.Float32:
                            {
                                byte[] temparr = new byte[4];
                                for (int j = 0; j < 4; j++)
                                    temparr[j] = arr[++i];
                                Array.Reverse(temparr);
                                parsed.Add((TypeTags.Float32, BitConverter.ToSingle(temparr, 0)));
                                break;
                            }
                        case TypeTags.Float64:
                            {
                                byte[] temparr = new byte[8];
                                for (int j = 0; j < 8; j++)
                                    temparr[j] = arr[++i];
                                Array.Reverse(temparr);
                                parsed.Add((TypeTags.Float64, BitConverter.ToDouble(temparr, 0)));
                                break;
                            }
                        case TypeTags.Numeric18t:
                            {
                                Numeric18t n;
                                n.Perc = arr[++i];
                                n.Value = 0;
                                for (int j = ++i; j < i + 8 && j < arrsize; j++)
                                    n.Value += (ulong)arr[j] << 8 * (i + 7 - j); i += 7;
                                parsed.Add((TypeTags.Numeric18t, n));
                                break;
                            }
                        case TypeTags.GUID:
                            {
                                GUID g;
                                g.Data1 = new byte[4];
                                g.Data2 = new byte[2];
                                g.Data3 = new byte[2];
                                g.Data4 = new byte[8];
                                for (int j = 3; j >= 0; j--)
                                    g.Data1[j] = arr[++i];
                                for (int j = 1; j >= 0; j--)
                                    g.Data2[j] = arr[++i];
                                for (int j = 1; j >= 0; j--)
                                    g.Data3[j] = arr[++i];
                                for (int j = 0; j < 8; j++)
                                    g.Data4[j] = arr[++i];
                                parsed.Add((TypeTags.GUID, g));
                                break;
                            }
                        case TypeTags.Header:
                            {
                                Header h;
                                h.FF = arr[++i];
                                h.SSSS = 0;
                                h.BBBB = 0;
                                h.TTTT = 0;
                                for (int j = ++i; j < i + 4 && j < arrsize; j++)
                                    h.SSSS += (uint)arr[j] << 8 * (i + 3 - j); i += 3;
                                for (int j = ++i; j < i + 4 && j < arrsize; j++)
                                    h.BBBB += (uint)arr[j] << 8 * (i + 3 - j); i += 3;
                                for (int j = ++i; j < i + 4 && j < arrsize; j++)
                                    h.TTTT += (uint)arr[j] << 8 * (i + 3 - j); i += 3;
                                parsed.Add((TypeTags.Header, h));
                                break;
                            }
                        case TypeTags.HelloSignature:
                            {
                                i += 4;
                                parsed.Add((TypeTags.HelloSignature, default));
                                break;
                            }
                        case TypeTags.StructureContext:
                            {
                                i+=3;
                                Dictionary<int, (TypeTags, dynamic)> tempdic = new Dictionary<int, (TypeTags, dynamic)>();
                                List<(TypeTags, dynamic)> temp = Dec(arr, arr[i++], true);
                                for (int j = 0; j < temp.Count - 1; j += 2)
                                    tempdic[temp[j].Item2] = temp[j + 1];

                                parsed.Add((TypeTags.StructureContext, tempdic));
                                break;
                            }
                        default://any tagged structure
                            {
                                i++;
                                Dictionary<int, (TypeTags, dynamic)> tempdic = new Dictionary<int, (TypeTags, dynamic)>();
                                List<(TypeTags, dynamic)> temp = Dec(arr, arr[i++], true);
                                for (int j = 0; j < temp.Count - 1; j += 2)
                                    tempdic[temp[j].Item2] = temp[j + 1];

                                parsed.Add(((TypeTags)_tag, tempdic));
                                break;
                            }
                    }
                }
                catch { return parsed; }
                if (parsed.Count / (str ? 2 : 1) == count) break;
            }
            return parsed;
        }
    }
}