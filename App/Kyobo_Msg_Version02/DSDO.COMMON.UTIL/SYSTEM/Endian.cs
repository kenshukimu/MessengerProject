using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.SYSTEM
{
    public class Endian
    {
        /// <summary>
        /// double 엔디안을 스왑하고 스왑 된 값을 반환
        /// </summary>
        /// <param name="val">the value to swap.</param>
        /// <returns>double 엔디안을 스왑하고 스왑 된 값을 반환</returns>
        public static double Swap(double val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            Array.Reverse(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        /// <summary>
        /// float 엔디안을 스왑하고 스왑 된 값을 반환
        /// </summary>
        /// <param name="val">the value to swap.</param>
        /// <returns>float 부동 엔디안을 스왑하고 스왑 된 값을 반환</returns>
        public static float Swap(float val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// int 엔디안을 스왑하고 스왑 된 값을 반환
        /// </summary>
        /// <param name="val">the value to swap.</param>
        /// <returns>the swapped value</returns>
        public static int Swap(int val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// uint 엔디안을 스왑하고 스왑 된 값을 반환
        /// </summary>
        /// <param name="val">the value to swap.</param>
        /// <returns>the swapped value</returns>
        public static uint Swap(uint val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        /// long 엔디안을 스왑하고 스왑 된 값을 반환
        /// </summary>
        /// <param name="val">the value to swap.</param>
        /// <returns>the swapped value</returns>
        public static long Swap(long val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            Array.Reverse(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// ulong 엔디안을 스왑하고 스왑 된 값을 반환
        /// </summary>
        /// <param name="val">the value to swap.</param>
        /// <returns>the swapped value</returns>
        public static ulong Swap(ulong val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            Array.Reverse(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        /// <summary>
        /// short 엔디안을 스왑하고 스왑 된 값을 반환
        /// </summary>
        /// <param name="val">the value to swap.</param>
        /// <returns>the swapped value</returns>
        public static short Swap(short val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            Array.Reverse(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        /// <summary>
        /// ushort 엔디안을 스왑하고 스왑 된 값을 반환
        /// </summary>
        /// <param name="val">the value to swap.</param>
        /// <returns>the swapped value</returns>
        public static ushort Swap(ushort val)
        {
            byte[] bytes = BitConverter.GetBytes(val);
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        /// <summary>
        /// 네트워크 순서에서 주어진 short 값을 변경하고 변경된 값을 반환
        /// </summary>
        /// <param name="host">the value to change order</param>
        /// <returns>value in network order</returns>
        public static short HostToNetWorkOrder(short host)
        {
            return IPAddress.HostToNetworkOrder(host);
        }

        /// <summary>
        /// 네트워크 순서에서 주어진 int 값을 변경하고 변경된 값을 반환
        /// </summary>
        /// <param name="host">the value to change order</param>
        /// <returns>value in network order</returns>
        public static int HostToNetWorkOrder(int host)
        {
            return IPAddress.HostToNetworkOrder(host);
        }

        /// <summary>
        /// 네트워크 순서에서 주어진 long 값을 변경하고 변경된 값을 반환
        /// </summary>
        /// <param name="host">the value to change order</param>
        /// <returns>value in network order</returns>
        public static long HostToNetWorkOrder(long host)
        {
            return IPAddress.HostToNetworkOrder(host);
        }

        /// <summary>
        /// 호스트 순서에서 지정된 short 값을 변경하고 변경된 값을 반환
        /// </summary>
        /// <param name="host">the value to change order</param>
        /// <returns>value in host order</returns>
        public static short NetworkToHostOrder(short host)
        {
            return IPAddress.NetworkToHostOrder(host);
        }

        /// <summary>
        /// 호스트 순서에서 지정된 int 값을 변경하고 변경된 값을 반환
        /// </summary>
        /// <param name="host">the value to change order</param>
        /// <returns>value in host order</returns>
        public static int NetworkToHostOrder(int host)
        {
            return IPAddress.NetworkToHostOrder(host);
        }

        /// <summary>
        /// 호스트 순서에서 지정된 long 값을 변경하고 변경된 값을 반환
        /// </summary>
        /// <param name="host">the value to change order</param>
        /// <returns>value in host order</returns>
        public static long NetworkToHostOrder(long host)
        {
            return IPAddress.NetworkToHostOrder(host);
        }

        /// <summary>
        /// 현재머신이 리틀엔디안 인지여부
        /// </summary>
        /// <returns>true if current machine is in little endian, otherwise false</returns>
        public static bool IsLittleEndian()
        {
            return BitConverter.IsLittleEndian;
        }

        /// <summary>
        /// 현재머신이 빅엔디안 인지여부
        /// </summary>
        /// <returns>true if current machine is in big endian, otherwise false</returns>
        public static bool IsBigEndian()
        {
            return !BitConverter.IsLittleEndian;
        }
    }
}
