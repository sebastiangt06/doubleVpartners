using DoubleV.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DoubleV.Application.Feature
{
    public class ResponseApiService
    {
        public static IActionResult Response(int Statuscode, dynamic Data = null, string message = null)
        {
            bool success = false;
            int element = 0;

            if (Statuscode >= 200 && Statuscode < 300)
                success = true;
            if (Data != null)
            {
                if (Data.GetType() != Type.GetType("System.Boolean") && Data.GetType() != Type.GetType("System.String"))
                {
                    try
                    {
                        if (Data != null) { element = Enumerable.Count(Data); }
                    }
                    catch (Exception)
                    {

                        element = 1;
                    }
                   
                }
                else if (Data.GetType() == Type.GetType("System.String"))
                {
                    try
                    {
                        Boolean.Parse(Data);
                    }
                    catch (Exception)
                    {
                        message = Data.ToString();
                        Data = null;
                    }
                }

            }

            BaseResponseModel result = new BaseResponseModel
            {
                Status = Statuscode,
                Succes = success,
                Message = message,
                Data = Data,
                totalElements = element

            };
            return new OkObjectResult(result){StatusCode = Statuscode};

        }

    }
}
