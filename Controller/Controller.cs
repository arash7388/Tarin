using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shared;

using Repository.DAL;


namespace Controller
{
    public class CommonController
    {
        private AdvertisementRepository advertisement;
        
        public CommonController()
        {
            advertisement = new AdvertisementRepository();
        }
        
        public Object ExecuteOperation(String operationType, Dictionary<String, Object> data)
        {
            Object retVal = new Object();

            //Implementing a global try-catch block, never use this shortcut for production version 
            try
            {
                switch (operationType)
                {
                    case OperationType.Read:
                        retVal = advertisement.ReadAdvertisementsData();
                        break;
                    case OperationType.ReadById:
                       
                        retVal = advertisement.ReadAdvertisementById(data);
                        break;
                    case OperationType.ReadByCategory:
                        retVal = advertisement.ReadBookByCategory(data);
                        break;

                    case OperationType.ReadMenus:
                        retVal = advertisement.ReadMenus();
                        //retVal = manager.ReadBookByCategory(data);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(String.Concat(Exceptions.Error, ": ", exception.Message));
            }
            return retVal;
        }
    }
}
