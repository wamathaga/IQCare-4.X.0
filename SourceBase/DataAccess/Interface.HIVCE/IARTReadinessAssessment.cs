using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HIVCE.Common.Entities;

namespace Interface.HIVCE
{
    public interface IARTReadinessAssessment
    {
        ARTReadinessAssessment GetARTReadinessAssessment(int ptn_pk, int visitPK);
        bool SaveUpdateARTReadinessAssessment(ARTReadinessAssessment obj);
    }
}
