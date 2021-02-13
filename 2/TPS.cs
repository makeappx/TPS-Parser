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
	};
	public struct Date_Time
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
							byte[] tmp = BitConverter.GetBytes((ushort)args[i].Item2);
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
							byte[] tmp = BitConverter.GetBytes((ushort)args[i].Item2);
							Array.Reverse(tmp);
							encoded.Add((byte)(args[i].Item2.Length / 2));
							encoded.AddRange(Enc(args[i].Item2));
							break;
						}
					case TypeTags.Structure8:
						{
							encoded.Add((byte)args[i].Item2.Length);
							encoded.AddRange(Enc(args[i].Item2));
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
	}

    /// <summary>
    /// Decoder
    /// </summary>
    class Decoder
	{
		public Decoder()
		{
			i = 0;
			Result = new List<dynamic>();
		}
		private int i;

		/// <summary>
		/// Decoded values
		/// </summary>
		public List<dynamic> Result;

		/// <summary>
		/// Decode values
		/// </summary>
		/// <param name="arr">Bytes to decode</param>
		/// <param name="count">Count of decoding values(0 = all)</param>
		/// <returns>List of decoded values</returns>
		public List<dynamic> Decode(byte[] arr, int count = 0) 
        {
			i = 0;
			return Result = Dec(arr, count);
        }
		private List<dynamic> Dec(byte[] arr, int count, bool str = false, int tag = 0)
        {
			List<dynamic> parsed = new List<dynamic>();
			for (int arrsize = arr.Length; i + (str ? 1 : 0) < arrsize; i += tag != 0 ? 0 : 1)
			{
				if (str) parsed.Add(arr[i++]);
				try
				{
					switch (tag != 0 ? (TypeTags)tag : (TypeTags)arr[i])
					{
						case TypeTags.Null:
							{
								parsed.Add(0);
								break;
							}
						case TypeTags.UInt32:
							{
								uint tmp = 0;
								for (int j = ++i; j < i + 4 && j < arrsize; j++)
									tmp += (uint)arr[j] << 8 * (i + 3 - j); i += 3;

								parsed.Add(tmp);
								break;
							}
						case TypeTags.UInt64:
							{
								ulong tmp = 0;
								for (int j = ++i; j < i + 8 && j < arrsize; j++)
									tmp += (ulong)arr[j] << 8 * (i + 7 - j); i += 7;

								parsed.Add(tmp);
								break;
							}
						case TypeTags.SInt64:
							{
								long tmp = 0;
								for (int j = ++i; j < i + 8 && j < arrsize; j++)
									tmp += arr[j] << 8 * (i + 7 - j); i += 7;

								parsed.Add(tmp);
								break;
							}
						case TypeTags.SInt32:
							{
								int tmp = 0;
								for (int j = ++i; j < i + 4 && j < arrsize; j++)
									tmp += arr[j] << 8 * (i + 3 - j); i += 3;

								parsed.Add(tmp);
								break;
							}
						case TypeTags.UInt16:
							{
								ushort tmp = 0;
								for (int j = ++i; j < i + 2 && j < arrsize; j++)
									tmp += (ushort)(arr[j] << 8 * (i + 1 - j)); i++;

								parsed.Add(tmp);
								break;
							}
						case TypeTags.UInt8:
							{
								byte tmp = arr[++i];

								parsed.Add(tmp);
								break;
							}
						case TypeTags.SInt16:
							{
								short tmp = 0;
								for (int j = ++i; j < i + 2 && j < arrsize; j++)
									tmp += (short)(arr[j] << 8 * (i + 1 - j)); i++;

								parsed.Add(tmp);
								break;
							}
						case TypeTags.SInt8:
							{
								short tmp = arr[++i];

								parsed.Add(tmp);
								break;
							}
						case TypeTags.Bool:
							{
								byte k = arr[++i];

								parsed.Add(k);
								break;
							}
						case TypeTags.OctetString8:
							{
								byte size = arr[++i];
								List<byte> tmp = new List<byte>();
								for (int j = 0; j < size; j++)
									tmp.Add(arr[++i]);

								parsed.Add(tmp);
								break;
							}
						case TypeTags.BitString8:
							{
								byte size = arr[++i];
								List<byte> tmp = new List<byte>();
								for (int j = 0; j < size; j++)
									tmp.Add(arr[++i]);

								parsed.Add(tmp);
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

								parsed.Add(tmp);
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

								parsed.Add(tmp);
								break;
							}
						case TypeTags.Utf8:
							{
								int size = (++i < arr.Length ? arr[i] << 8 : 0) + arr[++i];
								string k = "";
								for (int j = ++i; j < i + size; j++)
									k += (char)arr[j];
								i += size - 1;

								parsed.Add(k);
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

								parsed.Add(k);
								break;
							}
						case TypeTags.Enum8:
							{
								short k = arr[++i];

								parsed.Add(k);
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
								parsed.Add(d);
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
								parsed.Add(d);
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
								parsed.Add(d);
								break;
							}
						case TypeTags.VisibleString8:
                            {
								byte size = arr[++i];
								string tmp = "";
								for (int j = 0; j < size; j++)
									tmp += arr[++i];

								parsed.Add(tmp);
								break;
                            }
						case TypeTags.Array8:
                            {
								int size = arr[++i];
								int temptag = arr[++i];
								parsed.Add(Dec(arr, size, false, temptag));
								break;
                            }
						case TypeTags.Array16:
							{
								int size = 0;
								for (int j = ++i; j < i + 2 && j < arrsize; j++)
									size += (arr[j] << 8 * (i + 1 - j)); i++;

								int temptag = arr[++i];
								parsed.Add(Dec(arr, size, false, temptag));
								break;
							}
						case TypeTags.Array32:
							{
								int size = 0;
								for (int j = ++i; j < i + 4 && j < arrsize; j++)
									size += arr[j] << 8 * (i + 3 - j); i += 3;

								int temptag = arr[++i];
								parsed.Add(Dec(arr, size, false, temptag));
								break;
							}
						case TypeTags.Sequence32:
                            {
								int size = 0;
								for (int j = ++i; j < i + 4 && j < arrsize; j++)
									size += arr[j] << 8 * (i + 3 - j); i += 3;
								parsed.Add(Dec(arr, size));
								break;
                            }
						case TypeTags.Structure8:
                            {
								i++;
								parsed.Add(Dec(arr, arr[i++]));
								break;
                            }
						case TypeTags.Float32:
                            {
								byte[] temparr = new byte[4];
								for (int j = 0; j < 4; j++)
									temparr[j] = arr[++i];
								Array.Reverse(temparr);
								parsed.Add(BitConverter.ToSingle(temparr, 0)); 
								break;
                            }
						case TypeTags.Float64:
							{
								byte[] temparr = new byte[8];
								for (int j = 0; j < 8; j++)
									temparr[j] = arr[++i];
								Array.Reverse(temparr);
								parsed.Add(BitConverter.ToDouble(temparr, 0));
								break;
							}
						case TypeTags.Numeric18t:
                            {
								Numeric18t n;
								n.Perc = arr[++i];
								n.Value = 0;
								for (int j = ++i; j < i + 8 && j < arrsize; j++)
									n.Value += (ulong)arr[j] << 8 * (i + 7 - j); i += 7;
								parsed.Add(n);
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
								parsed.Add(g);
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
								parsed.Add(h);
								break;
                            }
						case TypeTags.StructureContext:
                            {
								ushort tmp = 0;
								for (int j = ++i; j < i + 2 && j < arrsize; j++)
									tmp += (ushort)(arr[j] << 8 * (i + 1 - j)); i++;
								parsed.Add(tmp);
								parsed.Add(Dec(arr, arr[i++], true));
								break;
                            }
						default://any tagged structure
                            {
								i++;
								Dictionary<int, dynamic> tempdic = new Dictionary<int, dynamic>();
								List<dynamic> temp = Dec(arr, arr[i++], true);
								for (int j = 0; j < temp.Count - 1; j += 2)
									tempdic[temp[j]] = temp[j + 1];

								parsed.Add(tempdic);
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