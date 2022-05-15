using System;
using System.Collections.Generic;
using System.Text;

namespace Keyboard
{
    public static class KeyCodes
    {
        public enum KeyCode
        {
            k0 = 0x30,
            k1 = 0x31,
            k2 = 0x32,
            k3 = 0x33,
            k4 = 0x34,
            k5 = 0x35,
            k6 = 0x36,
            k7 = 0x37,
            k8 = 0x38,
            k9 = 0x39,
            ka = 0x41,
            kb = 0x42,
            kc = 0x43,
            kd = 0x44,
            ke = 0x45,
            kf = 0x46,
            kg = 0x47,
            kh = 0x48,
            ki = 0x49,
            kj = 0x4A,
            kk = 0x4B,
            kl = 0x4C,
            km = 0x4D,
            kn = 0x4E,
            ko = 0x4F,
            kp = 0x50,
            kq = 0x51,
            kr = 0x52,
            ks = 0x53,
            kt = 0x54,
            ku = 0x55,
            kv = 0x56,
            kw = 0x57,
            kx = 0x58,
            ky = 0x59,
            kz = 0x5A,
            kspace = 0x20
        }

        public static int GetValueByName(string pName)
        {
            foreach (int val in Enum.GetValues(typeof(KeyCode)))
            {
                if (Enum.GetName(typeof(KeyCode), val) == pName) return val;
            }
            return -1;
        }

        public static int[] GetValuesFromText(string pText)
        {
            List<int> result = new List<int>();
            foreach(char c in pText)
            {
                int res = GetValueByName("k"+c.ToString());
                if (res > 0) result.Add(res);
            }
            return result.ToArray();
        }
    }
}
