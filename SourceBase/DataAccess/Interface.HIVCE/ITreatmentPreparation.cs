using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HIVCE.Common.Entities;

namespace Interface.HIVCE
{
    public interface ITreatmentPreparation
    {
        TreatmentPreparation GetTreatmentPreparation(int ptn_pk);
        bool SaveUpdateTreatmentPreparation(TreatmentPreparation obj);
    }
}
