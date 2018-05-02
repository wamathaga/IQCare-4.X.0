using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interface.Pharmacy
{
    public interface IBIQTouchImmunisation
    {
        //int SaveUpdateImmunisationDetail(int ptn_pk,int visit_pk,int location_id,int immunisation_id,DateTime immunisationDate,int ImmunisationCU,int UserId,DateTime CreateDate,DateTime UpdateDate);
        int SaveUpdateImmunisationDetail(List<BIQTouchmmunisationFields> immnisationFields);
        DataSet GetImmunisationDetails(BIQTouchmmunisationFields immnisationFields);




    }
    [Serializable()]
    public class BIQTouchmmunisationFields
    {
        public int Ptnpk { get; set; }
        public int LocationId { get; set; }
        public string ImmunisationCode { get; set; }
        public DateTime ImmunisationDate { get; set; }
        public int ImmunisationCU { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CardAvailable { get; set; }
        public string ImmunisationOther { get; set; }
        public int Flag { get; set; }




    }






}

