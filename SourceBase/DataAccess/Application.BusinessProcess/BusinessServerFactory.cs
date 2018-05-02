using System;
using Application.Interface;

namespace Application.BusinessProcess
{
    public class BusinessServerFactory : MarshalByRefObject, IBusinessServerFactory
    {
        public Object CreateInstance(string type)
        {
            object remserver = null;
            try
            {
                remserver = Activator.CreateInstance(Type.GetType(type));
            }
            catch(Exception ex)
            {
                string str = ex.StackTrace.ToString();
            }
           
            //return (IRemServer)Convert.ChangeType(remserver,typeof(IRemServer));
            return remserver; 
        }
    }
}
