using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Uwl.Utility.Common
{
    public class PageHelper
    {
        public static PageDataView<T> GetDataInPage<T>(PageCriteria pageCriteria)
        {
            if(pageCriteria.ParamsList.Count<=0)
                 throw new ArgumentException("只少传入一个参数");
            using (SqlConnection context =new SqlConnection(UwlDbContext))
            {

            }
        }
    }
}
