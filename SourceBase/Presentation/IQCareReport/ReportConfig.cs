using System.Collections.Generic;
using System.Linq;
namespace IQCare.CustomConfig
{
    public class ReportConfig
    {
        /// <summary>
        /// The _instances
        /// </summary>
        protected static Dictionary<string, ReportInstanceElement> _instances;
        /// <summary>
        /// Initializes the <see cref="ReportConfig"/> class.
        /// </summary>
        static ReportConfig()
        {
            _instances = new Dictionary<string, ReportInstanceElement>();
            ReportConfigSection sec = (ReportConfigSection)System.Configuration.ConfigurationManager.GetSection("reportCustomConfig");
            foreach (ReportInstanceElement i in sec.Instances)
            {
                _instances.Add(i.Code, i);
            }
        }
        public static ReportInstanceElement GetReportConfigElement(string code)
        {
            return _instances[code];
        }
        /// <summary>
        /// Gets the report configuration elements.
        /// </summary>
        /// <value>
        /// The report configuration elements.
        /// </value>
        public static List<ReportInstanceElement> ReportConfigElements
        {
            get { return _instances.Values.ToList();
            }
        }
        /// <summary>
        /// Instanceses the specified instance name.
        /// </summary>
        /// <param name="instanceName">Name of the instance.</param>
        /// <returns></returns>
        public static ReportInstanceElement Instances(string instanceName)
        {
            return _instances[instanceName];
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ReportConfig"/> class from being created.
        /// </summary>
        private ReportConfig()
        {
        }
    }
    //public class PaymentMethodConfig
    //{
    //    /// <summary>
    //    /// The _instances
    //    /// </summary>
    //    protected static Dictionary<string, PayMethodElement> _instances;
    //    /// <summary>
    //    /// Initializes the <see cref="ReportConfig"/> class.
    //    /// </summary>
    //    static PaymentMethodConfig()
    //    {
    //        _instances = new Dictionary<string, PayMethodElement>();
    //        PaymentMethodConfigSection sec = (PaymentMethodConfigSection)System.Configuration.ConfigurationManager.GetSection("paymentMethodConfig");
    //        foreach (PayMethodElement i in sec.Instances)
    //        {
    //            _instances.Add(i.MethodName, i);
    //        }
    //    }
    //    /// <summary>
    //    /// Gets the report configuration element.
    //    /// </summary>
    //    /// <param name="name">The name.</param>
    //    /// <returns></returns>
    //    public static PayMethodElement GetPayMethodConfigElement(string name)
    //    {
    //        return _instances[name];
    //    }

    //    /// <summary>
    //    /// Gets the pay method available.
    //    /// </summary>
    //    /// <value>
    //    /// The pay method available.
    //    /// </value>
    //    public static List<PayMethodElement> PayMethodAvailable
    //    {
    //        get
    //        {
    //            return _instances.Values.ToList();
    //        }
    //    }
    //    /// <summary>
    //    /// Instanceses the specified instance name.
    //    /// </summary>
    //    /// <param name="instanceName">Name of the instance.</param>
    //    /// <returns></returns>
    //    public static PayMethodElement Instances(string instanceName)
    //    {
    //        return _instances[instanceName];
    //    }

    //    /// <summary>
    //    /// Prevents a default instance of the <see cref="ReportConfig"/> class from being created.
    //    /// </summary>
    //    private PaymentMethodConfig()
    //    {
    //    }
    //}

}