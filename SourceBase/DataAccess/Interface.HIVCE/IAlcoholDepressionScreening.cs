using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HIVCE.Common.Entities;

namespace Interface.HIVCE
{
    public interface IAlcoholDepressionScreening
    {
        AlcoholDepressionScreeningObj GetAlcoholDepressionScreening(int ptn_pk, int visitPK);
        bool SaveUpdateAlcoholDepressionScreening(AlcoholDepressionScreeningObj obj);
    }
}
