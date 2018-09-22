using Eleveight.Models.Domain.User;
using Eleveight.Models.Requests.User;
using Eleveight.Services.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleveight.Services.User
{
    public class AwardTypeService : BaseService, IAwardTypeService
    {
        public int Insert(AwardTypeAddRequest model)

        {
            int returnValue = 0;

            DataProvider.ExecuteNonQuery("dbo.AwardType_Insert",
               inputParamMapper: (SqlParameterCollection inputs) =>
               {
                   inputs.Add(SqlDbParameter.Instance.BuildParameter("@TypeName", model.TypeName, SqlDbType.NVarChar, 128));
                   SqlParameter idOut = new SqlParameter("@Id", 0);
                   idOut.Direction = ParameterDirection.Output;

                   inputs.Add(idOut);
               },
               returnParameters: (SqlParameterCollection inputs) =>
               {
                   int.TryParse(inputs["@Id"].Value.ToString(), out returnValue);
               }
               );
            return returnValue;
        }

        List<AwardType> IAwardTypeService.SelectAll()
        {
            List<AwardType> list = new List<AwardType>();
            DataProvider.ExecuteCmd("dbo.AwardType_SelectAll",
                inputParamMapper: null,
                singleRecordMapper: (IDataReader reader, short resultSet) =>
                {
                    list.Add(DataMapper<AwardType>.Instance.MapToObject(reader));
                });
            return list;
        }

        AwardType IAwardTypeService.SelectById(int id)
        {
            AwardType awardType = new AwardType();
            DataProvider.ExecuteCmd("dbo.AwardType_SelectById",
               inputParamMapper: (SqlParameterCollection inputs) =>
               {
                   inputs.AddWithValue("@id", id);
               },
               singleRecordMapper: (IDataReader reader, short resultSet) =>
               {
                   awardType = DataMapper<AwardType>.Instance.MapToObject(reader);
               });
            return awardType;
        }

        public void Update(AwardTypeUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.AwardType_Update",
                inputParamMapper: (SqlParameterCollection inputs) =>
                {
                    inputs.Add(SqlDbParameter.Instance.BuildParameter("@Id", model.Id, SqlDbType.Int));
                    inputs.Add(SqlDbParameter.Instance.BuildParameter("@TypeName", model.TypeName, SqlDbType.NVarChar, 128));
                });
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.AwardType_Delete",
            inputParamMapper: (SqlParameterCollection inputs) =>
            {
                inputs.AddWithValue("@Id", id);
            });
        }

    }
}

