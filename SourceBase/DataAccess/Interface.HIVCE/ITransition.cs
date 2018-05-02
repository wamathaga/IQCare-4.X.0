using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HIVCE.Common.Entities;

namespace Interface.HIVCE
{
    public interface ITransition
    {
        Transition GetTransition(int ptn_pk, int visitPK);
        bool SaveUpdateTransition(Transition obj);
    }
}
