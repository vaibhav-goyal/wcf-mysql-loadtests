using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EmployeeManagement
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployee" in both code and config file together.
    [ServiceContract]
    public interface IEmployee
    {
        [OperationContract]
        [WebGet(UriTemplate = "api/v1/GetHiresByYear", 
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json)]
        List<GetHiresByYearResult> GetHiresByYear();
    }

    [DataContract]
    public class GetHiresByYearResult
    {
        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public long TotalHires { get; set; }
    }
}
