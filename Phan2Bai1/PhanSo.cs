using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phan2Bai1
{
    internal class PhanSo
    {
        private int m_nTuSo, m_nMauSo;
        
        public PhanSo(int tuso)
        {
            m_nTuSo = tuso;
            m_nMauSo = 1;
        }

        public PhanSo(int tuso, int mauso)
        {
            m_nTuSo = tuso;
            m_nMauSo = mauso;
        }

        public override string ToString()
        {
            return $"[{m_nTuSo} / {m_nMauSo}]";
        }

        public static PhanSo operator +(PhanSo a, PhanSo b)
        {
            int tu_so=a.m_nTuSo*b.m_nMauSo+b.m_nTuSo*a.m_nMauSo;
            int mau_so = a.m_nMauSo * b.m_nMauSo;
            return new PhanSo(tu_so, mau_so);
        }

        public static explicit operator float(PhanSo ps)
        {
            return (float)ps.m_nTuSo/ps.m_nMauSo;
        }

    }
}
